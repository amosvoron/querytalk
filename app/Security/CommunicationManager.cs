using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using QueryTalk.Globals;
using System.Web;

namespace QueryTalk.Security
{
    internal class CommunicationManager
    {
        //internal static event DownloadDataCompletedEventHandler DownloadFileToMemoryCompleted;

        /// <summary>
        /// Starts communication with the server.
        /// </summary>
        /// <param name="communicationID">
        /// Is a string created by Guid.NewGuid().ToString("D") command.
        /// It will always have 36 characters/bytes (https://en.wikipedia.org/wiki/Universally_unique_identifier).
        /// </param>
        internal static string StartCommunication(string communicationID)
        {
            try
            {
                byte[] bytes;
                string responseStr = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GlobalResources.StartCommunicationUrl);

                //CryptographyManager objCryptographyManager = new CryptographyManager();

                var xml = string.Format("<message><communicationID>{0}</communicationID><RSA>{1}</RSA></message>", communicationID, CryptoContext.RSAinternalKey);
                bytes = Encoding.ASCII.GetBytes(xml);

                request.ContentType = "text/xml; encoding='utf-8'";
                request.ContentLength = bytes.Length;
                request.Method = "POST";

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                HttpWebResponse responseHttpWebResponse;
                responseHttpWebResponse = (HttpWebResponse)request.GetResponse();

                if (responseHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    return Deny(GlobalResources.ServerFailedMessage);
                }

                using (var responseStream = responseHttpWebResponse.GetResponseStream())
                {
                    responseStr = new StreamReader(responseStream).ReadToEnd();

                    if (string.IsNullOrEmpty(responseStr))
                    {
                        return Deny(GlobalResources.ServerFailedMessage);
                    }

                    if (IsDenied(responseStr))
                    {
                        return responseStr;
                    }

                    CryptoContext.CryptedSimKey = responseStr;

                    return Allow();
                }
            }
            catch (Exception ex)
            {
                return Deny(GlobalResources.ServerFailedMessage);
            }
        }

        internal static string ExecuteCommand(string communicationID, string command, string user, string password, string email)
        {
            string decryptedSimKey, responseStr;
            CryptographyManager cryptographyManager;
            responseStr = GlobalResources.ServerFailedMessage;

            var xmlMessage = PrepareCommand(communicationID, command, user, password, email, out decryptedSimKey, out cryptographyManager);

            //Envia el mensaje obtiene el resultado
            if (command == Commands.AUTHENTICATE ||
                command == Commands.REMOVE)
            {
                responseStr = SendMessageLicense(xmlMessage, decryptedSimKey, user, password);
            }
            else if (command == Commands.AUTHENTICATE_ANONYMOUS)
            {
                responseStr = Anonymous.SendMessageLicense(xmlMessage, decryptedSimKey);
            }
            else if (command == Commands.GETVERSIONS)
            {
                responseStr = SendMessageGetVersions(xmlMessage, decryptedSimKey);
            }
            else if (command == Commands.VIEWLICENSE)
            {
                responseStr = ViewLicense(cryptographyManager.EncryptSim(xmlMessage, CryptoContext.ComunicationKey));
            }
            else if (command == Commands.REGISTEREMAIL)
            {
                responseStr = RegisterAnonymousEmail(xmlMessage, decryptedSimKey);
            }

            decryptedSimKey = string.Empty;
            return responseStr;
        }

        // prepara el mensaje XML criptado 
        internal static string PrepareCommand(string communicationID, string command, string user, string password, string email,
            out string decryptedSimKey, out CryptographyManager cryptographyManager)
        {
            Message message = new Message();
            message.CommunicationID = communicationID;
            message.Command = command;
            message.CK = CryptoContext.ComunicationKey;
            message.User = user;
            message.Password = password;
            message.MK = MachineManager.MachineKey;
            message.ComputerName = MachineManager.ComputerName;
            message.Manufacturer = MachineManager.ComputerManufacturer;
            message.Model = MachineManager.Model;
            message.Processor = MachineManager.Procesor;
            message.Email = email;

            cryptographyManager = new CryptographyManager();
            decryptedSimKey = cryptographyManager.DecryptAsim(CryptoContext.CryptedSimKey, CryptoContext.RSAPrivateKey);

            return CreateXmlMessage(message, decryptedSimKey);
        }

        internal static string SendXmlMessage(string xmlMessage, string url)
        {
            byte[] bytes;
            string responseStr = "";

            bytes = Encoding.ASCII.GetBytes(xmlMessage);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();

            HttpWebResponse responseHttpWebResponse;
            responseHttpWebResponse = (HttpWebResponse)request.GetResponse();
            if (responseHttpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = responseHttpWebResponse.GetResponseStream();
                responseStr = new StreamReader(responseStream).ReadToEnd();
            }

            return responseStr;
        }

        private static string SendMessageLicense(string xmlMessage, string decryptedsimkey, string user, string password)
        {
            string responseStr = "";
            Message xmlResponseMessage;

            responseStr = SendXmlMessage(xmlMessage, GlobalResources.LicenseUrl);

            if (string.IsNullOrEmpty(responseStr))
            {
                return Deny(GlobalResources.ServerFailedMessage);
            }

            if (IsDenied(responseStr))
            {
                return responseStr;
            }

            //Los mensajes de los servicios de licenciamiento se encriptan doblemente en el servidor con el ComunicationKey
            CryptographyManager objQueryTalkCryptography = new CryptographyManager();
            responseStr = objQueryTalkCryptography.DecryptSim(responseStr, CryptoContext.ComunicationKey);

            xmlResponseMessage = ReadXML(responseStr, decryptedsimkey, true);

            //Los mensajes de los servicios de licenciamiento se verifica que no se alterara ninguno de los datos originalmente enviados
            //y que hacen unica y universal la comunicación entre el usuario/machine/servidor
            if (!IsValidResponse(xmlResponseMessage as ResponseMessage, user, password))
            {
                return Deny(GlobalResources.ServerFailedMessage);
            }

            return (xmlResponseMessage as ResponseMessage).Response;
        }

        private static string SendMessageGetVersions(string xmlMessage, string decryptedsimkey)
        {
            string responseStr = "";
            Message xmlResponseMessage;

            responseStr = SendXmlMessage(xmlMessage, GlobalResources.VersionsUrl);

            if (string.IsNullOrEmpty(responseStr))
            {
                return Deny(GlobalResources.ServerFailedMessage);
            }

            if (IsDenied(responseStr))
            {
                return responseStr;
            }

            xmlResponseMessage = ReadXML(responseStr, decryptedsimkey, true);
            return (xmlResponseMessage as ResponseMessage).Response;
        }

        private static string ViewLicense(string xmlMessage)
        {
            string param = HttpUtility.UrlEncode(xmlMessage);

            string dir = GlobalResources.ViewLicenseUrl + "?id=" + param;
            System.Diagnostics.Process.Start(dir);

            return Allow();
        }

        private static string RegisterAnonymousEmail(string xmlMessage, string decryptedsimkey)
        {
            var responseStr = SendXmlMessage(xmlMessage, GlobalResources.RegisterAnonymousEmailUrl);

            if (string.IsNullOrEmpty(responseStr))
            {
                return Deny(GlobalResources.ServerFailedMessage);
            }

            if (IsDenied(responseStr))
            {
                return responseStr;
            }

            CryptographyManager objQueryTalkCryptography = new CryptographyManager();
            responseStr = objQueryTalkCryptography.DecryptSim(responseStr, CryptoContext.ComunicationKey);

            var xmlResponseMessage = ReadXML(responseStr, decryptedsimkey, true);

            //Los mensajes de los servicios de licenciamiento se verifica que no se alterara ninguno de los datos originalmente enviados
            //y que hacen unica y universal la comunicación entre el usuario/machine/servidor
            if (!IsValidResponse(xmlResponseMessage as ResponseMessage))
            {
                return Deny(GlobalResources.ServerFailedMessage);
            }

            return (xmlResponseMessage as ResponseMessage).Response;
        }

        internal static string CreateXmlMessage(Message message, string simKey)
        {
            string xmlContent = "";

            CryptographyManager objQueryTalkCryptography = new CryptographyManager();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            StringWriter writer = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(writer, settings))
            {
                xmlWriter.WriteStartElement("message");

                //Guid
                xmlWriter.WriteStartElement("block0");
                //GuidText
                xmlWriter.WriteElementString("block0value1", message.CommunicationID);
                xmlWriter.WriteEndElement();

                //Command
                xmlWriter.WriteStartElement("block1");
                //CommandText
                xmlWriter.WriteElementString("block1value1", objQueryTalkCryptography.EncryptSim(message.Command, simKey));
                xmlWriter.WriteEndElement();

                if (message.Command != Commands.VIEWLICENSE)
                {
                    //CK
                    xmlWriter.WriteStartElement("block2");
                    //CKText
                    xmlWriter.WriteElementString("block2value1", objQueryTalkCryptography.EncryptSim(message.CK, simKey));
                    xmlWriter.WriteEndElement();
                }

                //Credentials
                xmlWriter.WriteStartElement("block3");
                //User
                xmlWriter.WriteElementString("block3value1", objQueryTalkCryptography.EncryptSim(message.User, simKey));
                //Password
                xmlWriter.WriteElementString("block3value2", objQueryTalkCryptography.EncryptSim(message.Password, simKey));
                xmlWriter.WriteEndElement();

                //Información necesaria solo para los servicios de licenciamiento
                if (message.Command == Commands.AUTHENTICATE ||
                    message.Command == Commands.AUTHENTICATE_ANONYMOUS ||
                    message.Command == Commands.REMOVE ||
                    message.Command == Commands.REGISTEREMAIL)
                {
                    //ComputerInfo
                    xmlWriter.WriteStartElement("block4");
                    //MK
                    xmlWriter.WriteElementString("block4value1", objQueryTalkCryptography.EncryptSim(message.MK, simKey));
                    //ComputerName
                    xmlWriter.WriteElementString("block4value2", objQueryTalkCryptography.EncryptSim(message.ComputerName, simKey));
                    //Manufacturer
                    xmlWriter.WriteElementString("block4value3", objQueryTalkCryptography.EncryptSim(message.Manufacturer, simKey));
                    //Model
                    xmlWriter.WriteElementString("block4value4", objQueryTalkCryptography.EncryptSim(message.Model, simKey));
                    //Processor
                    xmlWriter.WriteElementString("block4value5", objQueryTalkCryptography.EncryptSim(message.Processor, simKey));
                    xmlWriter.WriteEndElement();
                }

                //Información usada para los responses desde el servidor
                if (message is ResponseMessage)
                {
                    //Response
                    xmlWriter.WriteStartElement("block5");
                    //CommandText
                    xmlWriter.WriteElementString("block5value1", objQueryTalkCryptography.EncryptSim((message as ResponseMessage).Response, simKey));
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndDocument();
                }

                if (message.Command == Commands.REGISTEREMAIL)
                {
                    //CK
                    xmlWriter.WriteStartElement("block6");
                    //CKText
                    xmlWriter.WriteElementString("block6value1", objQueryTalkCryptography.EncryptSim(message.Email, simKey));
                    xmlWriter.WriteEndElement();
                }

                writer.Flush();
            }

            xmlContent = writer.ToString();
            return xmlContent;
        }

        internal static Message ReadXML(string xmlString, string simKey, bool response)
        {
            return ReadXML(xmlString, cid => simKey, response);
        }

        internal static Message ReadXML(string xmlString, Func<string, string> simKeyProvider, bool response)
        {
            Message message = response ? new ResponseMessage() : new Message();
            CryptographyManager objCrypto = new CryptographyManager();

            XmlNode node;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            node = doc.DocumentElement.SelectSingleNode("/message/block0/block0value1");
            message.CommunicationID = node.InnerText;

            // get symetric key
            var simKey = simKeyProvider(message.CommunicationID);

            node = doc.DocumentElement.SelectSingleNode("/message/block1/block1value1");
            message.Command = objCrypto.DecryptSim(node.InnerText, simKey);

            if (message.Command != Commands.VIEWLICENSE)
            {
                node = doc.DocumentElement.SelectSingleNode("/message/block2/block2value1");
                message.CK = objCrypto.DecryptSim(node.InnerText, simKey);
            }

            node = doc.DocumentElement.SelectSingleNode("/message/block3/block3value1");
            message.User = objCrypto.DecryptSim(node.InnerText, simKey);
            node = doc.DocumentElement.SelectSingleNode("/message/block3/block3value2");
            message.Password = objCrypto.DecryptSim(node.InnerText, simKey);

            //Información necesaria solo para los servicios de licenciamiento
            if (message.Command == Commands.AUTHENTICATE ||
                message.Command == Commands.AUTHENTICATE_ANONYMOUS ||
                message.Command == Commands.REMOVE ||
                message.Command == Commands.REGISTEREMAIL)
            {
                node = doc.DocumentElement.SelectSingleNode("/message/block4/block4value1");
                message.MK = objCrypto.DecryptSim(node.InnerText, simKey);
                node = doc.DocumentElement.SelectSingleNode("/message/block4/block4value2");
                message.ComputerName = objCrypto.DecryptSim(node.InnerText, simKey);
                node = doc.DocumentElement.SelectSingleNode("/message/block4/block4value3");
                message.Manufacturer = objCrypto.DecryptSim(node.InnerText, simKey);
                node = doc.DocumentElement.SelectSingleNode("/message/block4/block4value4");
                message.Model = objCrypto.DecryptSim(node.InnerText, simKey);
                node = doc.DocumentElement.SelectSingleNode("/message/block4/block4value5");
                message.Processor = objCrypto.DecryptSim(node.InnerText, simKey);
            }

            //Información usada para los responses desde el servidor
            if (response)
            {
                node = doc.DocumentElement.SelectSingleNode("/message/block5/block5value1");
                (message as ResponseMessage).Response = objCrypto.DecryptSim(node.InnerText, simKey);
            }

            if (message.Command == Commands.REGISTEREMAIL)
            {
                node = doc.DocumentElement.SelectSingleNode("/message/block6/block6value1");
                message.Email = objCrypto.DecryptSim(node.InnerText, simKey);
            }

            return message;
        }

        private static bool IsValidResponse(ResponseMessage xmlResponseMessage, string user, string password)
        {
            return
                xmlResponseMessage.CK == CryptoContext.ComunicationKey &&
                xmlResponseMessage.User == user &&
                xmlResponseMessage.Password == password &&
                xmlResponseMessage.MK == MachineManager.MachineKey;
        }

        private static bool IsValidResponse(ResponseMessage xmlResponseMessage)
        {
            return
                xmlResponseMessage.CK == CryptoContext.ComunicationKey &&
                xmlResponseMessage.MK == MachineManager.MachineKey;
        }

        internal static string Deny(string message)
        {
            return message;
        }

        internal static string Allow()
        {
            return ResponseType.Allowed.ToString();
        }

        internal static bool IsDenied(string value)
        {
            return Regex.IsMatch(value ?? "", "^Denied");
        }

    }

    internal static class Commands
    {
        internal static readonly string AUTHENTICATE = "AUTHENTICATE";
        internal static readonly string AUTHENTICATE_ANONYMOUS = "AUTHENTICATE-ANONYMOUS";
        internal static readonly string REMOVE = "REMOVE";
        internal static readonly string GETVERSIONS = "GETVERSIONS";
        internal static readonly string PULLLIB = "PULLLIB";
        internal static readonly string PULLLIBDOC = "PULLLIBDOC";
        internal static readonly string PULLMAPPER = "PULLMAPPER";
        internal static readonly string VIEWLICENSE = "VIEWLICENSE";
        public static readonly string REGISTEREMAIL = "REGISTEREMAIL";
    }
}

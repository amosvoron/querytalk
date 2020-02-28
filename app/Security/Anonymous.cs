using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryTalk.Globals;
using QueryTalk.Security;

namespace QueryTalk.Security
{
    internal class Anonymous
    {

        internal static string SendMessageLicense(string xmlMessage, string decryptedsimkey)
        {
            string responseStr = "";
            Message xmlResponseMessage;

            // get server response
            responseStr = CommunicationManager.SendXmlMessage(xmlMessage, GlobalResources.LicenseAnonymousUrl);

            if (string.IsNullOrEmpty(responseStr))
            {
                return CommunicationManager.Deny(GlobalResources.ServerFailedMessage);
            }

            if (CommunicationManager.IsDenied(responseStr))
            {
                return responseStr;
            }

            CryptographyManager objQueryTalkCryptography = new CryptographyManager();
            responseStr = objQueryTalkCryptography.DecryptSim(responseStr, CryptoContext.ComunicationKey);
            xmlResponseMessage = CommunicationManager.ReadXML(responseStr, decryptedsimkey, true);
            if (!IsValidResponseNoUser(xmlResponseMessage as ResponseMessage))
            {
                return CommunicationManager.Deny(GlobalResources.ServerFailedMessage);
            }

            return (xmlResponseMessage as ResponseMessage).Response;
        }

        private static bool IsValidResponseNoUser(ResponseMessage xmlResponseMessage)
        {
            return
                xmlResponseMessage.CK == CryptoContext.ComunicationKey &&
                xmlResponseMessage.MK == MachineManager.MachineKey;
        }

    }
}

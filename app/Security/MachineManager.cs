using System.Text;
using System.Security.Cryptography;

namespace QueryTalk.Security
{
    internal class MachineManager
    {
        private static string _machinekey = string.Empty;
        private static string _computerName = string.Empty;
        private static string _computerManufacturer = string.Empty;
        private static string _model = string.Empty;
        private static string _procesor = string.Empty;

        private static object _locker = new object();

        internal static string MachineKey
        {
            get
            {
                lock (_locker)
                {
                    if (string.IsNullOrEmpty(_machinekey))
                    {
                        string _cpuId = cpuId();
                        string _biosId = biosId();
                        string _baseId = baseId();

                        _machinekey = GetHash("CPU >> " + _cpuId + "\nBIOS >> " + _biosId + "\nBASE >> " + _baseId
                                             //+"\nDISK >> "+ diskId() + "\nVIDEO >> " + videoId() +"\nMAC >> "+ macId()
                                             );
                    }
                    return _machinekey;
                }
            }
        }
        internal static string ComputerName
        {
            get
            {
                if (string.IsNullOrEmpty(_computerName))
                {
                    _computerName = identifier("Win32_ComputerSystem", "Name").Trim();
                }
                return _computerName;
            }
        }
        internal static string ComputerManufacturer
        {
            get
            {
                if (string.IsNullOrEmpty(_computerManufacturer))
                {
                    _computerManufacturer = identifier("Win32_ComputerSystem", "Manufacturer").Trim();
                }
                return _computerManufacturer;
            }
        }
        internal static string Model
        {
            get
            {
                if (string.IsNullOrEmpty(_model))
                {
                    _model = identifier("Win32_ComputerSystem", "Model").Trim();
                }
                return _model;
            }
        }
        internal static string Procesor
        {
            get
            {
                if (string.IsNullOrEmpty(_procesor))
                {
                    _procesor = identifier("Win32_Processor", "Name").Trim() + identifier("Win32_Processor", "CurrentClockSpeed").Trim();
                }
                return _procesor;
            }
        }

        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }

        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }

        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            // the following keys do not exist:
            if ((wmiClass == "Win32_Processor" && wmiProperty == "UniqueId")
                || (wmiClass == "Win32_BIOS" && wmiProperty == "IdentificationCode")
                || (wmiClass == "Win32_BaseBoard" && wmiProperty == "Model"))
            {
                return string.Empty;
            }

            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    { }
                }
            }
            return result;
        }

        //CPU Id
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId").Trim();
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name").Trim();
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer").Trim();
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed").Trim();
                }
            }
            return retVal;
        }

        //BIOS Identifier
        private static string biosId()
        {
            string _Manufacturer = identifier("Win32_BIOS", "Manufacturer").Trim();
            string _SMBIOSBIOSVersion = identifier("Win32_BIOS", "SMBIOSBIOSVersion").Trim();
            string _IdentificationCode = identifier("Win32_BIOS", "IdentificationCode").Trim();
            string _SerialNumber = identifier("Win32_BIOS", "SerialNumber").Trim();
            string _ReleaseDate = identifier("Win32_BIOS", "ReleaseDate").Trim();
            string _Version = identifier("Win32_BIOS", "Version").Trim();

            return _Manufacturer + " " + _SMBIOSBIOSVersion + " " + _IdentificationCode + " " + _SerialNumber + " " + _ReleaseDate + " " + _Version;
        }

        //Main physical hard drive ID
        private static string diskId()
        {
            return
            identifier("Win32_DiskDrive", "Model").Trim() + " "
            + identifier("Win32_DiskDrive", "Manufacturer").Trim() + " "
            + identifier("Win32_DiskDrive", "Signature").Trim() + " "
            + identifier("Win32_DiskDrive", "TotalHeads").Trim();
        }

        //Motherboard ID
        private static string baseId()
        {
            string _Model = identifier("Win32_BaseBoard", "Model").Trim();
            string _Manufacturer = identifier("Win32_BaseBoard", "Manufacturer").Trim();
            string _Name = identifier("Win32_BaseBoard", "Name").Trim();
            string _ModelSerialNumber = identifier("Win32_BaseBoard", "SerialNumber").Trim();

            return _Model + " " + _Manufacturer + " " + _Name + " " + _ModelSerialNumber;
        }

        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion").Trim() + " " + identifier("Win32_VideoController", "Name").Trim();
        }

        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled").Trim();
        }

        #endregion
    }
}

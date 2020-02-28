namespace QueryTalk.Security
{
    internal class Message
    {
        internal string CommunicationID { get; set; }
        internal string Command { get; set; }
        internal string CK { get; set; }
        internal string User { get; set; }
        internal string Password { get; set; }
        internal string MK { get; set; }
        internal string ComputerName { get; set; }
        internal string Manufacturer { get; set; }
        internal string Model { get; set; }
        internal string Processor { get; set; }
        internal string Email { get; set; }

        internal ResponseMessage CloneToResponse()
        {
            ResponseMessage objResult = new ResponseMessage();

            objResult.CommunicationID = this.CommunicationID;
            objResult.Command = this.Command;
            objResult.CK = this.CK;
            objResult.User = this.User;
            objResult.Password = this.Password;
            objResult.MK = this.MK;
            objResult.ComputerName = this.ComputerName;
            objResult.Manufacturer = this.Manufacturer;
            objResult.Model = this.Model;
            objResult.Processor = this.Processor;

            return objResult;

        }
    }

    internal class ResponseMessage : Message
    {
        internal string Response { get; set; }
    }
}
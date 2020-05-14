using System;
using System.Runtime.Serialization;

namespace BizTalk.Adapters.PowerShellTransmitter
{
    public class PowerShellAdapterException : ApplicationException
    {
        public static readonly string UnhandledTransmitError = "The PowerShellAdapter encountered and error transmitting message.";

        public PowerShellAdapterException() { }

        public PowerShellAdapterException(string msg) : base(msg) { }

        public PowerShellAdapterException(Exception inner) : base(string.Empty, inner) { }

        public PowerShellAdapterException(string msg, Exception e) : base(msg, e) { }

        protected PowerShellAdapterException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

using System;
using Microsoft.BizTalk.Adapter.Common;

namespace BizTalk.Adapters.PowerShellTransmitter
{
    public class PowerShellTransmitAdapter : AsyncTransmitter
    {
        internal static readonly string PowerShellNamespace = "http://schemas.microsoft.com/BizTalk/2003/Messaging/Transmit/PowerShell-properties";

        public PowerShellTransmitAdapter() : base(
			"PowerShell Transmit Adapter",
			"1.0",
			"Runs PowerShell scripts",
			"PowerShell script",
			new Guid("49f50364-0b81-4ed9-8c67-ad7ba5328dfe"),
			PowerShellNamespace,
			typeof(PowerShellTransmitterEndpoint),
			PowerShellTransmitterProperties.BatchSize)
		{
        }

        protected override void HandlerPropertyBagLoaded()
        {
            // implementation not needed since adapter has no handler properties
        }
    }
}

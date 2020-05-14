using System;
using System.Xml;
using Microsoft.BizTalk.Adapter.Common;
using Microsoft.BizTalk.Message.Interop;

namespace BizTalk.Adapters.PowerShellTransmitter
{
    class PowerShellTransmitterProperties : ConfigProperties
    {
        public static int BatchSize { get; } = 20;

        public string Script { get; }
        public string Host { get; }
        public string User { get; }
        public string Password { get; }

        public string Uri { get; set; }
        

        public PowerShellTransmitterProperties(IBaseMessage message, string propertyNamespace)
        {
            //  get the adapter configuration off the message
            IBaseMessageContext context = message.Context;
            string config = (string)context.Read("AdapterConfig", propertyNamespace);

            //  the config can be null all that means is that we are doing a dynamic send
            if (null != config)
            {
                var locationConfigDom = new XmlDocument();
                locationConfigDom.LoadXml(config);

                Uri = Extract(locationConfigDom, "/Config/uri", string.Empty);

                Script = Extract(locationConfigDom, "/Config/script", string.Empty);
                Host = IfExistsExtract(locationConfigDom, "Config/host", string.Empty);
                User = IfExistsExtract(locationConfigDom, "Config/user", string.Empty);
                Password = IfExistsExtract(locationConfigDom, "Config/password", string.Empty);
            }
        }
                
        public void UpdateUriForDynamicSend()
        {
            if (!string.IsNullOrEmpty(Uri))
            {
                // Strip off the adapters alias
                const string adapterAlias = "PowerShell://";
                if (Uri.StartsWith(adapterAlias, StringComparison.OrdinalIgnoreCase))
                {
                    Uri = Uri.Substring(adapterAlias.Length);
                }
            }
        }
    }
}

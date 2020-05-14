using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.BizTalk.Adapter.Framework;

namespace BizTalk.Adapters.PowerShellManagement
{
    public class PowerShellManagement : IStaticAdapterConfig, IAdapterConfigValidation
    {
        public string GetConfigSchema(ConfigType configType)
        {
            switch (configType)
            {
                case ConfigType.TransmitHandler:
                    return GetResource("BizTalk.Adapters.PowerShellManagement.TransmitHandler.xsd");

                case ConfigType.TransmitLocation:
                    return GetResource("BizTalk.Adapters.PowerShellManagement.TransmitLocation.xsd");

                default:
                    return null;
            }
        }

        public Result GetSchema(string uri, string namespaceName, out string fileLocation)
        {
            fileLocation = null;
            return Result.Continue;
        }

        public string[] GetServiceDescription(string[] wsdlReferences)
        {
            string[] result = new string[1];
            result[0] = GetResource("Phuuskon.BizTalk.Adapters.PowerShellManagement.service1.wsdl");
            return result;
        }

        public string GetServiceOrganization(Microsoft.BizTalk.Component.Interop.IPropertyBag endpointConfiguration, string nodeIdentifier)
        {
            string result = GetResource("Phuuskon.BizTalk.Adapters.PowerShellManagement.CategorySchema.xml");
            return result;
        }

        public string ValidateConfiguration(ConfigType configType, string configuration)
        {
            string validXml = String.Empty;

            switch (configType)
            {
                case ConfigType.TransmitHandler:
                    validXml = configuration;
                    break;

                case ConfigType.TransmitLocation:
                    validXml = ValidateTransmitLocation(configuration);
                    break;
            }

            return validXml;
        }

        private string GetResource(string resource)
        {
            string value = null;
            if (null != resource)
            {
                Assembly assem = GetType().Assembly;
                Stream stream = assem.GetManifestResourceStream(resource);
                StreamReader reader;

                using (reader = new StreamReader(stream))
                {
                    value = reader.ReadToEnd();
                }
            }

            return value;
        }

        private string ValidateTransmitLocation(string xmlInstance)
        {
            // Load up document
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlInstance);

            // Build up inner text
            StringBuilder builder = new StringBuilder();

            XmlNode script = document.SelectSingleNode("Config/script");
            if (null != script && 0 < script.InnerText.Length)
            {
                builder.Append(script.InnerText + @"\");
            }
            
            XmlNode uri = document.SelectSingleNode("Config/uri");
            if (null == uri)
            {
                uri = document.CreateElement("uri");
                document.DocumentElement.AppendChild(uri);
            }
            uri.InnerText = "PowerShell://"+builder.ToString();

            return document.OuterXml;
        }
    }
}

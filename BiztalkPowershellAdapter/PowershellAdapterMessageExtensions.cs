using System.IO;
using System.Xml;
using Microsoft.BizTalk.Message.Interop;

namespace BizTalk.Adapters.PowerShellTransmitter
{
    public static class BaseMessageExtensions
    {
        public static XmlDocument GetBody(this IBaseMessage msg)
        {
            XmlDocument xmlDoc = new XmlDocument();
            using (Stream stream = msg.BodyPart.Data)
            {
                XmlReader reader = XmlReader.Create(stream);
                xmlDoc.Load(reader);
            }
            return xmlDoc;
        }       
    }
}

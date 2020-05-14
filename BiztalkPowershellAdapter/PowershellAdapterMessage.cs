using System;
using System.IO;
using System.Xml;
using Microsoft.BizTalk.Message.Interop;

namespace BizTalk.Adapters.PowerShellTransmitter
{
    public class PowerShellAdapterMessage
    {
        private Stream MsgStream { get; set; }
        public string BodyPartName { get; private set; }
        public Guid MessageId { get; private set; }

        public PowerShellAdapterMessage(IBaseMessage pMsg)
        {
            MsgStream = pMsg.BodyPart.Data;
            BodyPartName = pMsg.BodyPartName;
            MessageId = pMsg.MessageID;
        }

        public XmlDocument GetBody()
        {
            XmlDocument xmlDoc = new XmlDocument();
            using (MsgStream)
            {
                using (XmlReader reader = XmlReader.Create(MsgStream))
                {
                    xmlDoc.Load(reader);
                }
            }
            return xmlDoc;
        }
    }
}

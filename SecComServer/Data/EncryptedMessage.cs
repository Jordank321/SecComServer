using System;

namespace SecComServer.Data
{
    public class EncryptedMessage : EncryptedMessageResponse
    {
        public long EncryptedMessageId { get; set; }
        public Conversation Conversation { get; set; }
    }

    public class EncryptedMessageResponse
    {
        internal EncryptedMessageResponse()
        {
        }

        internal EncryptedMessageResponse(EncryptedMessage encryptedMessage)
        {
            FirstUser = encryptedMessage.FirstUser;
            SecondUser = encryptedMessage.SecondUser;
            Sent = encryptedMessage.Sent;
            Content = encryptedMessage.Content;
        }

        public string FirstUser { get; set; }
        public string SecondUser { get; set; }
        public DateTime Sent { get; set; }
        public string Content { get; set; }
    }
}
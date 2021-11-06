using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Networking;

namespace Content
{
    internal class ChatClient
    {
        public int UserId;
        private string moduleIdentifier = "Content";
        private ICommunicator _communicator;
        private ISerializer _serializer;

        public ChatClient()
        {
             _communicator = CommunicationFactory.GetCommunicator();
             _serializer = new Serializer();
        }
        private MessageData SendToMessage(SendMessageData toconvert, MessageEvent ChatEvent)
        {
            MessageData Converted = new MessageData();
            Converted.Event = ChatEvent;
            Converted.Type = toconvert.Type;
            Converted.Message = toconvert.Message;
            Converted.FileData = null;
            Converted.SenderId = UserId;
            Converted.ReceiverIds = toconvert.ReceiverIds;
            Converted.ReplyThreadId = toconvert.ReplyThreadId;
            Converted.Starred = false;
            Converted.SentTime = DateTime.Now;
            return Converted;
        }
        public void ChatNewMessage(SendMessageData toserver)
        {
            MessageData tosend = SendToMessage(toserver, MessageEvent.NewMessage);
            tosend.MessageId = ObjectId.Empty;
            string xml = _serializer.Serialize<MessageData>(tosend);
            _communicator.Send(xml, moduleIdentifier);

        }

        public void ChatUpdate()
        {
            throw new NotImplementedException();
        }

        public void ChatStar(ObjectId messageId)
        {
            MessageData toSend = new MessageData();
            toSend.MessageId = messageId;
            toSend.Event = MessageEvent.Star;
            toSend.SenderId = UserId;

            string xml = _serializer.Serialize<MessageData>(toSend);
            _communicator.Send(xml, moduleIdentifier);
            
        }

       

    }

}
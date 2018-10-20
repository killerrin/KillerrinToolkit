using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KillerrinToolkit.Core.Services
{
    public class CodeNotificationService
    {
        public Dictionary<string, Queue<CodeNotification>> Messages { get; private set; }

        public CodeNotificationService()
        {
            Messages = new Dictionary<string, Queue<CodeNotification>>();
        }

        public int Count()
        {
            int count = 0;
            foreach (var item in Messages)
            {
                count += item.Value.Count;
            }

            return count;
        }
        public int Count(string messageType)
        {
            var count = Messages[messageType]?.Count;
            return count == null ? 0 : count.Value;
        }

        public bool HasMessages() { return Count() > 0; }
        public bool HasMessages(string messageType) { return Count(messageType) > 0; }

        public IEnumerable<CodeNotification> GetNotifications(string messageType) { return Messages[messageType]; }
        public CodeNotification GetById(Guid id)
        {
            foreach (var type in Messages)
            {
                foreach (var message in type.Value)
                {
                    if (message.ID == id) return message;
                }
            }

            return null;
        }

        public CodeNotification Peek(string messageType) { return Messages[messageType]?.Peek(); }
        public CodeNotification Dequeue(string messageType) { return Messages[messageType]?.Dequeue(); }
        public void LogMessage(CodeNotification message)
        {
            var list = Messages[message.MessageType];
            if (list == null)
            {
                list = new Queue<CodeNotification>();
                Messages[message.Message] = list;
            }

            list.Enqueue(message);
        }
    }

    public class CodeNotification
    {
        public Guid ID { get; }
        public string MessageType { get; }
        public string Message { get; }

        public CodeNotification(string messageType, string message)
        {
            ID = Guid.NewGuid();
            MessageType = messageType;
            Message = message;
        }
    }
}

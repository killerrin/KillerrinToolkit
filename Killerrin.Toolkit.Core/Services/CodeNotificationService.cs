using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Killerrin.Toolkit.Core.Services
{
    public class CodeNotificationService
    {
        public Dictionary<string, Queue<CodeNotification>> Messages { get; private set; }

        public CodeNotificationService()
        {
            Messages = new Dictionary<string, Queue<CodeNotification>>();
        }

        /// <summary>
        /// The number of messages in our queue
        /// </summary>
        /// <returns>A count of the messages in our queue</returns>
        public int Count()
        {
            int count = 0;
            foreach (var item in Messages)
            {
                count += item.Value.Count;
            }

            return count;
        }

        /// <summary>
        /// The number of messages in our queue for a given Message Type
        /// </summary>
        /// <param name="messageType">The message type of the queue to count</param>
        /// <returns>A count of the messages in our queue</returns>
        public int Count(string messageType)
        {
            var count = Messages[messageType]?.Count;
            return count == null ? 0 : count.Value;
        }

        /// <summary>
        /// Whether we have any messages
        /// </summary>
        /// <returns>Whether there are any messages in the queues</returns>
        public bool HasMessages() { return Count() > 0; }

        /// <summary>
        /// Whether we have any messages for a given queue
        /// </summary>
        /// <param name="messageType">The message type of the queue</param>
        /// <returns>Whether there are any messages in the queue</returns>
        public bool HasMessages(string messageType) { return Count(messageType) > 0; }

        /// <summary>
        /// Gathers all the notifications for a given queue
        /// </summary>
        /// <param name="messageType">The message type of the queue</param>
        /// <returns>The messages in the queue</returns>
        public IEnumerable<CodeNotification> GetNotifications(string messageType) { return Messages[messageType]; }

        /// <summary>
        /// Gets a specific message by ID
        /// </summary>
        /// <param name="id">The ID of the message to gather</param>
        /// <returns>The specific message</returns>
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

        /// <summary>
        /// Peeks the top message in the queue
        /// </summary>
        /// <param name="messageType">The message type of the queue to search</param>
        /// <returns>The top message</returns>
        public CodeNotification Peek(string messageType) { return Messages[messageType]?.Peek(); }

        /// <summary>
        /// Dequeues the top message from the queue
        /// </summary>
        /// <param name="messageType">The message type of the queue to use</param>
        /// <returns>The top message</returns>
        public CodeNotification Dequeue(string messageType) { return Messages[messageType]?.Dequeue(); }

        /// <summary>
        /// Logs a message into its proper queue
        /// </summary>
        /// <param name="message">The Message to log in the queue</param>
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

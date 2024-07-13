using UnityEngine;

namespace MergeClaw3D.Scripts.Events
{
    public class MessageBase
    {
        public MonoBehaviour sender { get; private set; }
        public int id { get; private set; }
        public object data { get; private set; }

        public MessageBase(MonoBehaviour sender, int id, System.Object data)
        {
            this.sender = sender;
            this.id = id;
            this.data = data;
        }
         
        public static MessageBase Create(MonoBehaviour sender,
            int id, System.Object data)
        {
            return new MessageBase(sender, id, data);
        }
    }
}
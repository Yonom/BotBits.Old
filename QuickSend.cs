using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotBits.SendMessages;
using PlayerIOClient;

namespace BotBits.Old
{
    internal static class QuickSend
    {
        public static void Enable(BotBitsClient client)
        {
            var handle = new SendHandle(client);
            PlaceSendMessage.Of(client).Send += handle.Send;
            SmileySendMessage.Of(client).Send += handle.Send;
            MoveSendMessage.Of(client).Send += handle.Send;
        }

        class SendHandle
        {
            private readonly BotBitsClient _client;

            public SendHandle(BotBitsClient client)
            {
                this._client = client;
            }

            public void Send<T>(object sender, SendQueueEventArgs<T> e) where T : SendMessage<T>
            {
                e.Cancelled = true;

                MessageServices.EnableInstantSend(() =>
                    e.Message.SendIn(this._client));
            } 
        }
    }
}

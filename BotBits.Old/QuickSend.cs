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
            PlaceSendMessage.Of(client).Sending += handle.Sending;
            SmileySendMessage.Of(client).Sending += handle.Sending;
            MoveSendMessage.Of(client).Sending += handle.Sending;
        }

        class SendHandle
        {
            private readonly BotBitsClient _client;

            public SendHandle(BotBitsClient client)
            {
                this._client = client;
            }

            public void Sending<T>(object sender, SendingEventArgs<T> e) where T : SendMessage<T>
            {
                if (!e.Cancelled)
                {
                    e.Cancelled = true;

                    MessageServices.EnableInstantSend(() =>
                        e.Message.SendIn(this._client));
                }
            } 
        }
    }
}

using BotBits.Events;
using BotBits.SendMessages;

namespace BotBits.Old
{
    internal static class QuickSend
    {
        public static void Enable(BotBitsClient client)
        {
            var handle = new SendHandle(client);
            SendingEvent<PlaceSendMessage>.Of(client).Bind(handle.Sending);
            SendingEvent<SmileySendMessage>.Of(client).Bind(handle.Sending);
            SendingEvent<MoveSendMessage>.Of(client).Bind(handle.Sending);
        }

        private class SendHandle
        {
            private readonly BotBitsClient _client;

            public SendHandle(BotBitsClient client)
            {
                this._client = client;
            }

            public void Sending<T>(SendingEvent<T> e) where T : SendMessage<T>
            {
                if (!e.Cancelled)
                {
                    e.Cancelled = true;

                    if (!e.Message.SkipsQueue)
                    {
                        MessageServices.EnableInstantSend(() =>
                            e.Message.SendIn(this._client));
                    }
                }
            }
        }
    }
}
namespace BotBits.Old
{
    public sealed class BotBitsOldExtension : Extension<BotBitsOldExtension>
    {
        public static bool LoadInto(BotBitsClient client)
        {
            return LoadInto(client, null);
        }
    }
}

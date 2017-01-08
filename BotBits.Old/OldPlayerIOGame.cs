namespace BotBits.Old
{
    public sealed class OldPlayerIOGame : IPlayerIOGame<OldLoginClient>
    {
        public OldPlayerIOGame(ILogin<OldLoginClient> login, string gameId)
        {
            this.Login = login;
            this.GameId = gameId;
        }

        public string GameId { get; }
        public ILogin<OldLoginClient> Login { get; }
    }
}
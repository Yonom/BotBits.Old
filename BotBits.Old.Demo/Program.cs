using System;
using System.Threading;
using BotBits.Events;

namespace BotBits.Old.Demo
{
    internal static class Program
    {
        private static readonly BotBitsClient bot = new BotBitsClient();

        private static void Main()
        {
            EventLoader
                .Of(bot)
                .LoadModule(typeof(Program));

            BotBitsOldExtension
                .LoadInto(bot);

            OldLogin
                .Of(bot)
                .ConnectAsync()
                .CreateJoinRoomAsync(0, 0);

            Thread.Sleep(-1);
        }

        [EventListener]
        private static void OnMove(MoveEvent e)
        {
            Console.WriteLine("{0} {1} {2}", e.Player.Username, e.X, e.Y);
        }

        [EventListener]
        private static void OnInit(InitEvent e)
        {
            //Blocks.Of(bot).In(new Rectangle(50, 50, 50, 50)).Set(Foreground.Basic.Blue);
            Blocks.Of(bot).In(new Rectangle(0, 0, 70, 70)).Set(OldBlock.Basic.Green);
        }
    }
}
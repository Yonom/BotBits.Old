﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotBits.Events;

namespace BotBits.Old.Demo
{
    static class Program
    {
        private static BotBitsClient bot = new BotBitsClient();

        static void Main()
        {
            EventLoader
                .Of(bot)
                .LoadModule(typeof(Program));

            BotBitsOldExtension
                .LoadInto(bot);

            OldConnectionManager
                .Of(bot)
                .Connect()
                .CreateJoinRoom(0, 0);
            
            Thread.Sleep(-1);
        }

        [EventListener]
        static void OnMove(MoveEvent e)
        {
            Console.WriteLine("{0} {1} {2}", e.Player.Username, e.X, e.Y);
        }
    }
}
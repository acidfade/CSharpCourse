﻿using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoleRPG.Creatures.Heros;
using ConsoleRPG.Creatures.NPC;

using static ConsoleRPG.Utils.Generator;
using static ConsoleRPG.Utils.GameGraphics;
using static ConsoleRPG.Utils.InputOutput;
using ConsoleRPG.Creature;
using ConsoleRPG.Items;

namespace ConsoleRPG.Engine
{
    internal class GameEngine
    {
        private Player GamePlayer;

        public Monster GetMonster()
        {
            /* This function generate Monster by Player characteristics */
            string name = GenerateName();
            int level = RandomNumber(GamePlayer.Level, GamePlayer.Level + 2);
            int health = RandomNumber(GamePlayer.Health - 100, GamePlayer.Health);
            int max_health = RandomNumber(health, health + 100);
            int armor = RandomNumber(100, 500);
            int energy = RandomNumber(200, 500);
            int strength = RandomNumber(GamePlayer.Strength - 10, GamePlayer.Strength + 10);
            int expireance_points = RandomNumber(10, 200);

            return new Monster(name, level, health, max_health, strength, energy, expireance_points);
        }

        public void CreatePlayer(string name, int type)
        {
            /* This function create Player with some type and name */ 
            switch (type) 
            {
                case 1:
                    GamePlayer = new Barbarian(name);
                    break;
                case 2:
                    GamePlayer = new Tank(name);
                    break;
                case 3:
                    GamePlayer = new Bandit(name);
                    break;
                default: 
                    GamePlayer = null;
                    break;
            }
        }

        private int PlayerHit()
        {
            Console.Clear();

            int hit = RandomNumber(1, 50);
            int direction = 1;

            bool exit = false;

            Task.Factory.StartNew(() =>
            {
                while (Console.ReadKey().Key != ConsoleKey.Spacebar);
                exit = true;
            });

            int pause = 10;

            DrawHitBar(100);

            while (!exit)
            {
                DrawHitLine(hit);

                hit += direction;

                if (hit == 51 || hit == 0)
                    direction *= -1;

                Thread.Sleep(pause);
            }

            if (hit > 25)
                hit = 50 - hit;

            Console.Clear();
            return hit * 2;
        }

        private BodyPart ChoseBodyPart()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Print("Натисніть відповідну клавішу що вдарити монстра по відповідній частині тіла\n",AlignPrint.Center);
            Print("[H] Head\t[B] Body\t[L] Legs\t[F] Feet\n", AlignPrint.Center);
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.H:
                    return BodyPart.Head;
                case ConsoleKey.B:
                    return BodyPart.Body;
                case ConsoleKey.L:
                    return BodyPart.Legs;
                case ConsoleKey.F:
                    return BodyPart.Feet;
                default:
                    return BodyPart.Body;
            }
        }

        public int Battle(Monster monster)
        {
            Console.Clear();

            int player_hit;
            int monster_hit;
            int random;
            BodyPart part;

            ShowPlayerInfo(GamePlayer);
            ShowMonsterInfo(monster);

            PrintByCords("Натисніть будь-яку клавішу щоб продовжити...", (Console.WindowWidth - 40) / 2, Console.WindowHeight - 1);
            Console.ReadKey();

            while (GamePlayer.Health != 0 && monster.Health != 0)
            {   
                ShowPlayerInfo(GamePlayer);
                ShowMonsterInfo(monster);

                part = ChoseBodyPart();
                random = PlayerHit();
                player_hit = GamePlayer.HitMonster(monster, part, random);
                monster_hit = monster.HitPlayer(GamePlayer, (BodyPart)RandomNumber(0, 3));

                ShowPlayerInfo(GamePlayer);
                ShowMonsterInfo(monster);
                PrintByCords($"-{player_hit}", Console.WindowWidth * 2/3, 6, ConsoleColor.Cyan);
                PrintByCords($"-{monster_hit}", 15, 0, ConsoleColor.Red);

                PrintByCords("Натисніть будь-яку клавішу щоб продовжити...", (Console.WindowWidth - 40) / 2, Console.WindowHeight-1);
                Console.ReadKey();

                Console.Clear();
            }

            if (GamePlayer.Health == 0)
                return 0;
            else
                return 1;
        }
    }
}

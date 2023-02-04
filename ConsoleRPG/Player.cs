﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    internal class Player : Creature
    {
        public string Type;
        public Characteristic Agility = new Characteristic();
        public Characteristic Endurance = new Characteristic();

        public void DisplayCreature()
        {
            Console.WriteLine($"[Name] :: {Name}");
            Console.WriteLine($"[Type] :: {Type}");
            Console.WriteLine($"[Level] :: {Level.Value}");
            Console.WriteLine($"[Health] :: {Health.Value}");
            Console.WriteLine($"[Energy] :: {Energy.Value}");
            Console.WriteLine($"[Strength] :: {Strength.Value}");
            Console.WriteLine($"[Agility] :: {Agility.Value}");
            Console.WriteLine($"[Endurance] :: {Endurance.Value}");
            Console.WriteLine($"[Experience points] :: {ExperiencePoints.Value}");
        }

        private string[] AviableTypes = { "Barbarian", "Bandit", "Tank" };
        public Player(string name, int type, int agility = 100, int endurance = 100) : base (name) 
        {
            Type = AviableTypes[type];
            Agility.Set(agility);
            Endurance.Set(endurance);
        }
    }
}

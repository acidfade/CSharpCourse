﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Creatures.Heros
{
    internal class Barbarian : Player
    {
        public Barbarian(string name) : base(name)
        {
            SetStrength(80);
        }
    }
}
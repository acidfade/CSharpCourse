﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Creatures.Heros
{
    internal class Tank : Player
    {
        public Tank(string name) : base(name)
        {
            SetEndurance(150);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    abstract class Enemy : Entity
    {
        public int HealthPoints { get; set; }
        public int AttackPower { get; protected set; }


        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMUSS
{
    static public class SpriteAnimation
    {
        static public Dictionary<Animation, int>AnimationFrames { get; set; }
        public enum Facing
        {
            Up,
            Right,
            Down,
            Left
        };

        public enum Animation
        {
            Still,
            RunningLeft,
            RunningRight,
            RunningUp,
            RunningDown
        };
    }
}

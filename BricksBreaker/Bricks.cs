using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksBreaker
{
    class Bricks
    {
        public int bricksizeX { get; set; }
        public int bricksizeY { get; set; }
        public int brickXposition { get; set; }
        public int brickYposition { get; set; }
        public bool visiblity { get; set; }
        public int bricksGap { get; set; }
        public Bricks()
        {
            bricksizeX = 40;
            bricksizeY = 20;
            bricksGap = 50;
            visiblity = true;
        }
    }
}

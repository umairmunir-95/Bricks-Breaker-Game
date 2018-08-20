using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksBreaker
{
    class Slide
    {
        public int slideX { get; set; }
        public int slideY { get; set; }
        public int slideXPosition { get; set; }
        public int slideYPosition { get; set; }

        public Slide()
        {
            //Size
            slideX = 60;
            slideY = 7;
            //position of ball in picturebox
            slideXPosition = 150;
            slideYPosition = 380;
        }
    }
}

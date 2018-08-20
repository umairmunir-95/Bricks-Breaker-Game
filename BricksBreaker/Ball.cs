using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksBreaker
{
    class Ball
    {
        public int ballX { get; set; }
        public int ballY { get; set; }
        public int ballXPosition { get; set; }
        public int ballYPosition { get; set; }
        public int NextSide { get; set; }
        public int PreviousSide { get; set; }

        public Ball()
        {
            //ball size
            ballX = 10;
            ballY = 10;

            //position of ball in picturebox
            ballXPosition = 170;
            ballYPosition = 370;

            //stages
            NextSide = 1;
            PreviousSide = 0;
        }
    }
}

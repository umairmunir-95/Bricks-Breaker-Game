using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksBreaker
{
    class Settings
    {
        public static int ball_speed { get; set; }
        public static int slide_speed { get; set; }
        public static int score { get; set; }
        public static int point { get; set; }
        public static bool GameOver { get; set; }
        public static int angle { get; set; }
        public static int BonusPoint { get; set; }

        public Settings()
        {
            ball_speed = 5;
            slide_speed = 30;
            score = 0;
            point = 100;
            GameOver = false;
            angle = 45;
            BonusPoint = 200;
        }

    }
}

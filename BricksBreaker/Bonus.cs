using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksBreaker
{
    class Bonus
    {
        public int BonusSizeX;
        public int BonusSizeY;
        public int BonusPositionX;
        public int BonusPositionY;
        public int BonusMove;
        public int BonusBrick;
        public bool BonusVisibility;

        public Bonus()
        {
            Random random = new Random();
            BonusSizeX = 10;
            BonusSizeY = 15;
            BonusMove = 3;
            BonusBrick = random.Next(0, 15);
            BonusVisibility = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
   static class Config
    {
        const int speed = 300;
        const double numOfPositionsX = 20;
        const double numOfPositionsY = 20;
        const int numOfBlocks = 4;

        
public static int Speed => speed;

        public static double NumOfPositionsX => numOfPositionsX;

        public static double NumOfPositionsY => numOfPositionsY;

        public static int NumOfBlocks => numOfBlocks;
    }
}

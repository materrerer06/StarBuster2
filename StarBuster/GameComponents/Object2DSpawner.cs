using StarBuster.Objects2D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBuster.GameComponents
{
    public static class Object2DSpawner
    {
        public static void Update(int aFrameIndex)
        {
            if(aFrameIndex % 50 == 30)
            {
                GameManager.Instance.AddObject2D(new Enemy(1200, 100));
                GameManager.Instance.AddObject2D(new Enemy(1200, 300));
            }
            //do zmiany

            if(aFrameIndex == 1000)
            {
                GameManager.Instance.AddObject2D(new Boss(1200, 400));
            }
        }
    }
}

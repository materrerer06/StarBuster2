using StarBuster.Objects2D;
using System;
using System.Collections.Generic;
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
                GameManager.Instance.AddObject2D(new Enemy(1200, 600));
            }
            if(aFrameIndex % 200 == 0)
            {
                GameManager.Instance.AddObject2D(new Boss(600, 100));
            }
        }
    }
}

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
                GameManager.Instance.AddObject2D(new Enemy(700, 200));
            }
        }
    }
}

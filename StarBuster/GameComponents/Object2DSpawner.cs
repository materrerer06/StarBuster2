using StarBuster.Objects2D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StarBuster.GameComponents
{
    public class Object2DSpawner
    {
        private static int spawntime = 100;

        public static int SpawnTime
        {
            get { return spawntime; }
            set { spawntime = value; }
        }
        public static void Update(int aFrameIndex)
        {
            if(aFrameIndex % spawntime == 10)
            {
                GameManager.Instance.AddObject2D(new Enemy(1200, 100));
                GameManager.Instance.AddObject2D(new Enemy(1200, 300));
            }
            //do zmiany

            if(aFrameIndex % 1000 == 0)
            {
                GameManager.Instance.AddObject2D(new Boss(1200, 400));
            }
        }
    }
}

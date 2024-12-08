using StarBuster.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBuster.Objects2D
{
    public class BossExplosion : Object2D
    {
        private int _radius;
        private int _duration;
        private double _angle;

        public BossExplosion(int x, int y, int radius = 10, int duration = 20) : base(x, y)
        {
            _angle = 0;
            _radius = radius;
            _duration = duration;
        }

        public override void Render(Graphics g)
        {
            if (_duration > 0)
            {
                g.FillEllipse(Brushes.Orange, x - _radius, y - _radius, _radius * 2, _radius * 2);

                Random rand = new Random();

                for (int i = 0; i < 10; i++)
                {
                    double angle = rand.NextDouble() * 2 * Math.PI;
                    double distance = rand.NextDouble() * _radius;

                    double px = x + distance * Math.Cos(angle);
                    double py = y + distance * Math.Sin(angle);

                    g.FillEllipse(Brushes.Yellow, (int)px - 10, (int)py - 10, 20, 20);
                    g.FillEllipse(Brushes.Red, (int)px - 8, (int)py - 8, 16, 16);
                }

                _duration--;
            }
        }

        public override void Update()
        {
            _angle += 0.2;
            _radius += 1;

            y += 2;
            x -= 2;

            if (_duration <= 0)
            {
                // Assuming GameManager.Instance.Remove() is part of the framework
                GameManager.Instance.Remove(this);
            }
        }
    }
}

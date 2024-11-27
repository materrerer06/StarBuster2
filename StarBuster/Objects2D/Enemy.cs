using StarBuster.GameComponents;

namespace StarBuster.Objects2D
{
    public class Enemy : Object2D
    {
        int dir = 2;
        double _angle;

        public Enemy(int x, int y) : base(x, y)
        {
            _hw = 25;
            _hh = 25;

            _angle = 0;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Red, new Rectangle(x - 20, y - 20, 40, 40));

            for (int i = 0; i < 5; i++)
            {
                double a = 2.0 * Math.PI * i / 5.0 + _angle;
                double px = x + 20.0 * Math.Cos(a);
                double py = y + 20.0 * Math.Sin(a);

                g.FillEllipse(Brushes.Brown, new Rectangle((int)px - 10, (int)py - 10, 20, 20));
            }
        }

        public override void Update()
        {
            _angle += 0.2;

            if (y < 10 || y > GameManager.Instance.Height - 10) dir = -dir;

            x -= 4;
            y += dir;
        }
    }
}

namespace StarBuster.Objects2D
{
    public class BossBullet : Object2D
    {
        private int _speed = 20;  // Prędkość pocisku

        public BossBullet(int x, int y) : base(x, y)
        {
            _hw = 8;
            _hh = 2;

        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(Brushes.Red, new Rectangle(x - 8, y - 1, 12, 6));
        }

        public override void Update()
        {
            x -= _speed;
        }
    }
}

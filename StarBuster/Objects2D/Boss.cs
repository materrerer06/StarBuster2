

using StarBuster.GameComponents;

namespace StarBuster.Objects2D
{
    public class Boss : Object2D
    {
        private int _health;
        private int _shootDealy;
        private int _helperDealey;
        int dir = 12;


        public Boss(int x, int y) : base(x, y)
        {
            _health = 100;
            _shootDealy = 0;
            _helperDealey = 0;

            _hw = 40;
            _hh = 65;
        }
        public int Health
        {
            get { return _health; }
            set { _health = Health; }
        }

        public override void Render(Graphics g)
        {
            DrawBoss(g);
            DrawEnergy(g);
        }

        private void DrawBoss(Graphics g)
        {
            g.FillRectangle(Brushes.DarkSlateGray, new Rectangle(x - 50, y - 50, 100, 120));  

            g.FillRectangle(Brushes.DarkOliveGreen, new Rectangle(x - 70, y - 30, 40, 30));  
            g.FillRectangle(Brushes.DarkOliveGreen, new Rectangle(x + 30, y - 30, 40, 30));

            g.FillRectangle(Brushes.DarkOliveGreen, new Rectangle(x - 70, y + 30, 40, 30));
            g.FillRectangle(Brushes.DarkOliveGreen, new Rectangle(x + 30, y + 30, 40, 30));

            g.FillRectangle(Brushes.DarkSlateGray, new Rectangle(x - 20, y + 50, 40, 20)); 
            g.FillRectangle(Brushes.DarkSlateGray, new Rectangle(x + 10, y + 50, 40, 20));

            g.FillRectangle(Brushes.Orange, new Rectangle(x - 50, y, 40, 30));
        }

        private void DrawEnergy(Graphics g)
        {
            int energy_width = _health * 3;

            g.DrawRectangle(Pens.White, 800, 19, 302, 32);
            g.FillRectangle(Brushes.Yellow, 801, 20, energy_width, 30);
        }

        public override void Update()
        {
            HandleShooting();
            HandleMovement();
            HandleHelpers();
        }
        public void ChangeEnergy(int aValue)
        {
            _health += aValue;
        }
        public void Hitted(int aValue)
        {
            _health -= aValue;
        }
        private void HandleHelpers()
        {

        }

        private void HandleMovement()
        {
/*            if (y < 10 || y > GameManager.Instance.Height - 50) dir = -dir;

            y += dir;*/
        }

        private void HandleShooting()
        {

        }
    }
}

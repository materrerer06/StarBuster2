

namespace StarBuster.Objects2D
{
    public class Boss : Object2D
    {
        private int _energy;
        private int _shootDealy;
        private int _helperDealey;

        public Boss(int x, int y) : base(x, y)
        {
            _energy = 100;
            _shootDealy = 0;
            _helperDealey = 0;
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
            int energy_width = _energy * 3;

            g.DrawRectangle(Pens.White, 800, 19, 302, 32);
            g.FillRectangle(Brushes.Yellow, 801, 20, energy_width, 30);
        }

        public override void Update()
        {
            HandleShooting();
            HandleMovement();
            HandleHelpers();
        }

        private void HandleHelpers()
        {

        }

        private void HandleMovement()
        {

        }

        private void HandleShooting()
        {

        }
    }
}

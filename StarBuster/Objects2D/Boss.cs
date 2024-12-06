

using StarBuster.GameComponents;
using System.Diagnostics;

namespace StarBuster.Objects2D
{
    public class Boss : Object2D
    {
        private int _health;
        private int _shootDelay;
        private float _timeSinceLastShoot;
        int FrameIndex;
        private int _helperDealey;
        private float _timeSinceLastSpawn;
        int dir = 5;


        public Boss(int x, int y) : base(x, y)
        {
            _health = 100;

            _helperDealey = 300;
            _timeSinceLastSpawn = 0f;

            _shootDelay = 60;
            _timeSinceLastShoot = 0f;

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
            HandleMovement(FrameIndex++);
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
            _timeSinceLastSpawn += GameManager.Instance.FrameIndex / 60f;

            if (_timeSinceLastSpawn >= _helperDealey)
            {
                GameManager.Instance.AddObject2D(new Enemy(x, y-10));
                _timeSinceLastSpawn = 0f;
            }
        }

        private void HandleMovement(int bFrameIndex)
        {
            if (bFrameIndex >= 0 && bFrameIndex < 50)
            {
                x -= 8;
            }
            if (bFrameIndex >= 50 && bFrameIndex <90)
            {
                x -= 0;
            }
            if (bFrameIndex >= 90)
            {
                x -= 0;
                if (y < 10 || y > GameManager.Instance.Height - 10) dir = -dir;
                y += dir;
            }
            Debug.WriteLine(bFrameIndex);
        }

        private void HandleShooting()
        {
            // Zwiększamy czas od ostatniego strzału
            _timeSinceLastShoot += GameManager.Instance.FrameIndex / 60f;  // Czas w sekundach

            // Sprawdzamy, czy minął czas, by boss mógł oddać kolejny strzał
            if (_timeSinceLastShoot >= _shootDelay)
            {
                // Tworzymy pocisk z pozycji Bossa
                var bossbullet = new BossBullet(x, y);  // Zmieniamy na odpowiednią klasę Bullet
                GameManager.Instance.AddObject2D(bossbullet);  // Dodajemy pocisk do gry

                _timeSinceLastShoot = 0f;  // Resetujemy licznik czasu od ostatniego strzału
            }
        }
    }
}

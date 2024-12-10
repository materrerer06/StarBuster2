using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StarBuster.GameComponents;

namespace StarBuster.Objects2D
{
    public class Hero : Object2D
    {
        private int _ileDoStrzalu;
        private int _energy;

        public int Energy
        {
            get { return _energy; }
            set { _energy = Energy; }
        }
        public int ShootDelay
        {
            get { return _ileDoStrzalu; }
            set { _ileDoStrzalu = value; }
        }

        public Hero(int x, int y) : base(x, y)
        {
            _hw = 20;
            _hh = 10;

            _energy = 100;
            _ileDoStrzalu = 10;
        }

        public override void Render(Graphics g)
        {
            DrawHero(g);
            DrawFireEffect(g);
            DrawEnergy(g);

        }

        private void DrawEnergy(Graphics g)
        {
            int energy_width = _energy * 3;

            g.DrawRectangle(Pens.White, 29, 19, 302, 32);
            g.FillRectangle(Brushes.Red, 30, 20, energy_width, 30);
        }

        public override void Update()
        {
            HandleMovement();
            HandleShooting();
            ApplyMovementLimits();
        }

        public void ChangeEnergy(int aValue)
        {
            _energy += aValue;
        }
        public void Hitted(int aValue)
        {
            _energy -= aValue;
        }
        private void DrawHero(Graphics g)
        {
            g.FillEllipse(Brushes.DodgerBlue, new Rectangle(x - 30, y - 7, 60, 14));
            g.FillEllipse(Brushes.DodgerBlue, new Rectangle(x - 20, y - 20, 12, 40));
            g.FillEllipse(Brushes.White, new Rectangle(x - 5, y - 4, 10, 8));
        }

        private void DrawFireEffect(Graphics g)
        {
            for (int i = 0; i < 10; i++)
            {
                var px = x - _r.Next() % 20 + 4;
                var py = y + _r.Next() % 7 - 3;
                Point pt1 = new Point(x - 26, py);
                Point pt2 = new Point(px - 26, py);

                g.DrawLine(Pens.Orange, pt1, pt2);
            }
        }

        private void HandleMovement()
        {
            var keyboard = GameManager.Instance.KeySet;

            if (keyboard.Contains(Keys.Up)) y -= 5;
            else if (keyboard.Contains(Keys.Down)) y += 5;

            if (keyboard.Contains(Keys.Left)) x -= 5;
            else if (keyboard.Contains(Keys.Right)) x += 5;
        }

        private void HandleShooting()
        {
            var gm = GameManager.Instance;

            if (gm.KeySet.Contains(Keys.Space) && _ileDoStrzalu == 0)
            {
                gm.AddObject2D(new Bullet(x + 20, y));
            }

            if (_ileDoStrzalu > 0) _ileDoStrzalu--;
        }

        private void ApplyMovementLimits()
        {
            var gm = GameManager.Instance;

            if (x < 10) x = 10;
            else if (x > gm.Width / 4) x = gm.Width / 4;

            if (y < 10) y = 10;
            else if (y > gm.Height - 10) y = gm.Height - 10;
        }
    }
}

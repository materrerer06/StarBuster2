using StarBuster.Objects2D;
using StarBuster.Types;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StarBuster.GameComponents
{
    public class GameManager
    {
        public int Width;           // szerokość ekranu gry
        public int Height;          // wysokość ekranu gry
        public HashSet<Keys> KeySet;  // zbiór wciśniętych klawiszy
        public int FrameIndex;
        public GameState State;

        private List<Object2D> _objects;    // lista aktywnych obiektów w grze
        private List<Object2D> _toAdd;      // lista obiektów do dodania w następnej klatce
        private List<Object2D> _toRemove;   // lista obiektów do usunięcia w następnej klatce
        private CollisionDetector _detector; // obiekt do wykrywania kolizji
        private CollisionSolver _solver;     // obiekt do rozwiązywania kolizji
        private int _explosionTimer = 0;
        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance => _instance;

        public int ObjectCount => _objects.Count;

        private GameManager()
        {
            _objects = new List<Object2D>
            {
                new StarField(200, 1200, 800),
                new Hero(100, 100)
            };

            _toAdd = new List<Object2D>();
            _toRemove = new List<Object2D>();

            KeySet = new HashSet<Keys>();

            _detector = new CollisionDetector(_objects);
            _solver = new CollisionSolver();
            FrameIndex = 0;
            State = GameState.TitleScreen;
        }

        public void AddObject2D(Object2D obj)
        {
            _toAdd.Add(obj);
        }

        public void Remove(Object2D obj)
        {
            _toRemove.Add(obj);
        }

        public void Render(Graphics g)
        {
            switch (State)
            {
                case GameState.TitleScreen:
                    RenderTitleScreen(g);
                    break;
                case GameState.GamePlay:
                    RenderGamePlay(g);
                    break;
                case GameState.About:
                    RenderAbout(g);
                    break;
                case GameState.Options:
                    RenderOptions(g);
                    break;
                case GameState.GameOver:
                    RenderGameOver(g);
                    break;
                case GameState.EndGame:
                    RenderEndGame(g);
                    break;
            }
        }
        private void RenderEndGame(Graphics g)
        {
            _objects[0].Render(g);
            string endText = "You WON!!!";
            Font titleFont = new Font("Arial", 36);
            SolidBrush titleBrush = new SolidBrush(Color.White);
            g.DrawString(endText, titleFont, titleBrush, 400, 50);

            if (FrameIndex % 50 < 20)
            {
                string pressKeyText = "Press Space to Play Again";
                Font pressKeyFont = new Font("Arial", 16);
                SolidBrush pressKeyBrush = new SolidBrush(Color.Red);
                g.DrawString(pressKeyText, pressKeyFont, pressKeyBrush, 500, 450);
            }
        }

        private void UEndGame()
        {
            if (KeySet.Contains(Keys.Space))
            {
                RestartGame();
            }
            else if (KeySet.Contains(Keys.Left))
            {
                State = GameState.TitleScreen;
            }
        }

        private void RenderTitleScreen(Graphics g)
        {
            _objects[0].Render(g);
            string titleText = "StarBuster ~Beta~";
            Font titleFont = new Font("Arial", 36);
            SolidBrush titleBrush = new SolidBrush(Color.White);
            g.DrawString(titleText, titleFont, titleBrush, 400, 50);

            string optionsText = "Press O for Options";
            string aboutText = "Press A for Options";
            string ExitText = "Press Q for Quit";

            Font optionsFont = new Font("Arial", 16);
            g.DrawString(optionsText, optionsFont, titleBrush, 100, 600);
            g.DrawString(aboutText, optionsFont, titleBrush, 100, 630);
            g.DrawString(ExitText, optionsFont, titleBrush, 100, 660);

            if (FrameIndex % 50 < 20)
            {
                string pressKeyText = "Press Space to Start";
                Font pressKeyFont = new Font("Arial", 16);
                SolidBrush pressKeyBrush = new SolidBrush(Color.Red);
                g.DrawString(pressKeyText, pressKeyFont, pressKeyBrush, 500, 450);
            }
        }

        private void RenderGamePlay(Graphics g)
        {
            foreach (Object2D obj in _objects)
            {
                obj.Render(g);
            }
        }

        private void RenderAbout(Graphics g)
        {
            string aboutText = "About StarBuster Game: ";
            string descText = "Game by: Mateusz Cembala \n Version: v.1.01";
            Font aboutFont = new Font("Arial", 16);
            SolidBrush aboutBrush = new SolidBrush(Color.White);
            g.DrawString(aboutText, aboutFont, aboutBrush, 150, 50);
            g.DrawString(descText, aboutFont, aboutBrush, 150, 100);
        }

        private void Btn_Click(object sender, MouseEventArgs e, Hero hero)
        {
            Rectangle buttonRectangle = new Rectangle(150, 150, 200, 50);

            if (buttonRectangle.Contains(e.Location))
            {
                hero.Energy = hero.Energy - 20;
                MessageBox.Show("Health adjustment option clicked!");
            }
        }

        private void RenderOptions(Graphics g)
        {
            var hero = _objects.OfType<Hero>().FirstOrDefault();
            string optionsText = "Options: Customize your game...";
            Font optionsFont = new Font("Arial", 16);
            SolidBrush optionsBrush = new SolidBrush(Color.White);
            g.DrawString(optionsText, optionsFont, optionsBrush, 150, 50);


            int currentSpawnTime = Object2DSpawner.SpawnTime;
            int spawninterval = currentSpawnTime / 60;
            int health = hero.Energy;
            int shootdelay = hero.ShootDelay;
            //lewo prawo
            string spawn = "Kliknij LEFT lub RIGHT, aby zmienic spawn interval";
            g.DrawString(spawn, optionsFont, optionsBrush, 150, 500);
            string SpawnSeconds = "Spawn interval: "+currentSpawnTime.ToString();
            g.DrawString(SpawnSeconds, optionsFont, optionsBrush, 150, 520);
            //pg up lub pg down
            string HP = "Kliknij PG UP lub PG DOWN, aby zmienic zycie hero";
            g.DrawString(HP, optionsFont, optionsBrush, 150, 570);
            string HeroHP = "Zycie hero: "+health.ToString();
            g.DrawString(HeroHP, optionsFont, optionsBrush, 150, 590);
            //w i s
            string Shoot = "kliknij W lub S, aby zmienic predkosc strzalu hero";
            g.DrawString(Shoot, optionsFont, optionsBrush, 150, 640);
            string ShootDelay = "Prekosc Strzalu: " + shootdelay.ToString();
            g.DrawString(ShootDelay, optionsFont, optionsBrush, 150, 660);

        }


        private void RenderGameOver(Graphics g)
        {
            _objects.Clear();
            if (FrameIndex % 50 < 20)
            {
                string gameOverText = "Game Over!";
                Font gameOverFont = new Font("Arial", 120);
                SolidBrush gameOverBrush = new SolidBrush(Color.Red);
                g.DrawString(gameOverText, gameOverFont, gameOverBrush, 150, 50);
            }
            string restartText = "Press SPACE to restart the game!";
            Font restgameFont = new Font("Arial", 30);
            SolidBrush restartGameBrush = new SolidBrush(Color.White);
            g.DrawString(restartText, restgameFont, restartGameBrush, 350, 250);
        }

        public void Update()
        {
            switch (State)
            {
                case GameState.TitleScreen:
                    UpdateTitleScreen();
                    break;
                case GameState.GamePlay:
                    UpdateGamePlay();
                    break;
                case GameState.About:
                    UAbout();
                    break;
                case GameState.Options:
                    UOptions();
                    break;
                case GameState.GameOver:
                    UGameOver();
                    break;
                case GameState.EndGame:
                    UEndGame();
                    break;
            }
            FrameIndex++;
        }

        private void UpdateTitleScreen()
        {
            _objects[0].Update();
            if (KeySet.Contains(Keys.Space))
            {
                State = GameState.GamePlay;
            }
            else if (KeySet.Contains(Keys.A))
            {
                State = GameState.About;
            }
            else if (KeySet.Contains(Keys.O))
            {
                State = GameState.Options;
            }
            if (KeySet.Contains(Keys.Q))
            {
                Application.Exit();
            }
        }

        public void UpdateGamePlay()
        {
            if (KeySet.Contains(Keys.Escape))
            {
                _objects.RemoveAll(obj => _toRemove.Contains(obj));

                State = GameState.TitleScreen;
            }
                // Aktualizacja obiektów
            foreach (Object2D obj in _objects)
            {
                obj.Update();
            }

            // Sprawdzenie wykrytych kolizji
            var collisions = _detector.DetectCollisions();
            _solver.ResolveCollisions(collisions);

            // Dodanie nowych obiektów
            _objects.AddRange(_toAdd);
            _toAdd.Clear();

            // Sprawdzanie, które obiekty wychodzą poza ekran
            foreach (var obj in _objects)
            {
                if (obj.IsOutOfScreen(Width, Height) && obj is not Hero)
                {
                    _toRemove.Add(obj);
                }
            }
            // Usuwanie obiektów
            _objects.RemoveAll(obj => _toRemove.Contains(obj));
            _toRemove.Clear();
            // Sprawdzanie stanu bohatera
            var hero = _objects.OfType<Hero>().FirstOrDefault();
            if (hero != null && hero.Energy <= 0)
            {
                State = GameState.GameOver;
            }

            // Sprawdzanie stanu bossa
            var boss = _objects.OfType<Boss>().FirstOrDefault();
            if (boss != null && boss.Health <= 0)
            {
                // Jeżeli eksplozja jeszcze nie została zainicjowana
                if (_explosionTimer == 0)
                {
                    // Dodanie eksplozji bossa
                    var explosion = new BossExplosion(boss.x, boss.y, radius: 50, duration: 300);
                    _toAdd.Add(explosion);
                }
                boss.Movement = false;
                boss.Shoot = 524353450;

                _explosionTimer++;

                if (_explosionTimer >= 100)
                {
                    State = GameState.EndGame;
                }
            }
            else
            {
                _explosionTimer = 0;
            }

            // Spawnowanie obiektów
            Object2DSpawner.Update(FrameIndex);
            Debug.WriteLine(FrameIndex);
        }

        private void UAbout()
        {
            if (KeySet.Contains(Keys.Escape))
            {
                State = GameState.TitleScreen;
            }
        }

        private void UOptions()
        {
            var enemy = _objects.OfType<Enemy>().FirstOrDefault();
            var hero = _objects.OfType<Hero>().FirstOrDefault();
            if (KeySet.Contains(Keys.Escape))
            {
                State = GameState.TitleScreen;
            }
            if(Object2DSpawner.SpawnTime < 200 && Object2DSpawner.SpawnTime > 20)
            {
            if(KeySet.Contains(Keys.Right))
            {
                    Object2DSpawner.SpawnTime += 10;
            }
            if (KeySet.Contains(Keys.Left))
            {
                    Object2DSpawner.SpawnTime -= 10;
            }
            }
            if(hero.Energy >= 0 ||  hero.Energy <= 450) { 
            if (KeySet.Contains(Keys.PageUp))
            {
                hero.ChangeEnergy(10);
            }
            if (KeySet.Contains(Keys.PageDown))
            {
                hero.ChangeEnergy(-10);
            }
            }
            if(hero.ShootDelay >= 1 && hero.ShootDelay <= 20)
            {

                if (KeySet.Contains(Keys.W))
                {
                    hero.ShootDelay += 1;
                }
                if (KeySet.Contains(Keys.S))
                {
                    hero.ShootDelay -= 1;
                }
            }
            //hp-10 lub + 10
            //enemy spawn delay
            //shoot delay
        }

        private void UGameOver()
        {
            if (KeySet.Contains(Keys.Space))
            {
                RestartGame();
            }
        }

        public void SetResolution(int aWidth, int aHeight)
        {
            Width = aWidth;
            Height = aHeight;
        }

        private void RestartGame()
        {
            _objects.Clear();
            _objects.Add(new StarField(200, 1200, 800));
            _objects.Add(new Hero(100, 100));

            State = GameState.GamePlay;
        }
    }
}

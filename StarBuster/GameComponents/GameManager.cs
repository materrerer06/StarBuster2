using StarBuster.Objects2D;
using StarBuster.Types;

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

        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance => _instance;

        public int ObjectCount => _objects.Count;

        private GameManager()
        {
            _objects = new List<Object2D>
            {
                new StarField(200, 1200, 800),
                new Hero(100, 100),
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
                    foreach (Object2D obj in _objects) obj.Render(g);
                    break;
                case GameState.About:
                    break;
                case GameState.Options:
                    break;
                case GameState.GameOver:
                    break;
            }
        }
        private void RenderAbout(Graphics g)
        {

        }
        private void RenderTitleScreen(Graphics g)
        {
            _objects[0].Render(g);
            String drawString = "StarBuster ~Beta~";
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            g.DrawString(drawString, drawFont, drawBrush, 150,50);

            if(FrameIndex % 50 < 20)
            {
            String drawString2 = "Press Key";
            Font drawFont2 = new Font("Arial", 16);
            SolidBrush drawBrush2 = new SolidBrush(Color.Red);
            g.DrawString(drawString2, drawFont2, drawBrush2, 150, 450);
            }
        }

        public void Update()
        {
            switch (State)
            {
                case GameState.TitleScreen:
                    _objects[0].Update();
                    if (KeySet.Contains(Keys.Space))
                    {
                        State = GameState.GamePlay;
                    }
                    break;
                case GameState.GamePlay:
                    UpdateGameplay();
                    break;
                case GameState.About:

                    break;
                case GameState.Options:
                    break;
                case GameState.GameOver:
                    break;
            }
            FrameIndex++;
        }
        public void UpdateGameplay()
        {
            // Aktualizacja pozycji i stanu każdego obiektu
            foreach (Object2D obj in _objects) obj.Update();

            // Wykrywanie kolizji i ich obsługa
            var collisions = _detector.DetectCollisions();
            _solver.ResolveCollisions(collisions);

            // Dodanie obiektów z kolejki do listy aktywnych obiektów
            _objects.AddRange(_toAdd);
            _toAdd.Clear();


            // Usunięcie obiektów, które opuściły ekran, oprócz Hero
            foreach (var obj in _objects)
            {
                if (obj.IsOutOfScreen(Width, Height) && obj is not Hero)
                {
                    _toRemove.Add(obj);
                }
            }

            // Usunięcie obiektów z listy aktywnych obiektów
            _objects.RemoveAll(obj => _toRemove.Contains(obj));
            _toRemove.Clear();

            Object2DSpawner.Update(FrameIndex++);
        }

        public void SetResolution(int aWidth, int aHeight)
        {
            Width = aWidth;
            Height = aHeight;
        }
    }
}

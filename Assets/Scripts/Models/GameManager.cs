using System.Collections.ObjectModel;

namespace Models {
    public class GameManager {
        public static GameManager Instance { get; } = new();
        private int TotalEnemies { get; set; }
        private int EnemiesBumped { get; set; }
        public bool IsOver { get; private set; }

        private Collection<Hero> _heroes = new();
        
        private Hero MainHero { get; set; }

        public delegate void TotalEnemiesUpdateHandler(int value);

        public event TotalEnemiesUpdateHandler TotalEnemiesUpdate;

        public delegate void EnemiesBumpedUpdateHandler(int value);

        public event EnemiesBumpedUpdateHandler EnemiesBumpedUpdate;

        public delegate void GameOverUpdateHandler(int? score, int points);

        public event GameOverUpdateHandler GameOverUpdate;

        private GameManager() {
            Reset();
        }

        public void Reset() {
            TotalEnemies = 0;
            EnemiesBumped = 0;
            IsOver = false;

            TotalEnemiesUpdate = null;
            EnemiesBumpedUpdate = null;
            GameOverUpdate = null;

            _heroes.Clear();
            MainHero = null;
        }

        public void EndGame() {
            IsOver = true;

            GameOverUpdate?.Invoke(CalculateScore(), MainHero.Points);
        }

        public void EnemyWasCreated() {
            TotalEnemiesUpdate?.Invoke(TotalEnemies++);
        }

        public void EnemyWasBumped() {
            EnemiesBumpedUpdate?.Invoke(EnemiesBumped++);
        }

        public void AddHero(Hero hero) {
            MainHero = hero;
            _heroes.Add(hero);
        }

        private int? CalculateScore() {
            return TotalEnemies != 0
                ? (TotalEnemies - EnemiesBumped) * 100 / TotalEnemies
                : null;
        }
    }
}
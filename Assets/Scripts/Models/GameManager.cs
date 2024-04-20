namespace Models {
    public class GameManager {
        public static GameManager Instance { get; } = new();
        private int TotalEnemies { get; set; }
        private int EnemiesBumped { get; set; }
        public bool IsOver { get; private set; }

        public delegate void TotalEnemiesUpdateHandler(int value);

        public event TotalEnemiesUpdateHandler TotalEnemiesUpdate;

        public delegate void EnemiesBumpedUpdateHandler(int value);

        public event EnemiesBumpedUpdateHandler EnemiesBumpedUpdate;

        public delegate void GameOverUpdateHandler(int? score);

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
        }

        public void EndGame() {
            IsOver = true;

            GameOverUpdate?.Invoke(CalculateScore());
        }

        public void EnemyWasCreated() {
            TotalEnemiesUpdate?.Invoke(TotalEnemies++);
        }

        public void EnemyWasBumped() {
            EnemiesBumpedUpdate?.Invoke(EnemiesBumped++);
        }

        private int? CalculateScore() {
            return TotalEnemies != 0
                ? (TotalEnemies - EnemiesBumped) * 100 / TotalEnemies
                : null;
        }
    }
}
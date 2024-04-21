namespace Models {
    public class Award {
        public int Points { get; }

        public bool IsGiven { get; private set; }

        public delegate void GiveOutUpdateHandler();

        public event GiveOutUpdateHandler GiveOutUpdate;

        public Award(int points) {
            Points = points;
            IsGiven = false;
        }

        public void GiveOut() {
            IsGiven = true;

            GiveOutUpdate?.Invoke();
        }
    }
}
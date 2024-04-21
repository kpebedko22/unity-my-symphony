namespace Models {
    public class Hero {
        public string Name { get; private set; }
        public int Points { get; set; }

        public Hero(string name) {
            Name = name;
        }

        public void TakeAward(Award award) {
            if (award.IsGiven) {
                return;
            }

            Points += award.Points;
            award.GiveOut();
        }
    }
}
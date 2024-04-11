public class Hero {
    public int Points { get; set; }

    public void TakeAward(Award award) {
        Points += award.Points;
        award.IsGiven = true;
    }
}
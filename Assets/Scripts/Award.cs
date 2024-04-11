public class Award {
    public int Points { get; }

    public bool IsGiven { get; set; }

    public Award(int points) {
        Points = points;
        IsGiven = false;
    }
}
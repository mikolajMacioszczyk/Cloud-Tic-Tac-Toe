namespace CloudTicTacToe.Application.Results
{
    public class PlayerResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsComputer { get; set; }
        public int Points { get; set; }
    }
}

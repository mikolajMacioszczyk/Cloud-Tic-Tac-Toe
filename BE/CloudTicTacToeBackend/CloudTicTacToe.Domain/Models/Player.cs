namespace CloudTicTacToe.Domain.Models
{
    public class Player : BaseDomainModel
    {
        public string Name { get; set; } = null!;
        public bool IsComputer { get; set; }
        public int Points { get; set; }
    }
}

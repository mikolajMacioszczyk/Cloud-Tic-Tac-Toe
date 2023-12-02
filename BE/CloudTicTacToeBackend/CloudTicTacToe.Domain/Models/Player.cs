namespace CloudTicTacToe.Domain.Models
{
    public class Player : BaseDomainModel
    {
        public string Name { get; set; }
        public bool IsComputer { get; set; }
    }
}

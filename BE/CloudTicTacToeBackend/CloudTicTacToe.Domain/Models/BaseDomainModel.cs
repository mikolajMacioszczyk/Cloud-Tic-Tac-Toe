namespace CloudTicTacToe.Domain.Models
{
    public abstract class BaseDomainModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

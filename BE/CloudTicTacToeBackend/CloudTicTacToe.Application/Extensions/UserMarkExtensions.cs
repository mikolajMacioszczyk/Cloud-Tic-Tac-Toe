using CloudTicTacToe.Domain.Enums;

namespace CloudTicTacToe.Application.Extensions
{
    public static class UserMarkExtensions
    {
        public static FieldState ToFieldState(this UserMark userMark) =>
            userMark switch
            {
                UserMark.X => FieldState.X,
                UserMark.O => FieldState.O,
                _ => throw new ArgumentOutOfRangeException(userMark.ToString())
            };
    }
}

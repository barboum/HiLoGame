namespace HiLoGame.Domain.Exceptions;

public class NoPlayersException : GenericGameException
{
    internal Guid GameId { get; }
    internal NoPlayersException(Guid gameId) : base(gameId, "There are no players for this game")
    {
        GameId = gameId;
    }
}
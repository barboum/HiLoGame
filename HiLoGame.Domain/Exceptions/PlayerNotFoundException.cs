namespace HiLoGame.Domain.Exceptions;

public class PlayerNotFoundException : GenericGameException
{
    internal Guid GameId { get; }
    internal Guid PlayerId { get; }

    internal PlayerNotFoundException(Guid gameId, Guid playerId) : base(gameId, "The player that is trying to guess the number, does not exist")
    {
        PlayerId = playerId;
    }
}
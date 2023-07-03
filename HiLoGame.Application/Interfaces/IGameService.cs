using HiLoGame.Domain;

namespace HiLoGame.Application;

public interface IGameService
{
    Task<Guid> NewGame();
    Task<GameRange> InitGame(Guid gameId);
    Task<Guid> AddPlayer(Guid gameId, string name);
    Task<GameResult> GuessNumber(Guid gameId, Guid playerId, int chosenMisteryNumber);
}
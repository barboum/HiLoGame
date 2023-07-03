using HiLoGame.Domain;

namespace HiLoGame.Application;

public class GameService : IGameService
{
    private readonly IGenericRepository<Game> _repository;

    public GameService(IGenericRepository<Game> repository) => _repository = repository;

    public async Task<Guid> NewGame()
    {
        var game = Game.NewGame();
        await _repository.CreateAsync(game);
        return game.Id;
    }

    public async Task<Guid> AddPlayer(Guid gameId, string name)
    {
        Game game = await _repository.GetByIdAsync(gameId);
        Guid playerId = game.AddPlayer(name);
        await _repository.UpdateAsync(game);

        return playerId;
    }
    public async Task<GameRange> InitGame(Guid gameId)
    {
        Game game = await _repository.GetByIdAsync(gameId);
        GameRange gamerange = game.InitGame();
        await _repository.UpdateAsync(game);

        return gamerange;
    }
    public async Task<GameResult> GuessNumber(Guid gameId, Guid playerId, int chosenMisteryNumber)
    {
        Game game = await _repository.GetByIdAsync(gameId);
        GameResult gameResult = game.GuessNumber(playerId, chosenMisteryNumber);
        await _repository.UpdateAsync(game);

        return gameResult;
    }
}
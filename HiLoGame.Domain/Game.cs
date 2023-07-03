using HiLoGame.Domain.Exceptions;

namespace HiLoGame.Domain;

public record Game : BaseEntity
{
    public bool IsActive { get; set; }
    public IList<Player> Players { get; private set; } = new List<Player>();
    private readonly GameRange _gameRange = new(1, 1000);

    private Game(Guid Id) : base(Id)
    {

    }

    public static Game LoadGame(Guid Id, string description)
    {
        return new(Id);
    }

    public static Game NewGame()
    {
        return new(Guid.NewGuid());
    }

    public Guid AddPlayer(string name)
    {
        if (this is null)
            throw new GameNotCreatedOrStartedException();

        if (string.IsNullOrEmpty(name))
            throw new EmptyPlayerNameException();

        Player player = new(Guid.NewGuid(), name, SetMisteryNumber(_gameRange));

        this.Players.Add(player);
        return player.Id;
    }
    public GameRange InitGame()
    {
        if (this.Players.Count == 0)
            throw new NoPlayersException(this.Id);

        this.IsActive = true;
        return _gameRange;
    }
    private int SetMisteryNumber(GameRange gameRange)
    {
        Random rand = new Random();
        int misteryNumber = rand.Next(gameRange.Min, gameRange.Max);
        return misteryNumber;
    }
    public GameResult GuessNumber(Guid playerId, int chosenMisteryNumber)
    {
        if (!this.IsActive)
            throw new GameNotCreatedOrStartedException();

        var playerMisteryNumber = this.Players.Where(x => x.Id == playerId).Select(x => x.MisteryNumber).FirstOrDefault();

        if (playerMisteryNumber == 0)
            throw new PlayerNotFoundException(this.Id, playerId);

        if (chosenMisteryNumber < playerMisteryNumber)
            return GameResult.Lower;

        if (chosenMisteryNumber > playerMisteryNumber)
            return GameResult.Higher;

        this.IsActive = false;
        return GameResult.Win;
    }
}
using HiloGame.UI.Console;
using HiLoGame.Application;
using HiLoGame.Domain;

namespace HiLoGame.UI;

public class GameLauncher
{
    private readonly IGameService _gameService;
    private readonly IConsoleIO _console;
    private readonly Queue<(Guid Id, string Name)> _players;
    private GameRange? _gameRange;

    public GameLauncher(IGameService gameService, IConsoleIO console)
    {
        _gameService = gameService;
        _console = console;
        _players = new Queue<(Guid, string)>();
    }
    public async void Run()
    {
        Guid gameId;

        gameId = await NewGame();
        await CreateGamePlayers(gameId);
        await StartGame(gameId);
        await PlayTurn(gameId);
    }
    private async Task<Guid> NewGame()
    {
        this._console.WriteLine("Welcome to the fantastic High Low game from Gaming1!");
        this._console.WriteLine("----------------------------------------------------\n");
        this._console.WriteLine("Are you ready to start?\n");
        this._console.WriteLine("Press any Key if you want to start a new game!!!");
        this._console.WriteLine("... or (ESC) to exit..\n");

        ConsoleKeyInfo keyInfo = this._console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.Escape)
            TerminateGame();

        Guid gameId = await _gameService.NewGame();
        return gameId;
    }
    public async Task CreateGamePlayers(Guid gameId)
    {
        string playerName = SetPlayerName();

        try
        {
            await CreatePlayer(gameId, playerName);
            this._console.WriteLine("Do you want to insert another player? (Y) Yes or any Key to continue!!!");
            ConsoleKeyInfo keyInfo = this._console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Y)
            {
                playerName = SetPlayerName();
                await CreatePlayer(gameId, playerName);
            }
        }
        catch (Exception ex)
        {
            this._console.WriteLine(ex.Message);
            await CreateGamePlayers(gameId);
        }
    }

    private string SetPlayerName()
    {
        this._console.WriteLine("Please set the name of the Player: ");
        string playerName = this._console.ReadLine();

        if (!string.IsNullOrWhiteSpace(playerName))
            return playerName;

        this._console.WriteLine("The player name cannot be empty");
        return SetPlayerName();
    }

    private async Task CreatePlayer(Guid gameId, string playerName)
    {
        Guid guid = await _gameService.AddPlayer(gameId, playerName);
        _players.Enqueue((guid, playerName));
        this._console.WriteLine($"{playerName} created!!\n");
    }

    private async Task StartGame(Guid gameId)
    {
        try
        {
            _gameRange = await _gameService.InitGame(gameId);
            this._console.WriteLine("\n###########################################################################################################");
            this._console.WriteLine("Welcome players! Your game was created and your Mistery Number is already generated for you to guess!!");
            this._console.WriteLine($"Your Mistery Number is between {_gameRange.Min} and {_gameRange.Max}. Good luck!");
            this._console.WriteLine("\n###########################################################################################################\n\n");
        }
        catch (Exception ex)
        {
            this._console.WriteLine(ex.Message);
            TerminateGame();
        }
    }
    public async Task PlayTurn(Guid gameId)
    {
        (Guid Id, string Name) player = _players.Peek();

        this._console.WriteLine($"\nIt's time for {player.Name} to try to guess the Mistery Number!!");
        this._console.WriteLine(@$"What number will you choose?");

        int parsedChosenMisteryNumber = validatedChosenNumber();

        if (parsedChosenMisteryNumber > 0)
        {
            try
            {
                GameResult result = await _gameService.GuessNumber(gameId, player.Id, parsedChosenMisteryNumber);
                ParseGameResult(player, result);
                _players.Enqueue(_players.Dequeue());
                await PlayTurn(gameId);
            }
            catch (Exception ex)
            {
                this._console.WriteLine(ex.Message);
                await PlayTurn(gameId);
            }
        }
        else
        {
            this._console.WriteLine(@$"You need to enter a valid number and between {_gameRange.Min} and {_gameRange.Max}!!");
            await PlayTurn(gameId);
        }
    }
    public void ParseGameResult((Guid Id, string Name) player, Enum result)
    {
        if (result.Equals(GameResult.Win))
        {
            this._console.WriteLine($"\nCONGRATULATIONS!!! Player {player.Name} has beaten the game!! You are the true WINNER!!!\n");
            TerminateGame();
        }

        if (result.Equals(GameResult.Lower))
            this._console.WriteLine($"\nToo bad, {player.Name}!! Your guess is lower than the winning mistery Number\n");

        if (result.Equals(GameResult.Higher))
            this._console.WriteLine($"\nToo bad, {player.Name}!! Your guess is higher than the winning mistery Number\n");

        this._console.WriteLine("----------------------------------------------------------------------------\n");
    }

    private int validatedChosenNumber()
    {
        bool validNumber = int.TryParse(this._console.ReadLine(), out int parsedChosenMisteryNumber);

        if (validNumber && parsedChosenMisteryNumber > _gameRange.Min && parsedChosenMisteryNumber < _gameRange.Max)
            return parsedChosenMisteryNumber;
        return 0;
    }

    private void TerminateGame()
    {
        this._console.WriteLine("Thank you for playing our Gaming1 game!!!");
        this._console.WriteLine("Press any key to exit.");
        this._console.ReadKey(true);

        Environment.Exit(0);
    }
}
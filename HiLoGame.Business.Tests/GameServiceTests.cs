namespace HiLoGame.Business.Tests;

using HiLoGame.Application;
using HiLoGame.Domain;
using Moq;

public class GameServiceTests
{
    private Mock<IGenericRepository<Game>> _mockedRepository = new Mock<IGenericRepository<Game>>();
    private GameService _sut;
    private Game _game;

    public GameServiceTests()
    {
        _sut = new GameService(_mockedRepository.Object);

        DummyGame dummyGame = new();
        _game = dummyGame.CreateDummyGame();
    }

    [Fact]
    public async void When_InitGame_Game_Should_Be_Active()
    {
        //Arrange
        bool expectedGameState = true;
        _game.IsActive = false;

        _mockedRepository.Setup(c => c.GetByIdAsync(_game.Id)).ReturnsAsync(_game);
        _mockedRepository.Setup(c => c.UpdateAsync(_game)).ReturnsAsync(_game);

        //Act
        await _sut.InitGame(_game.Id);

        //Assert
        Assert.Equal(expectedGameState, _game.IsActive);
    }

    [Fact]
    public async void When_Add_PLayer_PlayerList_Should_Increment_1()
    {
        int expectedPlayerCount = 4;
        //Arrange
        _mockedRepository.Setup(c => c.GetByIdAsync(_game.Id)).ReturnsAsync(_game);

        //Act
        await _sut.AddPlayer(_game.Id, "Jorge");
        //Assert

        Assert.Equal(expectedPlayerCount, _game.Players.Count);
    }

    [Fact]
    public async void When_Chosen_Player_Number_Is_Higher_Than_Mistery_Number_Should_Return_Higher()
    {
        //Arrange
        int chosenMisteryNumber = 900;
        GameResult expectedGameResult = GameResult.Higher;

        _mockedRepository.Setup(c => c.GetByIdAsync(_game.Id)).ReturnsAsync(_game);

        //Act
        GameResult actualGameResult = await _sut.GuessNumber(_game.Id, _game.Players[2].Id, chosenMisteryNumber);

        //Assert
        Assert.Equal(expectedGameResult, actualGameResult);
    }

    [Fact]
    public async void When_Chosen_Player_Number_Is_Lower_Than_Mistery_Number_Should_Return_Lower()
    {
        //Arrange
        int chosenMisteryNumber = 300;
        GameResult expectedGameResult = GameResult.Lower;

        _mockedRepository.Setup(c => c.GetByIdAsync(_game.Id)).ReturnsAsync(_game);

        //Act
        GameResult actualGameResult = await _sut.GuessNumber(_game.Id, _game.Players[2].Id, chosenMisteryNumber);

        //Assert
        Assert.Equal(expectedGameResult, actualGameResult);
    }

    [Fact]
    public async void When_Chosen_Player_Number_Is_Equal_To_Mistery_Number_Should_Return_Win()
    {
        //Arrange
        int chosenMisteryNumber = 500;
        GameResult expectedGameResult = GameResult.Win;

        _mockedRepository.Setup(c => c.GetByIdAsync(_game.Id)).ReturnsAsync(_game);

        //Act
        GameResult actualGameResult = await _sut.GuessNumber(_game.Id, _game.Players[2].Id, chosenMisteryNumber);

        //Assert
        Assert.Equal(expectedGameResult, actualGameResult);
    }

}

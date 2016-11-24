using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Infrastructure.CustomExceptions;
using GameStore.BLL.Services;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;
using Moq;
using NUnit.Framework;

namespace GameStore.BLL.UnitTests.Services
{
    [TestFixture]
    public class GameServiceTest
    {
        private static List<Game> GetGameShortList()
        {
            var gameViewModelList = new List<Game>
            {
                new Game
                {
                    Description = "TestDescription1",
                    Key = "TestKey1",
                    Name = "TestName1"
                },
                new Game
                {
                    Key = "TestKey2",
                    Name = "TestName2"
                }
            };

            return gameViewModelList;
        }

        private static List<GameBll> GetGameBllShortList()
        {
            var gameBllList = new List<GameBll>
            {
                new GameBll
                {
                    // Id = 1,
                    Description = "TestDescription1",
                    Key = "TestKey1",
                    Name = "TestName1"
                },
                new GameBll
                {
                    Key = "TestKey2",
                    Name = "TestName2"
                }
            };

            return gameBllList;
        }

        [Test]
        public void GetAllAsync_IfAnyDALExceptionIsThrown_ShouldReturnAccessException()
        {
            //Arrange
            var gameRepositoryMock = new Mock<IGenericRepository<Game>>();
            gameRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Game, bool>>>(), null, "", 0))
                .Throws<Exception>();

            var gameUnitOfWorkMock = new Mock<IUnitOfWork>();
            gameUnitOfWorkMock.Setup(x => x.GetRepository<Game>()).Returns(gameRepositoryMock.Object);

            var gameService = new GameService(gameUnitOfWorkMock.Object, null);
            //Act
            //Assert
            Assert.ThrowsAsync<AccessException>(async () => await gameService.GetAllAsync());
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfGamesBll_WithAllGames()
        {
            // Arrange
            var gamesShortList = GetGameShortList();
            var gameRepositoryMock = new Mock<IGenericRepository<Game>>();
            gameRepositoryMock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(gamesShortList.AsEnumerable()));

            var gameUnitOfWorkMock = new Mock<IUnitOfWork>();
            gameUnitOfWorkMock.Setup(x => x.CommitAsync());
            gameUnitOfWorkMock.Setup(x => x.GetRepository<Game>())
                .Returns(gameRepositoryMock.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<GameBll>>(It.IsAny<List<Game>>()))
                .Returns(GetGameBllShortList().ToList());

            var gameService = new GameService(gameUnitOfWorkMock.Object, mapperMock.Object);

            // Act
            var actResult = await gameService.GetAllAsync();

            // Assert
            Assert.IsNotNull(actResult);
            Assert.AreEqual(gamesShortList.Count, actResult.Count);
        }

        [Test]
        [TestCase("Key")]
        public async Task GetByGenreAsync_ShouldReturnListOfGamesBll_ByGenreKey(string key)
        {
            // Arrange
            var gamesShortList = GetGameShortList();
            var gameRepositoryMock = new Mock<IGenericRepository<Game>>();
            gameRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Game, bool>>>(), null, "", 0))
                .ReturnsAsync(gamesShortList);

            var gameUnitOfWorkMock = new Mock<IUnitOfWork>();
            gameUnitOfWorkMock.Setup(x => x.CommitAsync());
            gameUnitOfWorkMock.Setup(x => x.GetRepository<Game>()).Returns(gameRepositoryMock.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<GameBll>>(It.IsAny<List<Game>>())).Returns(GetGameBllShortList());

            var gameService = new GameService(gameUnitOfWorkMock.Object, mapperMock.Object);

            // Act
            var actResult = await gameService.GetByGenreAsync(key);

            // Assert
            Assert.IsNotNull(actResult);
            Assert.AreEqual(gamesShortList.Count, actResult.Count);
        }

        [Test]
        [TestCase("Key")]
        public void GetByGenreAsync_IfAnyDALExceptionIsThrown_ShouldReturnAccessException(string key)
        {
            //Arrange
            var gameRepositoryMock = new Mock<IGenericRepository<Game>>();
            gameRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Game, bool>>>(), null, "", 0))
                .Throws<Exception>();

            var gameUnitOfWorkMock = new Mock<IUnitOfWork>();
            gameUnitOfWorkMock.Setup(x => x.GetRepository<Game>()).Returns(gameRepositoryMock.Object);

            var gameService = new GameService(gameUnitOfWorkMock.Object, null);
            //Act
            //Assert
            Assert.ThrowsAsync<AccessException>(async () => await gameService.GetByGenreAsync(key));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetByPlatformTypeAsync_ShouldReturnListOfGamesBll_ByPlatformType(string key)
        {
            // Arrange
            var gamesShortList = GetGameShortList();
            var gameRepositoryMock = new Mock<IGenericRepository<Game>>();
            gameRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Game, bool>>>(), null, "", 0))
                .ReturnsAsync(gamesShortList);

            var gameUnitOfWorkMock = new Mock<IUnitOfWork>();
            gameUnitOfWorkMock.Setup(x => x.CommitAsync());
            gameUnitOfWorkMock.Setup(x => x.GetRepository<Game>()).Returns(gameRepositoryMock.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<GameBll>>(It.IsAny<List<Game>>())).Returns(GetGameBllShortList());

            var gameService = new GameService(gameUnitOfWorkMock.Object, mapperMock.Object);

            // Act
            var actResult = await gameService.GetByPlatformTypeAsync(key);

            // Assert
            Assert.IsNotNull(actResult);
            Assert.AreEqual(gamesShortList.Count, actResult.Count);
        }

        [Test]
        [TestCase("Key")]
        public void GetByPlatformTypeAsync_IfAnyDALExceptionIsThrown_ShouldReturnAccessException(string key)
        {
            //Arrange
            var gameRepositoryMock = new Mock<IGenericRepository<Game>>();
            gameRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Game, bool>>>(), null, "", 0))
                .Throws<Exception>();

            var gameUnitOfWorkMock = new Mock<IUnitOfWork>();
            gameUnitOfWorkMock
                .Setup(x => x.GetRepository<Game>())
                .Returns(gameRepositoryMock.Object);

            var gameService = new GameService(gameUnitOfWorkMock.Object, null);
            //Act
            //Assert
            Assert.ThrowsAsync<AccessException>(async () => await gameService.GetByPlatformTypeAsync(key));
        }

        [Test]
        [TestCase("GameKey")]
        public async Task GetDetailsByKeyAsync_ShouldReturnGamesBllWithDetailsByGameKey_IfGameExist(string gamekey)
        {
            // Arrange
            var gameRepositoryMock = new Mock<IGenericRepository<Game>>();
            gameRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Game, bool>>>(), null, It.IsAny<string>(), 0))
                .ReturnsAsync(new List<Game>());

            var gameUnitOfWorkMock = new Mock<IUnitOfWork>();
            gameUnitOfWorkMock.Setup(x => x.CommitAsync());
            gameUnitOfWorkMock.Setup(x => x.GetRepository<Game>()).Returns(gameRepositoryMock.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<Game>())).Returns(new GameBll());

            var gameService = new GameService(gameUnitOfWorkMock.Object, mapperMock.Object);

            // Act
            var actResult = await gameService.GetDetailsByKeyAsync(gamekey);

            // Assert
            Assert.IsNotNull(actResult);
        }
    }
}
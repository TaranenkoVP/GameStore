using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FluentAssertions;
using GameStore.BLL.Entities;
using GameStore.BLL.Infrastructure.CustomExceptions;
using GameStore.BLL.Interfaces.Services;
using GameStore.Web.MVC.Controllers;
using GameStore.WEB.MVC.Models.CommentViewModels;
using GameStore.WEB.MVC.Models.GameViewModels;
using GameStore.WEB.MVC.Models.GenreViewModels;
using GameStore.WEB.MVC.Models.PlatformTypeViewModels;
using Moq;
using NUnit.Framework;

namespace GameStore.WEB.UnitTests.Controllers
{
    [TestFixture]
    public class GameControllerTest
    {
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

        private static List<GameViewModel> GetGameViewModelShortList()
        {
            var gameViewModelList = new List<GameViewModel>
            {
                new GameViewModel
                {
                    Description = "TestDescription1",
                    Key = "TestKey1",
                    Name = "TestName1"
                },
                new GameViewModel
                {
                    Key = "TestKey2",
                    Name = "TestName2"
                }
            };

            return gameViewModelList;
        }

        private static List<CommentBll> GetCommentBllList()
        {
            var commentBllList = new List<CommentBll>
            {
                new CommentBll
                {
                    Name = "Vasiliy",
                    GameId = 1,
                    Body = "The game broke my life!"
                },
                new CommentBll
                {
                    Name = "Alex",
                    GameId = 2,
                    Body = "It's just because you're playing a warlock",
                    AuthorId = 1
                }
            };

            return commentBllList;
        }

        private static List<CommentViewModel> GetCommentViewModelList()
        {
            var commentBllList = new List<CommentViewModel>
            {
                new CommentViewModel
                {
                    Name = "Vasiliy",
                    GameId = 1,
                    Body = "The game broke my life!"
                },
                new CommentViewModel
                {
                    Name = "Alex",
                    GameId = 2,
                    Body = "It's just because you're playing a warlock",
                    AuthorId = 1
                }
            };

            return commentBllList;
        }


        private static GameBll GetGameBllWithDetails()
        {
            var game = new GameBll
            {
                Id = 1,
                Description = "TestDescription1",
                Key = "TestKey1",
                Name = "TestName1",
                PlatformTypes = new List<PlatformTypeBll>
                {
                    new PlatformTypeBll {Id = 1, Type = "Mobile"},
                    new PlatformTypeBll {Id = 2, Type = "Desktop"},
                    new PlatformTypeBll {Id = 4, Type = "Browser"}
                },
                Genres = new List<GenreBll>
                {
                    new GenreBll {Id = 4, Name = "Strategy"},
                    new GenreBll {Id = 3, Name = "Sports"},
                    new GenreBll {Id = 1, Name = "Adventure"}
                }
            };

            return game;
        }

        private static GameDetailsViewModel GetGameViewModelWithDetails()
        {
            var game = new GameDetailsViewModel
            {
                Id = 1,
                Description = "TestDescription1",
                Key = "TestKey1",
                Name = "TestName1",
                PlatformTypes = new List<PlatformTypeViewModel>
                {
                    new PlatformTypeViewModel {Id = 1, Type = "Mobile"},
                    new PlatformTypeViewModel {Id = 2, Type = "Desktop"},
                    new PlatformTypeViewModel {Id = 4, Type = "Browser"}
                },
                Genres = new List<GenreViewModel>
                {
                    new GenreViewModel {Id = 4, Name = "Strategy"},
                    new GenreViewModel {Id = 3, Name = "Sports"},
                    new GenreViewModel {Id = 1, Name = "Adventure"}
                }
            };

            return game;
        }

        private static GameInputViewModel GetGameNewViewModel()
        {
            var game = new GameInputViewModel
            {
                Description = "TestDescription1",
                Key = "TestKey1",
                Name = "TestName1",
                PlatformTypes = new List<PlatformTypeViewModel>
                {
                    new PlatformTypeViewModel {Id = 1, Type = "Mobile"},
                    new PlatformTypeViewModel {Id = 2, Type = "Desktop"},
                    new PlatformTypeViewModel {Id = 4, Type = "Browser"}
                },
                Genres = new List<GenreViewModel>
                {
                    new GenreViewModel {Id = 4, Name = "Strategy"},
                    new GenreViewModel {Id = 3, Name = "Sports"},
                    new GenreViewModel {Id = 1, Name = "Adventure"}
                }
            };

            return game;
        }

        [Test]
        [TestCase("Key")]
        public async Task DeleteGame_DeletingGame_CalledGameServiceOnce(string key)
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.DeleteAsync(key)).Returns(Task.FromResult(GetGameBllWithDetails()));
            var gameController = new GameController(gameServiceMock.Object, null, null);

            // Act
            var actResult = await gameController.Delete(key);

            // Assert
            gameServiceMock.Verify(x => x.DeleteAsync(key), Times.Once);
        }

        [Test]
        [TestCase("Key")]
        public async Task DeleteGame_DeletingGame_ShouldBeRedirectedToGameIndexIfOK(string key)
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.DeleteAsync(key)).Returns(Task.FromResult(GetGameBllWithDetails()));
            var gameController = new GameController(gameServiceMock.Object, null, null);

            // Act
            var actResult = await gameController.Delete(key) as RedirectToRouteResult;

            // Assert
            Assert.That(actResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [TestCase("Key")]
        public async Task DeleteGame_IfAccessExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.DeleteAsync(key)).Throws<AccessException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.Delete(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task DeleteGame_IfAnyExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.DeleteAsync(key)).Throws<Exception>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.Delete(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task DeleteGame_IfValidationExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.DeleteAsync(key)).Throws<ValidationException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.Delete(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        public async Task EditGame_EditingGame_CalledGameServiceOnceForEditingGameBll()
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock
                .Setup(a => a.UpdateAsync(It.IsAny<GameBll>()))
                .Returns(Task.FromResult(GetGameBllWithDetails()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            // Act
            var actResult = await gameController.EditGame(GetGameNewViewModel());

            // Assert
            gameServiceMock.Verify(x => x.UpdateAsync(It.IsAny<GameBll>()), Times.Once);
        }

        [Test]
        public async Task EditGame_EditingGame_ShouldBeRedirectedToGameIndexIfOK()
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock
                .Setup(a => a.UpdateAsync(It.IsAny<GameBll>()))
                .Returns(Task.FromResult(GetGameBllWithDetails()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            // Act
            var actResult = await gameController.EditGame(GetGameNewViewModel()) as RedirectToRouteResult;

            // Assert
            Assert.That(actResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public async Task EditGame_IfAccessExceptionBllIsThrown_StayInNewViewModel()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.UpdateAsync(It.IsAny<GameBll>())).Throws<AccessException>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            //Act
            var actResult = await gameController.EditGame(GetGameNewViewModel()) as ViewResult;

            //Assert
            Assert.IsInstanceOf<GameInputViewModel>(actResult.Model);
        }

        [Test]
        public async Task EditGame_IfAnyExceptionBllIsThrown_StayInNewViewModel()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.UpdateAsync(It.IsAny<GameBll>())).Throws<Exception>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            //Act
            var actResult = await gameController.EditGame(GetGameNewViewModel()) as ViewResult;

            //Assert
            Assert.IsInstanceOf<GameInputViewModel>(actResult.Model);
        }


        [Test]
        public async Task EditGame_IfValidationExceptionBllIsThrown_StayInNewViewModel()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.UpdateAsync(It.IsAny<GameBll>())).Throws<ValidationException>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            //Act
            var actResult = await gameController.EditGame(GetGameNewViewModel()) as ViewResult;

            //Assert
            Assert.IsInstanceOf<GameInputViewModel>(actResult.Model);
        }

        [Test]
        [TestCase("Key")]
        public async Task GameComments_IfAccessExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var commentServiceMock = new Mock<ICommentService>();
            commentServiceMock.Setup(a => a.GetCommentsAsync(key)).Throws<AccessException>();
            var gameController = new GameController(null, commentServiceMock.Object, null);

            //Act
            var actResult = await gameController.GameComments(key) as ViewResult;

            //Assert
            actResult.Should().NotBeNull();
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GameComments_IfAnyExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var commentServiceMock = new Mock<ICommentService>();
            commentServiceMock.Setup(a => a.GetCommentsAsync(key)).Throws<Exception>();
            var gameController = new GameController(null, commentServiceMock.Object, null);

            //Act
            var actResult = await gameController.GameComments(key) as ViewResult;

            //Assert
            actResult.Should().NotBeNull();
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GameComments_IfValidationExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var commentServiceMock = new Mock<ICommentService>();
            commentServiceMock.Setup(a => a.GetCommentsAsync(key)).Throws<ValidationException>();
            var gameController = new GameController(null, commentServiceMock.Object, null);

            //Act
            var actResult = await gameController.GameComments(key) as ViewResult;

            //Assert
            actResult.Should().NotBeNull();
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GameComments_ReturnListOfComments_ByGameKey(string key)
        {
            // Arrange
            var commentServiceMock = new Mock<ICommentService>();
            commentServiceMock.Setup(a => a.GetCommentsAsync(key)).ReturnsAsync(GetCommentBllList());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<CommentViewModel>>(It.IsAny<List<CommentBll>>()))
                .Returns(GetCommentViewModelList());

            var gameController = new GameController(null, commentServiceMock.Object, mapperMock.Object);
            var expectedCommentList = GetCommentViewModelList().ToList();

            // Act
            var result = await gameController.GameComments(key) as ViewResult;

            // Assert
            result.Model.Should().NotBeNull();
            result.Model.ShouldBeEquivalentTo(expectedCommentList);
        }

        [Test]
        public async Task GetAllGames_IfAccessExceptionBllIsThrown_CreateErrorView()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetAllAsync()).Throws<AccessException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetAllGames() as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        public async Task GetAllGames_IfAnyExceptionBllIsThrown_CreateErrorView()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetAllAsync()).Throws<Exception>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetAllGames() as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        public async Task GetAllGames_ReturnListOfGameViewModels_WithAllGames()
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult(GetGameBllShortList()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<GameViewModel>>(It.IsAny<List<GameBll>>()))
                .Returns(GetGameViewModelShortList());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);
            var expectedGameList = GetGameViewModelShortList();

            // Act
            var actResult = await gameController.GetAllGames() as ViewResult;

            // Assert
            actResult.Model.Should().NotBeNull();
            actResult.Model.ShouldBeEquivalentTo(expectedGameList);
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGameByKey_IfAccessExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetDetailsByKeyAsync(key)).Throws<AccessException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGameByKey(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGameByKey_IfAnyExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetDetailsByKeyAsync(key)).Throws<Exception>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGameByKey(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGameByKey_IfValidationExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetDetailsByKeyAsync(key)).Throws<ValidationException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGameByKey(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGameByKey_ReturnGameViewModel_WithGameDetailsByKey(string key)
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetDetailsByKeyAsync(key)).ReturnsAsync(GetGameBllWithDetails());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameDetailsViewModel>(It.IsAny<GameBll>()))
                .Returns(GetGameViewModelWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);
            var expectedGame = GetGameViewModelWithDetails();

            // Act
            var result = await gameController.GetGameByKey(key) as ViewResult;

            // Assert
            result.Model.Should().NotBeNull();
            result.Model.ShouldBeEquivalentTo(expectedGame);
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByGenre_IfAccessExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetByGenreAsync(key)).Throws<AccessException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGamesByGenre(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByGenre_IfAnyExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetByGenreAsync(key)).Throws<Exception>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGamesByGenre(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByGenre_IfValidationExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetByGenreAsync(key)).Throws<ValidationException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGamesByGenre(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByGenre_ReturnListOfGameViewModels_ByGenreKey(string key)
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock
                .Setup(a => a.GetByGenreAsync(key))
                .Returns(Task.FromResult(GetGameBllShortList()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<GameViewModel>>(It.IsAny<List<GameBll>>()))
                .Returns(GetGameViewModelShortList());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);
            var expectedGameList = GetGameViewModelShortList();

            // Act
            var result = await gameController.GetGamesByGenre(key) as ViewResult;

            // Assert
            result.Model.Should().NotBeNull();
            result.Model.ShouldBeEquivalentTo(expectedGameList);
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByPlatformType_IfAccessExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetByPlatformTypeAsync(key)).Throws<AccessException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGamesByPlatformType(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByPlatformType_IfAnyExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetByPlatformTypeAsync(key)).Throws<Exception>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGamesByPlatformType(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByPlatformType_IfValidationExceptionBllIsThrown_CreateErrorView(string key)
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetByPlatformTypeAsync(key)).Throws<ValidationException>();
            var gameController = new GameController(gameServiceMock.Object, null, null);

            //Act
            var actResult = await gameController.GetGamesByPlatformType(key) as ViewResult;

            //Assert
            Assert.That(actResult.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        [TestCase("Key")]
        public async Task GetGamesByPlatformType_ReturnListOfGameViewModels_ByPlatformTypeKey(string key)
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.GetByPlatformTypeAsync(key)).ReturnsAsync(GetGameBllShortList());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<GameViewModel>>(It.IsAny<List<GameBll>>()))
                .Returns(GetGameViewModelShortList());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);
            var expectedGameList = GetGameViewModelShortList();

            // Act
            var result = await gameController.GetGamesByPlatformType(key) as ViewResult;

            // Assert
            result.Model.Should().NotBeNull();
            result.Model.ShouldBeEquivalentTo(expectedGameList);
        }

        [Test]
        public async Task NewGame_AddingNewGame_CalledGameServiceOnceForAddingGameBll()
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock
                .Setup(a => a.AddAsync(It.IsAny<GameBll>()))
                .Returns(Task.FromResult(GetGameBllWithDetails()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            // Act
            var actResult = await gameController.NewGame(GetGameNewViewModel());

            // Assert
            gameServiceMock.Verify(x => x.AddAsync(It.IsAny<GameBll>()), Times.Once);
        }

        [Test]
        public async Task NewGame_AddingNewGame_ShouldBeRedirectedToGameIndexIfOK()
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock
                .Setup(a => a.AddAsync(It.IsAny<GameBll>()))
                .Returns(Task.FromResult(GetGameBllWithDetails()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            // Act
            var actResult = await gameController.NewGame(GetGameNewViewModel()) as RedirectToRouteResult;

            // Assert
            Assert.That(actResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public async Task NewGame_IfAccessExceptionBllIsThrown_StayInNewViewModel()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.AddAsync(It.IsAny<GameBll>())).Throws<AccessException>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            //Act
            var actResult = await gameController.NewGame(GetGameNewViewModel()) as ViewResult;

            //Assert
            Assert.IsInstanceOf<GameInputViewModel>(actResult.Model);
        }

        [Test]
        public async Task NewGame_IfAnyExceptionBllIsThrown_StayInNewViewModel()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.AddAsync(It.IsAny<GameBll>())).Throws<Exception>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            //Act
            var actResult = await gameController.NewGame(GetGameNewViewModel()) as ViewResult;

            //Assert
            Assert.IsInstanceOf<GameInputViewModel>(actResult.Model);
        }


        [Test]
        public async Task NewGame_IfValidationExceptionBllIsThrown_StayInNewViewModel()
        {
            //Arrange
            var gameServiceMock = new Mock<IGameService>();
            gameServiceMock.Setup(a => a.AddAsync(It.IsAny<GameBll>())).Throws<ValidationException>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<GameBll>(It.IsAny<List<GameViewModel>>())).Returns(GetGameBllWithDetails());

            var gameController = new GameController(gameServiceMock.Object, null, mapperMock.Object);

            //Act
            var actResult = await gameController.NewGame(GetGameNewViewModel()) as ViewResult;

            //Assert
            Assert.IsInstanceOf<GameInputViewModel>(actResult.Model);
        }

        [Test]
        public void NewGame_return_NewGameInputModel()
        {
            // Arrange
            var gameController = new GameController(null, null, null);

            // Act
            var actResult = gameController.NewGame() as ViewResult;

            // Assert
            actResult.Model.Should().NotBeNull();
            Assert.That(actResult.ViewName, Is.EqualTo("NewGame"));
        }

        //public async Task Index_ReturnsAJsonResult_WithAListOfAllGames()

        //[Test]
        //        .Returns(Task.FromResult(_listGameBll));
        //    // Act
        //    var result = await _objGameController.Index();
        //    var output = new JavaScriptSerializer().Serialize(result.Data);
        //    // Assert
        //    Assert.IsInstanceOf<JsonResult>(result);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(ExpectedJsonGameBllList, output);
        //}

        //[Test]
        //public async Task Index_ReturnGameViewModel_WithAListOfAllGames()
        //{
        //    // Arrange
        //    _gameServiceMock
        //        .Setup(a => a.GetAllAsync())
        //        .Returns(Task.FromResult(_listGameBll));
        //    // Act
        //    var actionResult = await _objGameController.Index();
        //    var viewResult = actionResult as ViewResult;
        //    // Assert
        //    if (viewResult != null)
        //    {
        //        var result = viewResult.Model as IEnumerable<GameViewModel>;
        //        CollectionAssert.AreEqual(_expectedGameList, result);
        //    }

        //    Assert.IsNotNull(viewResult);
        //    //Assert.AreEqual(ExpectedJsonGameBllList, result);
        //}

        //[Test]
        //[TestCase("RTS")]
        //public async Task ByGenre_ReturnsActionResult_WithAListOfGamesByGenre(string key)
        //{
        //    // Arrange
        //    _gameServiceMock
        //        .Setup(a => a.GetByGenreAsync(key))
        //        .Returns(Task.FromResult(_listGameBll));
        //    // Act
        //    var actionResult = await _objGameController.ByGenre(key);
        //    var viewResult = actionResult as ViewResult;

        //    // Assert
        //    if (viewResult != null)
        //    {
        //        var result = viewResult.Model as IEnumerable<GameViewModel>;
        //        CollectionAssert.AreEqual(_expectedGameList, result);
        //    }
        //    Assert.IsNotNull(viewResult);
        //    //Assert.AreEqual(_expectedGameList, result);
        //}

        //[Test]
        //[TestCase("mobile")]
        //public async Task ByPlatformType_ReturnsActionResult_WithAListOfGamesByByPlatformType(string key)
        //{
        //    // Arrange
        //    _gameServiceMock
        //        .Setup(a => a.GetByPlatformTypeAsync(key))
        //        .Returns(Task.FromResult(_listGameBll));
        //    // Act
        //    var result = (((await _objGameController.ByPlatformType(key)) as ViewResult).Model) as List<GameViewModel>;
        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(_expectedGameList, result);
        //}
    }
}
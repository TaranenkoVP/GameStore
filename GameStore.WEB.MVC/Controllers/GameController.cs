using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Infrastructure.CustomExceptions;
using GameStore.BLL.Interfaces.Services;
using GameStore.WEB.MVC.HelpAttributes;
using GameStore.WEB.MVC.Models.CommentViewModels;
using GameStore.WEB.MVC.Models.GameViewModels;

namespace GameStore.Web.MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapperWeb;

        public GameController(IGameService gameService, ICommentService commentService, IMapper mapperWeb)
        {
            _gameService = gameService;
            _commentService = commentService;
            _mapperWeb = mapperWeb;
        }

        // GET: Game
        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> GetAllGames()
        {
            List<GameViewModel> games = null;
            try
            {
                var gamesBll = await _gameService.GetAllAsync();
                games = _mapperWeb.Map<List<GameViewModel>>(gamesBll);
            }
            catch (AccessException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (Exception)
            {
                return View("Error", new HandleErrorInfo(new InvalidOperationException("Bad request"), "Game", "Index"));
            }

            return View(games);
        }

        [HttpGet]
        [ActionName("ByKey")]
        public async Task<ActionResult> GetGameByKey(string key)
        {
            GameDetailsViewModel game = null;
            try
            {
                var gameBll = await _gameService.GetDetailsByKeyAsync(key);
                game = _mapperWeb.Map<GameDetailsViewModel>(gameBll);
            }
            catch (ValidationException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (AccessException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (Exception)
            {
                return View("Error", new HandleErrorInfo(new InvalidOperationException("Bad request"), "Game", "Index"));
            }

            return View(game);
        }

        [HttpGet]
        [ActionName("ByGenre")]
        public async Task<ActionResult> GetGamesByGenre(string key)
        {
            List<GameViewModel> games = null;
            try
            {
                var gamesBll = await _gameService.GetByGenreAsync(key);
                games = _mapperWeb.Map<List<GameViewModel>>(gamesBll);
            }
            catch (ValidationException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (AccessException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (Exception)
            {
                return View("Error", new HandleErrorInfo(new InvalidOperationException("Bad request"), "Game", "Index"));
            }

            return View(games);
        }

        [HttpGet]
        [ActionName("ByPlatformType")]
        public async Task<ActionResult> GetGamesByPlatformType(string key)
        {
            List<GameViewModel> games = null;
            try
            {
                var gamesBll = await _gameService.GetByPlatformTypeAsync(key);
                games = _mapperWeb.Map<List<GameViewModel>>(gamesBll);
            }
            catch (ValidationException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (AccessException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (Exception)
            {
                return View("Error", new HandleErrorInfo(new InvalidOperationException("Bad request"), "Game", "Index"));
            }

            return View(games);
        }

        [HttpGet]
        public async Task<ActionResult> GameComments(string gamekey)
        {
            List<CommentViewModel> comments = null;
            try
            {
                var commentsBll = await _commentService.GetCommentsAsync(gamekey);
                comments = _mapperWeb.Map<List<CommentViewModel>>(commentsBll);
            }
            catch (ValidationException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (AccessException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (Exception)
            {
                return View("Error", new HandleErrorInfo(new InvalidOperationException("Bad request"), "Game", "Index"));
            }

            return View(comments);
        }

        [HttpGet]
        [ActionName("Download")]
        public async Task<FileContentResult> GameDownload(string gamekey)
        {
            byte[] file = null;
            try
            {
                file = await Task.Run(() => _gameService.DownloadAsync(gamekey));
            }
            catch (ValidationException ex)
            {
                throw new HttpException(404, ex.Message);
            }
            catch (AccessException ex)
            {
                throw new HttpException(404, ex.Message);
            }
            catch (Exception)
            {
                throw new HttpException(404, "Bad request");
            }
            return File(file, MediaTypeNames.Application.Octet, gamekey);
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult NewGame()
        {
            var inputModel = new GameInputViewModel();
            return View("NewGame", inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("New")]
        public async Task<ActionResult> NewGame(GameInputViewModel game)
        {
            if (!ModelState.IsValid)
                return View(game);

            var gameBll = _mapperWeb.Map<GameBll>(game);
            try
            {
                await _gameService.AddAsync(gameBll);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(game);
            }
            catch (AccessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(game);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Bad request");
                return View(game);
            }
            // Success!!!
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditGame(int id )
        {
            var inputModel = new GameInputViewModel() {Id = id};
            return View("EditGame", inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<ActionResult> EditGame(GameInputViewModel game)
        {
            if (!ModelState.IsValid)
                return View(game);

            var gameBll = _mapperWeb.Map<GameBll>(game);
            try
            {
                await _gameService.UpdateAsync(gameBll);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(game);
            }
            catch (AccessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(game);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Bad request");
                return View(game);
            }
            // Success!!!
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string gameKey)
        {
            if (string.IsNullOrEmpty(gameKey))
                throw new HttpException(404, "Invalid key");

            try
            {
                await _gameService.DeleteAsync(gameKey);
            }
            catch (ValidationException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (AccessException ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Game", "Index"));
            }
            catch (Exception)
            {
                return View("Error", new HandleErrorInfo(new InvalidOperationException("Bad request"), "Game", "Index"));
            }
            // Success!!!
            return RedirectToAction("Index");
        }


        #region AJAX methods

        //// GET: Game
        //[HttpGet]
        //[Ajax]
        //[ActionName("Index")]
        //public async Task<JsonResult> GetAllGamesAjax()
        //{
        //    IEnumerable<GameViewModel> games = null;
        //    try
        //    {
        //        var gameBll = await _gameService.GetAllAsync();
        //        games = MapperWeb.Map<List<GameViewModel>>(gameBll);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (AccessException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new OperationResult(false), JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(games, JsonRequestBehavior.AllowGet);
        //}


        //[HttpGet]
        //[Ajax]
        //[ActionName("ByKey")]
        //public async Task<JsonResult> GetGamesByKeyAjax(string key)
        //{
        //    GameBll game = null;
        //    try
        //    {
        //        game = await _gameService.GetDetailsByKeyAsync(key);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (AccessException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new OperationResult(false), JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(game, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //[Ajax]
        //[ActionName("ByGenre")]
        //public async Task<JsonResult> GetGamesByGenreAjax(string key)
        //{
        //    IEnumerable<GameBll> games = null;
        //    try
        //    {
        //        games = await _gameService.GetByGenreAsync(key);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (AccessException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new OperationResult(false), JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(games, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //[Ajax(false)]
        //[ActionName("ByPlatformType")]
        //public async Task<JsonResult> GetGamesByPlatformTypeAjax(string key)
        //{
        //    IEnumerable<GameBll> games = null;
        //    try
        //    {
        //        games = await _gameService.GetByPlatformTypeAsync(key);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (AccessException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new OperationResult(false), JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(games, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        //[Ajax]
        [ActionName("Comments")]
        public async Task<JsonResult> GameCommentsAjax(string gamekey)
        {
            IEnumerable<CommentBll> comments = null;
            //try
            //{
                comments = await _commentService.GetCommentsAsync(gamekey);
            //}
            //catch (ValidationException ex)
            //{
            //    return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            //}
            //catch (AccessException ex)
            //{
            //    return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception)
            //{
            //    return Json(new OperationResult(false), JsonRequestBehavior.AllowGet);
            //}

            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[Ajax]
        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> NewAjax(NewGameViewModel game)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new OperationResult(false, "The game is not valid"), JsonRequestBehavior.AllowGet);

        //    var gameBll = MapperWeb.Map<GameBll>(game);
        //    try
        //    {
        //        await _gameService.AddAsync(gameBll);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (AccessException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new OperationResult(false, ""), JsonRequestBehavior.AllowGet);
        //    }
        //    // Success!!!
        //    return Json(new OperationResult(true, "The new game successfully added"), JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[Ajax]
        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> EditAjax(GameBll game)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new OperationResult(false, "The game is not valid"), JsonRequestBehavior.AllowGet);

        //    try
        //    {
        //        await _gameService.UpdateAsync(game);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (AccessException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new OperationResult(false, ""), JsonRequestBehavior.AllowGet);
        //    }
        //    // Success!!!
        //    return Json(new OperationResult(true, "The new game successfully updated"), JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[Ajax]
        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> DeleteAjax(string gameKey)
        //{
        //    if (string.IsNullOrEmpty(gameKey))
        //        return Json(new OperationResult(false, "Invalid key"), JsonRequestBehavior.AllowGet);

        //    try
        //    {
        //        await _gameService.DeleteAsync(gameKey);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (AccessException ex)
        //    {
        //        return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new OperationResult(false, ""), JsonRequestBehavior.AllowGet);
        //    }
        //    // Success!!!
        //    return Json(new OperationResult(true, "The new game successfully deleted"), JsonRequestBehavior.AllowGet);
        //}

        #endregion
    }
}
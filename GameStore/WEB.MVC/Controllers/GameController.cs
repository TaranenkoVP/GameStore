using System.Collections.Generic;
using System.Net.Mime;
using System.Web.Mvc;
using Bll.Core.Interfaces;
using BLL.Core.Entities;
using BLL.Core.Entities.GameBll;
using BLL.Infrastructure.CustomExceptions;
using Web.MVC.Common;
using Web.MVC.Models;
//using System.ComponentModel.DataAnnotations;

namespace Web.MVC.Controllers
{
    public class GameController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;

        public GameController(IGameService gameService, ICommentService commentService)
        {
            _gameService = gameService;
            _commentService = commentService;
        }

        // GET: Game
        [HttpGet]
        public JsonResult Index()
        {
            IEnumerable<GameBll> games = null;
            try
            {
                games = _gameService.GetAll();
            }
            catch (ValidationException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (AccessException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Show(string key)
        {
            GameDetailsBll game = null;
            try
            {
                game = _gameService.GetDetailsByKey(key);
            }
            catch (ValidationException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (AccessException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(game, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ByGenre(string key)
        {
            IEnumerable<GameBll> games = null;
            try
            {
                games = _gameService.GetByGenre(key);
            }
            catch (ValidationException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (AccessException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ByPlatformType(string key)
        {
            IEnumerable<GameBll> games = null;
            try
            {
                games = _gameService.GetByPlatformType(key);
            }
            catch (ValidationException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (AccessException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Comments(string gamekey)
        {
            IEnumerable<CommentBll> comments = null;
            try
            {
                //comments = _gameService.GetComments(gamekey);
                comments = _commentService.GetComments(gamekey);
            }
            catch (ValidationException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (AccessException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Download(string gamekey)
        {
            byte[] file = null;
            try
            {
                file = _gameService.Download(gamekey);
            }
            catch (ValidationException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (AccessException ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return File(file, MediaTypeNames.Application.Octet, gamekey);
        }

        [HttpPost]
        public ActionResult New(GameDetailsBll game)
        {
            if (game == null)
                return Json(new OperationResult(false, "The game not found"), JsonRequestBehavior.AllowGet);

            try
            {
                _gameService.Add(game);
            }
            catch (ValidationException ex)
            {
                return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
            catch (AccessException ex)
            {
                return Json(new OperationResult(false, ex.Message), JsonRequestBehavior.AllowGet);
            }

            return Json(new OperationResult(true, "The new game successfully added"), JsonRequestBehavior.AllowGet);
        }
    }
}
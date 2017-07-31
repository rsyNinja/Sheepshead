﻿using Sheepshead.Models;
using Sheepshead.Models.Players.Stats;
using Sheepshead.Models.Wrappers;
using System.Web.Http;

namespace Sheeshead.WebApi.Controllers
{
    public class GameController : ApiController
    {
        private static IRandomWrapper _rnd = new RandomWrapper();

        public string GetText()
        {
            return "Live long and prosper.";
        }

        [HttpPost]
        public IHttpActionResult Create(GameStartModel model)
        {
            var repository = new GameRepository(GameDictionary.Instance.Dictionary);
            var newGame = repository.CreateGame(model, _rnd, new LearningHelperFactory());
            repository.Save(newGame);
            newGame.RearrangePlayers();
            return Json("Success is ours!");
        }
    }
}

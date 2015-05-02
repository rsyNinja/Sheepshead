﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sheepshead.Models.Players;
using Sheepshead.Models.Players.Stats;

namespace Sheepshead.Models
{
    public class LearningHelper
    {
        private IMoveStatRepository _repository;
        private Dictionary<Tuple<ITrick, IPlayer>, MoveStatUniqueKey> _learningKeys = new Dictionary<Tuple<ITrick, IPlayer>, MoveStatUniqueKey>();

        private LearningHelper()
        {
        }

        public LearningHelper(IMoveStatRepository repository, IHand hand)
        {
            _repository = repository;
            hand.OnAddTrick += OnAddTrickToHand;
            hand.OnHandEnd += OnHandEnd;
        }

        public static MoveStatUniqueKey GenerateKey(ITrick trick, IPlayer player, ICard legalCard)
        {
            List<IPlayer> playerList = trick.Hand.Players;
            return new MoveStatUniqueKey()
            {
                Picker = trick.QueueRankOfPicker,
                Partner = trick.QueueRankOfPartner,
                Trick = trick.IndexInHand,
                MoveWithinTrick = player.QueueRankInTrick(trick),
                PointsAlreadyInTrick = trick.CardsPlayed.Sum(c => c.Value.Points),
                TotalPointsInPreviousTricks = trick.Hand.Tricks.Where(t => t != trick).Sum(t => t.CardsPlayed.Sum(c => c.Value.Points)),
                PointsInThisCard = legalCard.Points,
                RankOfThisCard = legalCard.Rank,
                PartnerCard = trick.Hand.PartnerCard == legalCard,
                HigherRankingCardsPlayedPreviousTricks =
                    trick.Hand.Tricks
                    .Where(t => t != trick) //SUSPECT: so this will break if the method is ever called when "trick" is not the most recent trick.
                    .SelectMany(t => t.CardsPlayed.Select(kvp => kvp.Value))
                    .Count(c => c.Rank < legalCard.Rank),
                HigherRankingCardsPlayedThisTrick =
                    trick.CardsPlayed
                    .Select(kvp => kvp.Value)
                    .Count(c => c.Rank < legalCard.Rank)
            };
        }

        private void OnAddTrickToHand(object sender, EventArgs e)
        {
            var hand = (IHand)sender;
            hand.Tricks.Last().OnTrickEnd += OnTrickEnd;
        }

        private void OnHandEnd(object sender, EventArgs e)
        {
            var hand = (IHand)sender;
            foreach (var trick in hand.Tricks)
            {
                foreach (var player in trick.Players)
                {
                    var tuple = new Tuple<ITrick, IPlayer>(trick, player);
                    var statKey = _learningKeys[tuple];
                    _repository.IncrementHandResult(statKey, trick.Hand.Scores()[player] > 0);
                }
            }
        }

        private void OnTrickMove(object sender, Trick.MoveEventArgs e)
        {
            var trick = (ITrick)sender;
            var tuple = new Tuple<ITrick, IPlayer>(trick, e.Player);
            _learningKeys.Add(tuple, LearningHelper.GenerateKey(trick, e.Player, e.Card));
        }

        private void OnTrickEnd(object sender, EventArgs e)
        {
            var trick = (ITrick)sender;
            foreach (var player in trick.Players)
            {
                var tuple = new Tuple<ITrick, IPlayer>(trick, player);
                var statKey = _learningKeys[tuple];
                _repository.IncrementTrickResult(statKey, trick.Winner().Player == this);
            }
        }
    }
}
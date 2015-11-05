﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sheepshead.Models.Players;

namespace Sheepshead.Models.Players.Stats
{
    public interface IPickKeyGenerator
    {
        PickStatUniqueKey GenerateKey(IHand hand, IPlayer player);
    }

    public class PickKeyGenerator : IPickKeyGenerator
    {
        public PickStatUniqueKey GenerateKey(IHand hand, IPlayer player)
        {
            var cardsPlayed = hand == null || hand.Tricks == null
                ? new List<ICard>()
                : hand.Tricks.Select(t => t.CardsPlayed[player]).Where(c => c != null);
            var cardsBuried = hand == null || hand.Deck.Buried == null ? new List<ICard>() : hand.Deck.Buried;
            var blinds = hand == null || hand.Deck.Blinds == null ? new List<ICard>() : hand.Deck.Blinds;
            var cards = player.Cards
                .Union(cardsPlayed)
                .Union(cardsBuried)
                .Where(c => !blinds.Contains(c))
                .ToList();
            var trumpCards = cards.Where(c => CardRepository.GetSuit(c) == Suit.TRUMP).ToList();
            var trumpCount = trumpCards.Count();
            var trumpRankAvg = trumpCards.Any() ? trumpCards.Average(c => c.Rank) : 0;
            return new PickStatUniqueKey()
            {
                TrumpCount = trumpCount,
                AvgTrumpRank = (int)Math.Round(trumpRankAvg),
                AvgPointsInHand = (int)Math.Round(cards.Average(c => c.Points)),
                TotalCardsWithPoints = cards.Count(c => c.Points > 0)
            };
        }
    }
}
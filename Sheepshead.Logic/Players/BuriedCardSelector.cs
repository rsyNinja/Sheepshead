﻿using System.Collections.Generic;
using System.Linq;

namespace Sheepshead.Logic.Players
{
    public class BuriedCardSelector
    {
        public Dictionary<Suit, int> CardsPerSuit { get; }
        private List<IGrouping<Suit, SheepCard>> _acesAndTensPerSuit { get; }
        private IReadOnlyList<SheepCard> _cards;
        private List<SheepCard> _acesAndTens;

        public BuriedCardSelector(IReadOnlyList<SheepCard> cards)
        {
            CardsPerSuit = cards
                .Where(c => CardUtil.GetSuit(c) != Suit.TRUMP)
                .GroupBy(c => CardUtil.GetSuit(c))
                .ToDictionary(g => g.Key, g => g.Count());
            _cards = cards;
            _acesAndTens = cards
                .Where(c => CardUtil.GetSuit(c) != Suit.TRUMP)
                .Where(c => new[] { CardType.ACE, CardType.N10 }.Contains(CardUtil.GetFace(c)))
                .ToList();
            _acesAndTensPerSuit = _acesAndTens
                .GroupBy(c => CardUtil.GetSuit(c))
                .Where(g => g.Key != Suit.TRUMP)
                .OrderBy(g => g.Count())
                .ToList();
        }

        public List<SheepCard> CardsToBury
        {
            get
            {
                return GetTwoFailAceOrTens()
                    ?? RetireTwoFailSuitsWithOneAceOrTen()
                    ?? RetireOneFailSuitsWithOneAceOrTen()
                    ?? RetireTwoFailSuits()
                    ?? RetireOneFailSuit()
                    ?? RetireOneFailOrLowestRankCards();
            }
        }

        public List<SheepCard> GetTwoFailAceOrTens()
        {
            if (_acesAndTens.Count < 2)
                return null;

            var oneCardSuits = _acesAndTensPerSuit
                .Where(g => g.Count() == 1)
                .Where(g => CardsPerSuit[g.Key] == 1)
                .ToList();
            if (oneCardSuits.Count >= 2)
                return oneCardSuits.SelectMany(g => g).Take(2).ToList();

            var twoCardSuits = _acesAndTensPerSuit
                .Where(g => g.Count() == 2)
                .Where(g => CardsPerSuit[g.Key] == 2)
                .ToList();
            if (twoCardSuits.Any())
                return twoCardSuits.First().Select(g => g).ToList();

            var orderedCards = _acesAndTensPerSuit
                .OrderBy(g => CardsPerSuit[g.Key])
                .SelectMany(g => g)
                .ToList();
            return orderedCards.Take(2).ToList();
        }

        public List<SheepCard> RetireTwoFailSuitsWithOneAceOrTen()
        {
            if (_acesAndTens.Count != 1)
                return null;

            var pointSuit = _acesAndTensPerSuit.Single(g => g.Key != Suit.TRUMP).Key;
            var oneCardSuits = CardsPerSuit
                .Where(cps => cps.Value == 1)
                .OrderBy(cps => cps.Key == pointSuit ? 1 : 2)
                .Select(cps => cps.Key)
                .Take(2)
                .ToList();
            var buryCards = _cards.Where(c => oneCardSuits.Contains(CardUtil.GetSuit(c))).ToList();
            if (buryCards.Count != 2)
                return null;
            if (!buryCards.Contains(_acesAndTens.Single()))
                return null;
            return buryCards;
        }

        public List<SheepCard> RetireOneFailSuitsWithOneAceOrTen()
        {
            if (_acesAndTens.Count != 1)
                return null;

            var pointSuit = _acesAndTensPerSuit.Single(g => g.Key != Suit.TRUMP).Key;
            var twoCardSuit = CardsPerSuit
                .Where(cps => cps.Value == 2)
                .Where(cps => cps.Key == pointSuit)
                .Select(cps => cps.Key)
                .FirstOrDefault();
            var buryCards = _cards.Where(c => CardUtil.GetSuit(c) == twoCardSuit).ToList();
            if (buryCards.Count != 2)
                return null;
            if (!buryCards.Contains(_acesAndTens.Single()))
                return null;
            return buryCards;
        }

        public List<SheepCard> RetireTwoFailSuits()
        {
            var buryCards = _cards
                .Where(c => CardUtil.GetSuit(c) != Suit.TRUMP)
                .Where(c => CardsPerSuit[CardUtil.GetSuit(c)] == 1)
                .Take(2)
                .ToList();
            if (buryCards.Count == 2)
                return buryCards;
            return null;
        }

        public List<SheepCard> RetireOneFailSuit()
        {
            var buryCards = _cards
                .Where(c => CardUtil.GetSuit(c) != Suit.TRUMP)
                .Where(c => CardsPerSuit[CardUtil.GetSuit(c)] == 2)
                .GroupBy(c => CardUtil.GetSuit(c))
                .ToList();
            if (buryCards.Any())
                return buryCards.First().ToList();
            return null;
        }

        public List<SheepCard> RetireOneFailOrLowestRankCards()
        {
            var groups = _cards
                .GroupBy(c => CardUtil.GetSuit(c))
                .OrderBy(g => g.Key != Suit.TRUMP ? CardsPerSuit[g.Key] : int.MaxValue);
            var retVal = new List<SheepCard>();
            foreach (var group in groups)
                retVal.AddRange(group.OrderByDescending(c => CardUtil.GetRank(c)));
            return retVal.Take(2).ToList();
        }
    }
}

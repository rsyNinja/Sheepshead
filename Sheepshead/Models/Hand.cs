﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheepshead.Models
{
    public class Hand : IHand
    {
        public IDeck Deck { get; private set; }
        public IPlayer Picker { get; private set; }
        public IPlayer Partner { set; get; }
        public ICard PartnerCard { get; private set; }
        private List<ITrick> _tricks = new List<ITrick>();

        public Hand(IDeck deck, IPlayer picker)
        {
            Deck = deck;
            Deck.Hand = this;
            Picker = picker;
            PartnerCard = ChoosePartnerCard(picker);
        }

        private ICard ChoosePartnerCard(IPlayer picker)
        {
            var cri = CardRepository.Instance;
            var potentialPartnerCards = new[] { 
                cri[StandardSuite.DIAMONDS, CardType.JACK],
                cri[StandardSuite.HEARTS, CardType.JACK],
                cri[StandardSuite.SPADES, CardType.JACK],
                cri[StandardSuite.CLUBS, CardType.JACK],
                cri[StandardSuite.DIAMONDS, CardType.QUEEN] 
            };
            if (Deck.Hand != null)
                throw new DeckHasHandException("Must add a hand to a deck without one.");
            var partnerCard = potentialPartnerCards.First(c => !picker.Cards.Contains(c));
            return partnerCard;
        }

        public void AddTrick(ITrick trick)
        {
            _tricks.Add(trick);
        }

        public Dictionary<IPlayer, int> Scores()
        {
            var pickerPoints = 0;
            var defensePoints = 0;
            foreach (var trick in _tricks)
            {
                var winnerData = trick.Winner();
                if (winnerData.Player == Picker || winnerData.Player == Partner)
                    pickerPoints += winnerData.Points;
                else
                    defensePoints += winnerData.Points;
            }
            int defensiveHandPoints;
            if (defensePoints == 0)
                defensiveHandPoints = -3;
            else if (defensePoints <= 29)
                defensiveHandPoints = -2;
            else if (defensePoints <= 59)
                defensiveHandPoints = -1;
            else
                defensiveHandPoints = 2;

            var dict = new Dictionary<IPlayer, int>();
            foreach (var player in Deck.Game.Players)
            {
                if (player == Picker)
                    dict.Add(player, defensiveHandPoints * -2);
                else if (player == Partner)
                    dict.Add(player, defensiveHandPoints * -1);
                else
                    dict.Add(player, defensiveHandPoints);
            }
            return dict;
        }

        public bool IsComplete()
        {
            return Deck.Game.Players.Sum(p => p.Cards.Count) == 0;
        }
    }

    public interface IHand
    {
        IDeck Deck { get; }
        IPlayer Picker { get; }
        IPlayer Partner { set; get; }
        ICard PartnerCard { get; }
        void AddTrick(ITrick trick);
        Dictionary<IPlayer, int> Scores();
        bool IsComplete();
    }

    public class DeckHasHandException : ApplicationException
    {
        public DeckHasHandException(string message)
            : base(message)
        {
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sheepshead.Model.Players;


namespace Sheepshead.Model
{
    public class Trick : ITrick
    {
        private Dictionary<IPlayer, SheepCard> _cards = new Dictionary<IPlayer, SheepCard>();

        public IHand Hand { get; private set; }
        public IGame Game { get { return Hand.Game; } }
        public IPlayer StartingPlayer { get; private set; }
        public Dictionary<IPlayer, SheepCard> CardsPlayed { get { return new Dictionary<IPlayer, SheepCard>(_cards); } }
        public event EventHandler<EventArgs> OnTrickEnd;
        public event EventHandler<MoveEventArgs> OnMove;

        public List<KeyValuePair<IPlayer, SheepCard>> OrderedMoves 
        { 
            get 
            {
                var indexOfStartingPlayer = Players.IndexOf(StartingPlayer);
                var playerList = Players.Skip(indexOfStartingPlayer).Union(Players.Take(indexOfStartingPlayer)).ToList();
                var orderedMoves = new List<KeyValuePair<IPlayer, SheepCard>>();
                foreach (var player in playerList)
                    if (_cards.ContainsKey(player))
                        orderedMoves.Add(new KeyValuePair<IPlayer, SheepCard>( player, _cards[player] ));
                return orderedMoves;
            } 
        }

        public Trick(IHand hand) : this (hand, new StartingPlayerCalculator())
        {
        }

        public Trick(IHand hand, IStartingPlayerCalculator startingPlayerCalculator)
        {
            Hand = hand;
            Hand.AddTrick(this);
            StartingPlayer = startingPlayerCalculator.GetStartingPlayer(hand, this);
        }

        public void Add(IPlayer player, SheepCard card)
        {
            _cards.Add(player, card);
            player.Cards.Remove(card);
            if (Hand.PartnerCard == card)
                Hand.SetPartner(player, this);
            OnMoveHandler(player, card);
            if (IsComplete())
                OnTrickEndHandler();
        }

        public bool IsLegalAddition(SheepCard card, IPlayer player)
        {
            //In the last trick, anything is legal.
            if (player.Cards.Count() == 1)
                return true;

            //There are some rules for the lead card in a trick.
            if (!_cards.Any())
                return Hand.Game.PartnerMethod == PartnerMethod.JackOfDiamonds 
                    || Hand.PartnerCard == null
                    || IsLegalStartingCardInCalledAceGame(card, player);

            //Other cards must follow suit.
            var firstCard = _cards.First().Value;
            return player.Cards.Contains(card) 
                && (CardUtil.GetSuit(card) == CardUtil.GetSuit(firstCard) || !player.Cards.Any(c => CardUtil.GetSuit(c) == CardUtil.GetSuit(firstCard)));
        }

        private bool IsLegalStartingCardInCalledAceGame(SheepCard card, IPlayer player)
        {
            var suitOfPartnerCard = CardUtil.GetSuit(Hand.PartnerCard.Value);
            //Once suit of partner card is lead, picker and partner may lead with that suit.
            if (Hand.Tricks != null
                && Hand.Tricks.Any(t => t != this && t.CardsPlayed.Any() && CardUtil.GetSuit(t.CardsPlayed.First().Value) == suitOfPartnerCard))
                return true;
            //Picker cannot lead with last card of Called Ace's suit.
            if (player == Hand.Picker
                && CardUtil.GetSuit(card) == suitOfPartnerCard
                && player.Cards.Union(Hand.Deck.Buried).ToList().Count(c => CardUtil.GetSuit(c) == suitOfPartnerCard) == 1)
                return false;
            //Partner cannot lead with partner card.
            if (Hand.PartnerCard == card)
                return false;
            return true;
        }

        public TrickWinner Winner()
        {
            if (!_cards.Any())
                return null;
            var firstSuite = CardUtil.GetSuit(_cards.First().Value);
            var validCards = new List<KeyValuePair<IPlayer, SheepCard>>();
            foreach(var keyValuePair in _cards) {
                var suite = CardUtil.GetSuit(keyValuePair.Value);
                if (suite == firstSuite || suite == Suit.TRUMP)
                    validCards.Add(keyValuePair);
            }
            return new TrickWinner()
            {
                Player = validCards.OrderBy(kvp => CardUtil.GetRank(kvp.Value)).First().Key,
                Points = _cards.Sum(c => CardUtil.GetPoints(c.Value))
            };
        }

        public class MoveEventArgs : EventArgs
        {
            public IPlayer Player;
            public SheepCard Card;
        }

        protected virtual void OnMoveHandler(IPlayer player, SheepCard card)
        {
            var e = new MoveEventArgs()
            {
                Player = player,
                Card = card
            };
            OnMove?.Invoke(this, e);
        }

        protected virtual void OnTrickEndHandler()
        {
            var e = new EventArgs();
            OnTrickEnd?.Invoke(this, e);
        }

        public bool IsComplete()
        {
            return CardsPlayed.Count() == Hand.PlayerCount;
        }

        public int PlayerCount
        {
            get { return Hand.Deck.Game.PlayerCount; }
        }

        public List<IPlayer> Players
        {
            get { return Hand.Players; }
        }

        public int QueueRankOfPicker
        {
            get { return Hand.Picker.QueueRankInTrick(this); }
        }

        public int? QueueRankOfPartner
        {
            get { return Hand.Partner == null ? (int?)null : Hand.Partner.QueueRankInTrick(this); } 
        }

        public int IndexInHand
        {
            get { return Hand.Tricks.IndexOf(this); }
        }

        public SheepCard? PartnerCard { get { return Hand.PartnerCard; } }

        public List<IPlayer> PlayersInTurnOrder => PickPlayerOrderer.PlayersInTurnOrder(Players, StartingPlayer);
        public List<IPlayer> PlayersWithoutTurn => PickPlayerOrderer.PlayersWithoutTurn(PlayersInTurnOrder, CardsPlayed.Keys.ToList());

        private IPlayerOrderer _pickPlayerOrderer;
        public IPlayerOrderer PickPlayerOrderer
        {
            get { return _pickPlayerOrderer ?? (_pickPlayerOrderer = new PlayerOrderer()); }
        }
    }

    public class TrickWinner {
        public IPlayer Player;
        public int Points;
    }

    public interface ITrick
    {
        IHand Hand { get; }
        IGame Game { get; }
        TrickWinner Winner();
        void Add(IPlayer player, SheepCard card);
        bool IsLegalAddition(SheepCard card, IPlayer player);
        IPlayer StartingPlayer { get; }
        Dictionary<IPlayer, SheepCard> CardsPlayed { get; }
        bool IsComplete();
        int PlayerCount { get; }
        List<IPlayer> Players { get; }
        int QueueRankOfPicker { get; }
        int? QueueRankOfPartner { get; }
        int IndexInHand { get; }
        SheepCard? PartnerCard { get; }
        List<KeyValuePair<IPlayer, SheepCard>> OrderedMoves { get; }
        event EventHandler<EventArgs> OnTrickEnd;
        List<IPlayer> PlayersWithoutTurn { get; }
        IPlayerOrderer PickPlayerOrderer { get; }
    }
}
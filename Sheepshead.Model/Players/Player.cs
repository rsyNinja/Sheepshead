﻿using System;
using System.Collections.Generic;
using System.Linq;

using Sheepshead.Models.Players.Stats;

namespace Sheepshead.Models.Players
{
    public class Player : IPlayer
    {
        private List<ICard> _hand = new List<ICard>();

        public virtual string Name { get { return String.Empty; } }
        public List<ICard> Cards { get { return _hand; } }

        public int QueueRankInTrick(ITrick trick)
        {
            var indexOfMe = trick.Players.IndexOf(this);
            var indexOfStartingPlayer = trick.Players.IndexOf(trick.StartingPlayer);
            var rank = indexOfMe - indexOfStartingPlayer;
            if (rank < 0) rank += trick.PlayerCount;
            return rank + 1;
        }

        public int QueueRankInDeck(IDeck deck)
        {
            var indexOfMe = deck.Players.IndexOf(this);
            var indexOfStartingPlayer = deck.Players.IndexOf(deck.StartingPlayer);
            var rank = indexOfMe - indexOfStartingPlayer;
            if (rank < 0) rank += deck.PlayerCount;
            return rank + 1;
        }
    }

    public interface IPlayer
    {
        string Name { get; }
        List<ICard> Cards { get; }
        int QueueRankInTrick(ITrick trick);
        int QueueRankInDeck(IDeck deck);
    }
}
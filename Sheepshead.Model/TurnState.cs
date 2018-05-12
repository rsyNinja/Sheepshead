﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sheepshead.Models.Players;

namespace Sheepshead.Models
{
    public class TurnState
    {
        public Guid GameId { get; set; }
        public IHumanPlayer HumanPlayer { get; set; }
        public TurnType TurnType { get; set; }
        public IDeck Deck { get; set; }
    }

    public enum TurnType
    {
        BeginDeck, Pick, Bury, PlayTrick
    }
}
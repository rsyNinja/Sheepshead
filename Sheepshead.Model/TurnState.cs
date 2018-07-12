﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sheepshead.Models.Players;

namespace Sheepshead.Models
{
    public class TurnState
    {
        public Guid GameId { get; set; }
        public TurnType TurnType { get; set; }
        public IDeck Deck { get; set; }
    }

    public enum TurnType
    {
        BeginDeck, Pick, Bury, PlayTrick
    }

    /// <summary>
    /// Like TurnState, but serializable.
    /// </summary>
    public class PlayState
    {
        public string TurnType { get; set; }
        public bool HumanTurn { get; set; }
        public string CurrentTurn { get; set; }
        public bool RequestingPlayerTurn { get; set; }
        public List<CardSummary> Blinds { get; set; }
        //Player name and whether or not the picked
        public List<Tuple<string, bool>> PickChoices { get; set; }
        //Player name and card filename
        public List<List<Tuple<string, CardSummary>>> CardsPlayed { get; set; }
        //Filename number for the player's cards.
        public List<CardSummary> PlayerCards { get; set; }
    }

    public class BuryState
    {
        public string TurnType { get; set; }
        public bool RequestingPlayerTurn { get; set; }
        public List<CardSummary> Blinds { get; set; }
        //Filename number for the player's cards.
        public List<CardSummary> PlayerCards { get; set; }
        public List<CardSummary> LegalCalledAces { get; set; }
        public string PartnerMethod { get; set; }
    }
}
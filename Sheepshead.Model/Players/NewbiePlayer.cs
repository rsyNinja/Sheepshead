﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace Sheepshead.Models.Players
{
    public class NewbiePlayer : ComputerPlayer
    {
        public override ICard GetMove(ITrick trick)
        {
            return this.Cards.First(c => trick.IsLegalAddition(c, this));
        }

        public override bool WillPick(IDeck deck)
        {
            return QueueRankInDeck(deck) == deck.PlayerCount;
        }

        protected override List<ICard> DropCardsForPickInternal(IDeck deck)
        {
            return Cards.OrderByDescending(c => c.Rank).Take(2).ToList();
        }
    }
}
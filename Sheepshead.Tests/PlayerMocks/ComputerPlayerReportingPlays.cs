﻿using System;
using System.Collections.Generic;
using Sheepshead.Model;
using Sheepshead.Model.Players;

namespace Sheepshead.Tests.PlayerMocks
{
    class ComputerPlayerReportingPlays : IComputerPlayer
    {
        public bool MadeMove { get; set; }
        private SheepCard _moveToMake;
        public string Name => throw new NotImplementedException();

        public List<SheepCard> Cards => throw new NotImplementedException();

        string IPlayer.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ComputerPlayerReportingPlays(SheepCard moveToMake)
        {
            _moveToMake = moveToMake;
        }

        public List<SheepCard> DropCardsForPick(IHand hand)
        {
            throw new NotImplementedException();
        }

        public SheepCard GetMove(ITrick trick)
        {
            MadeMove = true;
            return _moveToMake;
        }

        public int QueueRankInHand(IHand hand)
        {
            throw new NotImplementedException();
        }

        public int QueueRankInTrick(ITrick trick)
        {
            throw new NotImplementedException();
        }

        public bool WillPick(IHand hand)
        {
            throw new NotImplementedException();
        }

        public SheepCard? ChooseCalledAce(IHand hand)
        {
            throw new NotImplementedException();
        }

        public List<SheepCard> LegalCalledAces(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool GoItAlone(IHand hand)
        {
            return false;
        }
    }
}

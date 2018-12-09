﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheepshead.Model
{
    public interface IGameStateDescriber
    {
        List<IHand> Hands { get; }
        IHand CurrentHand { get; }
        ITrick CurrentTrick { get; }
        TurnType GetTurnType();
        bool LastHandIsComplete();
    }

    public class GameStateDescriber : IGameStateDescriber
    {
        public List<IHand> Hands { get; } = new List<IHand>();

        public IHand CurrentHand => LastHandIsComplete() ? null : Hands.Last();

        public ITrick CurrentTrick {
            get {
                var trick = Hands.LastOrDefault()?.Tricks?.LastOrDefault();
                if (trick == null || trick.IsComplete() && !Hands.LastOrDefault().IsComplete())
                    trick = new Trick(Hands.LastOrDefault());
                return trick;
            }
        }

        public TurnType GetTurnType()
        {
            var deck = Hands.LastOrDefault();
            if (!Hands.Any() || LastHandIsComplete())
                return TurnType.BeginHand;
            else if (!deck.PickPhaseComplete)
                return TurnType.Pick;
            else if (!deck.Buried.Any() && !deck.Leasters)
                return TurnType.Bury;
            else
                return TurnType.PlayTrick;
        }

        public bool LastHandIsComplete()
        {
            var lastHand = Hands.LastOrDefault();
            return lastHand == null || lastHand.IsComplete();
        }
    }
}

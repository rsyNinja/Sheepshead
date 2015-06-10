﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Sheepshead.Models.Wrappers;
using System.Web.Script.Serialization;
using System.Timers;

namespace Sheepshead.Models.Players.Stats
{
    public interface IMoveStatRepository
    {
        List<MoveStatUniqueKey> Keys { get; }
        void IncrementTrickResult(MoveStatUniqueKey key, bool wonTrick);
        void IncrementHandResult(MoveStatUniqueKey key, bool wonGame);
        MoveStat GetRecordedResults(MoveStatUniqueKey key);
    }

    public class MoveStatRepository : StatRepository<MoveStatUniqueKey, MoveStat>, IMoveStatRepository
    {
        protected override MoveStat CreateDefaultStat()
        {
            return new MoveStat();
        }

        public static MoveStatRepository FromFile(IStreamReaderWrapper streamReader)
        {
            var instance = new MoveStatRepository();
            return (MoveStatRepository)StatRepository<MoveStatUniqueKey, MoveStat>.FromFile(instance, streamReader);
        }

        public void IncrementTrickResult(MoveStatUniqueKey key, bool wonTrick)
        {
            if (!_dict.ContainsKey(key))
                _dict[key] = new MoveStat();
            if (wonTrick)
                ++_dict[key].TricksWon;
            ++_dict[key].TricksTried;
        }

        public void IncrementHandResult(MoveStatUniqueKey key, bool wonGame)
        {
            if (!_dict.ContainsKey(key))
                _dict[key] = new MoveStat();
            if (wonGame)
                ++_dict[key].HandsWon;
            ++_dict[key].HandsTried;
        }
    }
}

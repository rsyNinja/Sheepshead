﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sheepshead.Models.Wrappers;

namespace Sheepshead.Models.Players.Stats
{
    public class RepositoryRepository
    {
        public static string MOVE_SAVE_LOCATION = @"c:\temp\game-stat.json";

        private static RepositoryRepository _instance = new RepositoryRepository();

        private RepositoryRepository()
        {
            using (var reader = new StreamReaderWrapper(MOVE_SAVE_LOCATION))
                MoveStatRepository = MoveStatRepository.FromFile(reader);
        }

        internal static RepositoryRepository Instance { get { return _instance; } }

        public MoveStatRepository MoveStatRepository { get; private set; }
    }
}
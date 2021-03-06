﻿using System;
using System.Collections.Generic;

namespace Sheepshead.Logic.Models
{
    public partial class Trick
    {
        public Trick()
        {
            TrickPlays = new HashSet<TrickPlay>();
        }

        public int Id { get; set; }
        public int HandId { get; set; }
        public int StartingParticipantId { get; set; }

        public Hand Hand { get; set; }
        public Participant StartingParticipant { get; set; }
        public ICollection<TrickPlay> TrickPlays { get; set; }
        public int SortOrder { get; set; }
    }
}

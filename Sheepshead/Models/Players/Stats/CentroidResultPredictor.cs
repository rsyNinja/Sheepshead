﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheepshead.Models.Players.Stats
{
    public class CentroidResultPredictor
    {
        private Dictionary<MoveStatCentroid, MoveStat> _centroidAndStats;

        private CentroidResultPredictor() { }

        public CentroidResultPredictor(Dictionary<MoveStatCentroid, MoveStat> centroidAndStats)
        {
            _centroidAndStats = centroidAndStats;
        }

        public MoveStat GetPrediction(MoveStatUniqueKey key)
        {
            var nearestCentroid = GetNearestCentroid(key);
            return _centroidAndStats[nearestCentroid];
        }

        private MoveStatCentroid GetNearestCentroid(MoveStatUniqueKey key)
        {
            var minDistance = Double.MaxValue;
            MoveStatCentroid bestMatch = new MoveStatCentroid();
            foreach (var centroid in _centroidAndStats.Keys)
            {
                var curDistance = ClusterUtils.Distance(key, centroid);
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    bestMatch = centroid;
                }
            }
            if (minDistance == Double.MaxValue)
                throw new ApplicationException("No centroid found.");
            return bestMatch;
        }
    }
}
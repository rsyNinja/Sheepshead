﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheepshead.Models.Players.Stats
{
    public class ClusterUtils
    {
        public static double Distance(MoveStatUniqueKey2 tuple, MoveStatCentroid centroid)
        {
            double sumSquareDiffs = 0.0;
            sumSquareDiffs += Math.Pow((tuple.PointsInTrick - centroid.PointsInTrick), 2);
            sumSquareDiffs += Math.Pow((tuple.HighestRankInTrick - centroid.HighestRankInTrick), 2);
            sumSquareDiffs += Math.Pow((tuple.MorePowerfulUnknownCards - centroid.MorePowerfulUnknownCards), 2);
            sumSquareDiffs += Math.Pow((tuple.RemainingUnknownPoints - centroid.RemainingUnknownPoints), 2);
            sumSquareDiffs += Math.Pow((tuple.MorePowerfulHeld - centroid.MorePowerfulHeld), 2);
            sumSquareDiffs += Math.Pow((tuple.PointsHeld - centroid.PointsHeld), 2);
            sumSquareDiffs += Math.Pow((tuple.CardsHeldWithPoints - centroid.CardsHeldWithPoints), 2);
            sumSquareDiffs += Math.Pow((tuple.MoveIndex - centroid.MoveIndex), 2);
            sumSquareDiffs += Math.Pow((tuple.TrickIndex - centroid.TrickIndex), 2);
            return Math.Sqrt(sumSquareDiffs);
        }

        public static double Distance(MoveStatCentroid tuple, MoveStatCentroid centroid)
        {
            double sumSquareDiffs = 0.0;
            sumSquareDiffs += Math.Pow((tuple.PointsInTrick - centroid.PointsInTrick), 2);
            sumSquareDiffs += Math.Pow((tuple.HighestRankInTrick - centroid.HighestRankInTrick), 2);
            sumSquareDiffs += Math.Pow((tuple.MorePowerfulUnknownCards - centroid.MorePowerfulUnknownCards), 2);
            sumSquareDiffs += Math.Pow((tuple.RemainingUnknownPoints - centroid.RemainingUnknownPoints), 2);
            sumSquareDiffs += Math.Pow((tuple.MorePowerfulHeld - centroid.MorePowerfulHeld), 2);
            sumSquareDiffs += Math.Pow((tuple.PointsHeld - centroid.PointsHeld), 2);
            sumSquareDiffs += Math.Pow((tuple.CardsHeldWithPoints - centroid.CardsHeldWithPoints), 2);
            sumSquareDiffs += Math.Pow((tuple.MoveIndex - centroid.MoveIndex), 2);
            sumSquareDiffs += Math.Pow((tuple.TrickIndex - centroid.TrickIndex), 2);
            return Math.Sqrt(sumSquareDiffs);
        }

        public static Dictionary<int, Dictionary<MoveStatCentroid, MoveStat>> GetClusterDictionary(Dictionary<MoveStatUniqueKey2, MoveStat> moveResults, Dictionary<int, ClusterResult> clusterResultRooms)
        {
            var dictByRoom = new Dictionary<int, Dictionary<MoveStatCentroid, MoveStat>>();
            for (var j = 0; j < clusterResultRooms.Count(); ++j)
            {
                var clusterResult = clusterResultRooms.ElementAt(j).Value;
                var roomNo = clusterResultRooms.ElementAt(j).Key;
                var statDict = new Dictionary<MoveStatCentroid, MoveStat>();
                for (var i = 0; i < clusterResult.Data.Count(); ++i)
                {
                    var centroid = clusterResult.GetCentroid(i);
                    if (!statDict.ContainsKey(centroid))
                        statDict.Add(centroid, new MoveStat());
                    var key = clusterResult.Data[i];
                    var existingStat = moveResults[key];
                    statDict[centroid].TricksTried += existingStat.TricksTried;
                    statDict[centroid].TricksWon += existingStat.TricksWon;
                    statDict[centroid].HandsTried += existingStat.HandsTried;
                    statDict[centroid].HandsWon += existingStat.HandsWon;
                }
                dictByRoom.Add(roomNo, statDict);
            }
            return dictByRoom;
        }
    }
}
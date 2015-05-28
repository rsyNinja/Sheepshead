﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sheepshead.Models;
using Sheepshead.Models.Wrappers;
using Sheepshead.Models.Players.Stats;

namespace Sheepshead.Tests
{
    [TestClass]
    public class ClusterTests
    {
        [TestMethod]
        public void GetClusterDictionary()
        {
            var roomNo = 32;
            var clusterResult = new Dictionary<int, ClusterResult>() {
                { roomNo, GetClusterResult() }
            };
            var expectedDict = new Dictionary<MoveStatCentroid, MoveStat>() {
                { centroid1, new MoveStat() { TricksTried = 4, TricksWon = 4, HandsTried = 4, HandsWon = 0 } },
                { centroid2, new MoveStat() { TricksTried = 3, TricksWon = 0, HandsTried = 3, HandsWon = 3 } },
                { centroid3, new MoveStat() { TricksTried = 3, TricksWon = 2, HandsTried = 3, HandsWon = 2 } },
            };
            var statList = GetStatList(clusterResult[roomNo], 4);
            var actualDict = ClusterUtils.GetClusterDictionary(statList, clusterResult);
            Assert.AreEqual(expectedDict.Count, actualDict[roomNo].Count, "Dictionaries must be the same size.");
            var i = 0;
            foreach (var key in expectedDict.Keys)
            {
                Assert.AreEqual(expectedDict[key].TrickPortionWon, actualDict[roomNo][key].TrickPortionWon, "Trick portion won must match during iteration " + i + ".");
                Assert.AreEqual(expectedDict[key].HandPortionWon, actualDict[roomNo][key].HandPortionWon, "Hand portion won must match during iteration " + i + ".");
                ++i;
            }
        }

        private ClusterResult GetClusterResult()
        {
            return new ClusterResult()
            {
                Data = new List<MoveStatUniqueKey2>()
                {
                    GetNearbyKey(centroid2, 2.6),
                    GetNearbyKey(centroid3, 1.6),
                    GetNearbyKey(centroid1, -0.6),
                    GetNearbyKey(centroid1, -1.2),
                    GetNearbyKey(centroid3, 0.75),
                    GetNearbyKey(centroid2, 0.3),
                    GetNearbyKey(centroid3, -1.4),
                    GetNearbyKey(centroid1, -0.8),
                    GetNearbyKey(centroid2, 1.7),
                    GetNearbyKey(centroid1, -1.6),
                },
                ClusterIndicies = new int[] { 1, 2, 0, 0, 2, 1, 2, 0, 1, 0 },
                Centroids = new List<MoveStatCentroid>() { centroid1, centroid2, centroid3 }
            };
        }
        
        private Dictionary<MoveStatUniqueKey2, MoveStat> GetStatList(ClusterResult clusterResult, int indexOfSecondKeyInLastCentroid)
        {
            var moveList = new Dictionary<MoveStatUniqueKey2, MoveStat>();
            for (var i = 0; i < clusterResult.Data.Count(); ++i)
            {
                var cluster = clusterResult.ClusterIndicies[i];
                moveList.Add(clusterResult.Data[i], new MoveStat()
                {
                    TricksTried = 1,
                    TricksWon = cluster == 0 ? 1 : cluster == 1 ? 0 : i <= indexOfSecondKeyInLastCentroid ? 1 : 0,
                    HandsTried = 1,
                    HandsWon = cluster == 0 ? 0 : cluster == 1 ? 1 : i <= indexOfSecondKeyInLastCentroid ? 1 : 0
                });
            }
            return moveList;
        }

        [TestMethod]
        public void CentroidResultPrediction()
        {
            var roomNo = 32;
            var dict = new Dictionary<MoveStatCentroid, MoveStat>() {
                { centroid1, stat1 },
                { centroid2, stat2 },
                { centroid3, stat3 }
            };
            var movesInRoom = new Dictionary<int, Dictionary<MoveStatCentroid, MoveStat>>() {
                { roomNo, dict }
            };
            var predictor = new CentroidResultPredictor(movesInRoom);

            Assert.AreSame(stat1, predictor.GetPrediction(GetNearbyKey(centroid1, 2.6)));
            Assert.AreSame(stat2, predictor.GetPrediction(GetNearbyKey(centroid2, -0.8)));
            Assert.AreSame(stat3, predictor.GetPrediction(GetNearbyKey(centroid3, 0.92)));
            Assert.AreSame(stat1, predictor.GetPrediction(GetNearbyKey(centroid1, 0.4)));
            Assert.AreSame(stat2, predictor.GetPrediction(GetNearbyKey(centroid2, 1.9)));
            Assert.AreSame(stat3, predictor.GetPrediction(GetNearbyKey(centroid3, -1.5)));
        }

        private MoveStatUniqueKey2 GetNearbyKey(MoveStatCentroid centroid, double offset)
        {
            return new MoveStatUniqueKey2()
            {
                OffenseSide = false,
                PickerDone = false,
                PartnerDone = null,
                PointsInTrick = Round(centroid.PointsInTrick + offset * .95),
                HighestRankInTrick = Round(centroid.HighestRankInTrick + offset * .32),
                WinningSide = false,
                ThisCardMorePowerful = false,
                MorePowerfulUnknownCards = Round(centroid.MorePowerfulUnknownCards + offset * -0.5),
                RemainingUnknownPoints = Round(centroid.RemainingUnknownPoints + offset * -1.5),
                MorePowerfulHeld = Round(centroid.MorePowerfulHeld + offset * 1.2),
                PointsHeld = Round(centroid.PointsHeld + offset * 2.7),
                CardsHeldWithPoints = Round(centroid.CardsHeldWithPoints + offset * -1.1),
                MoveIndex = Round(centroid.MoveIndex + offset * -0.8),
                TrickIndex = Round(centroid.TrickIndex + offset * 0.04)
            };
        }

        private int Round(double d)
        {
            return (int)Math.Round(d, 0);
        }

        private MoveStatCentroid centroid1 = new MoveStatCentroid()
        {
            TrickIndex = 4.3,
            MoveIndex = 0.4,
            PointsInTrick = 1.4,
            HighestRankInTrick = 5.2,
            MorePowerfulUnknownCards = 25.1,
            RemainingUnknownPoints = 181.9,
            MorePowerfulHeld = 2.1,
            PointsHeld = 32.4,
            CardsHeldWithPoints = 1.4
            //PointsInThisCard = 12.3,
            //RankOfThisCard = 4.1,
            
        };
        private MoveStatCentroid centroid2 = new MoveStatCentroid()
        {
            TrickIndex = 2.1,
            MoveIndex = 4.8,
            PointsInTrick = 11.2,
            HighestRankInTrick = 24.1,
            MorePowerfulUnknownCards = 3.1,
            RemainingUnknownPoints = 34.6,
            MorePowerfulHeld = 4.1,
            PointsHeld = 4.3,
            CardsHeldWithPoints = 1.2
            //PointsInThisCard = 4.1,
            //RankOfThisCard = 7.8,
        };
        private MoveStatCentroid centroid3 = new MoveStatCentroid()
        {
            TrickIndex = 0.4,
            MoveIndex = 3.8,
            PointsInTrick = 12.1,
            HighestRankInTrick = 12.3,
            MorePowerfulUnknownCards = 0.2,
            RemainingUnknownPoints = 0.4,
            MorePowerfulHeld = 0.3,
            PointsHeld = 21.7,
            CardsHeldWithPoints = 4.8
            //PointsInThisCard = 4.4,
            //RankOfThisCard = 9.1,
            
        };
        private MoveStat stat1 = new MoveStat()
        {
            TricksWon = 4,
            TricksTried = 12,
            HandsWon = 9,
            HandsTried = 12
        };
        private MoveStat stat2 = new MoveStat()
        {
            TricksWon = 11,
            TricksTried = 12,
            HandsWon = 3,
            HandsTried = 12
        };
        private MoveStat stat3 = new MoveStat()
        {
            TricksWon = 7,
            TricksTried = 18,
            HandsWon = 9,
            HandsTried = 18
        };

        //This is not a unit test, but will generate a CSV file displaying the clustering results.
        //[TestMethod]
        public void DoClustering()
        {
            var rnd = new RandomWrapper();
            var keyList = new List<MoveStatUniqueKey2>();
            for (var i = 0; i < 1000; ++i)
                keyList.Add(GenerateKey(rnd));
            var numClusters = (int)Math.Sqrt(keyList.Count());
            var clusterer = new Clusterer(rnd);
            var results = clusterer.Cluster(keyList);
            using (var sb = new StreamWriter(@"C:\Temp\ClusterTest.csv"))
            {
                sb.WriteLine("Offense,Picker,Partner,Points,Highest Rank,Winning,This Card," +
                    "More Powerful,Remaining,More Powerful,Points,Cards Held," +
                    "Move,Trick,Closer," +
                    "Distance to,Distance to,Distance to,Distance to,Distance to,"+
                    "Distance to,Distance to,Distance to,Distance to,Distance to");
                sb.WriteLine("Side,Done,Done,In Trick,In Trick,Side,More Powerful," +
                    "Unknown Cards,Unknown Points,Held,Held,With Points," +
                    "Index,Index,Centroids," +
                    "Centroid 1,Centroid 2,Centroid 3,Centroid 4,Centroid 5," +
                    "Centroid 6,Centroid 7,Centroid 8,Centroid 9,Centroid 10"); 
                foreach (var roomAndResult in results)
                {
                    var roomNo = roomAndResult.Key;
                    var result = roomAndResult.Value;
                    sb.WriteLine("Room: " + roomNo);
                    foreach (var centroid in result.Centroids)
                    {
                        sb.WriteLine(
                            ",,," +
                            "*" + centroid.PointsInTrick.ToString("F3") + "*," +
                            "*" + centroid.HighestRankInTrick.ToString("F3") + "*," +
                            ",," +
                            "*" + centroid.MorePowerfulUnknownCards.ToString("F3") + "*," +
                            "*" + centroid.RemainingUnknownPoints.ToString("F3") + "*," +
                            "*" + centroid.MorePowerfulHeld.ToString("F3") + "*," +
                            "*" + centroid.PointsHeld.ToString("F3") + "*," +
                            "*" + centroid.CardsHeldWithPoints.ToString("F3") + "*," +
                            "*" + centroid.MoveIndex.ToString("F3") + "*," +
                            "*" + centroid.TrickIndex.ToString("F3") + "*"
                            );
                        foreach (var key in result.GetDataInCluster(centroid))
                        {
                            var comparison = "";
                            var closerCentroids = 0;
                            var mainDistance = ClusterUtils.Distance(key, centroid);
                            foreach (var searchCentroid in result.Centroids)
                            {
                                var distance = ClusterUtils.Distance(key, searchCentroid);
                                comparison += "," + distance;
                                if (distance < mainDistance)
                                    ++closerCentroids;
                            }
                            sb.WriteLine(
                                key.OffenseSide + "," +
                                key.PickerDone + "," +
                                (key.PartnerDone == null ? " " : key.PartnerDone.Value.ToString()) + "," +
                                key.PointsInTrick + "," +
                                key.HighestRankInTrick + "," +
                                key.WinningSide + "," +
                                key.ThisCardMorePowerful + "," +
                                key.MorePowerfulUnknownCards + "," +
                                key.RemainingUnknownPoints + "," +
                                key.MorePowerfulHeld + "," +
                                key.PointsHeld + "," +
                                key.CardsHeldWithPoints + "," +
                                key.MoveIndex + "," +
                                key.TrickIndex + "," +
                                closerCentroids +
                                comparison
                            );
                        }
                        sb.WriteLine();
                    }
                }
            }
        }

        private MoveStatUniqueKey2 GenerateKey(IRandomWrapper rnd)
        {
            var partner = rnd.Next(6);
            var totalpointsinprevioustricks = rnd.Next(120);
            return new MoveStatUniqueKey2()
            {
                OffenseSide = rnd.Next(2) == 1,
                PickerDone = rnd.Next(2) == 1,
                PartnerDone = GetPartnerDone(rnd.Next(2)),
                PointsInTrick = rnd.Next(54),
                HighestRankInTrick = rnd.Next(32),
                WinningSide = rnd.Next(2) == 1,
                ThisCardMorePowerful = rnd.Next(2) == 1,
                MorePowerfulUnknownCards = rnd.Next(31),
                RemainingUnknownPoints = rnd.Next(120),
                MorePowerfulHeld = rnd.Next(5),
                PointsHeld = rnd.Next(54),
                CardsHeldWithPoints = rnd.Next(5),
                MoveIndex = rnd.Next(4),
                TrickIndex = rnd.Next(5),
            };
        }

        private bool? GetPartnerDone(int i)
        {
            switch (i)
            {
                case 1:
                    return true;
                case 0:
                    return false;
                default:
                    return null;
            }
        }
    }
}

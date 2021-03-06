﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sheepshead.Logic;
using Sheepshead.Logic.Players;
using Sheepshead.Logic.Models;

namespace Sheepshead.Tests
{
    [TestClass]
    public class GameStateAnalyzerTests
    {
        #region All Opponents Have Played
        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Defense_PartnerProbablyKnown_Yes()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { pickerMock.Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns(partnerMock.Object);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns(partnerMock.Object);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Defense_PartnerUnknown_Yes()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { pickerMock.Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.KING_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Defense_PartnerUnknown_Null()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { pickerMock.Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Defense_PartnerProbablyKnown_No()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { pickerMock.Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns(partnerMock.Object);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Defense_PartnerUnknown_No()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns(partnerMock.Object);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Offense_PartnerProbablyKnown_Yes()
        {
            var partnerCard = SheepCard.JACK_HEARTS;
            var pickerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() { partnerCard });
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.JACK_DIAMONDS },
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(partnerCard);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Offense_PartnerUnknown_Yes()
        {
            var partnerCard = SheepCard.JACK_HEARTS;
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { partnerMock.Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.JACK_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_HEARTS },
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(playerMock.Object);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(partnerCard);
            trickMock.Setup(m => m.IHand.Partner).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Offense_PartnerUnknown_Null()
        {
            var partnerCard = SheepCard.JACK_HEARTS;
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { partnerMock.Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.JACK_DIAMONDS },
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(playerMock.Object);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.Partner).Returns(partnerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(partnerCard);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Offense_PartnerProbablyKnown_No()
        {
            var partnerCard = SheepCard.JACK_HEARTS;
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { partnerMock.Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.JACK_DIAMONDS },
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(playerMock.Object);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns(partnerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(partnerCard);
            trickMock.Setup(m => m.IHand.Partner).Returns((IPlayer)null);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_AllOpponentsHavePlayed_Offense_PartnerUnknown_No()
        {
            var partnerCard = SheepCard.JACK_HEARTS;
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_HEARTS },
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(playerMock.Object);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(partnerCard);
            trickMock.Setup(m => m.IHand.IGame.PlayerCount).Returns(5);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.AllOpponentsHavePlayed(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }
        #endregion

        #region My Side Winning

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Defense_PartnerProbablyKnown_Yes()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns(partnerMock.Object);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Defense_PartnerProbablyKnown_No()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns(partnerMock.Object);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Defense_PartnerUnknown_No()
        {
            var pickerMock = new Mock<IPlayer>();
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((Player)null);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Offense_PartnerProbablyKnown_Yes()
        {
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(playerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns(partnerMock.Object);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Offense_PartnerProbablyKnown_No()
        {
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(playerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns(partnerMock.Object);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Offense_ThisIsPartner_Yes()
        {
            var partnerCard = SheepCard.JACK_HEARTS;
            var pickerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() { partnerCard });
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS },
                { pickerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(partnerCard);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((Player)null);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Offense_ThisIsPartner_No()
        {
            var partnerCard = SheepCard.JACK_HEARTS;
            var pickerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() { partnerCard });
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { pickerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(pickerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(partnerCard);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((Player)null);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MySideWinning_Offense_PartnerUnknown_No()
        {
            var partnerMock = new Mock<IPlayer>();
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>());
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS },
                { partnerMock.Object, SheepCard.JACK_CLUBS }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock.Setup(m => m.IHand.Picker).Returns(playerMock.Object);
            trickMock.Setup(m => m.IHand.PartnerCardEnum).Returns(SheepCard.JACK_DIAMONDS);
            trickMock.Setup(m => m.IHand.Partner).Returns((Player)null);
            trickMock.Setup(m => m.IHand.PresumedParnter).Returns((Player)null);
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.MySideWinning(playerMock.Object, trickMock.Object);
            Assert.AreEqual(false, actual);
        }

        #endregion

        #region My Cards That Can Win

        [TestMethod]
        public void GameStateAnalyzer_MyCardsThatCanWin_SomeResults()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.N10_HEARTS,
                SheepCard.N8_HEARTS,
                SheepCard.N8_DIAMONDS,
                SheepCard.KING_SPADES,
                SheepCard.ACE_DIAMONDS,
                SheepCard.ACE_CLUBS
            });
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.N10_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_SPADES }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);
            trickMock
                .Setup(m => m.IsLegalAddition(It.IsAny<SheepCard>(), playerMock.Object))
                .Returns((SheepCard c, IPlayer p) => c == SheepCard.N10_HEARTS);

            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.ICanWinTrick(playerMock.Object, trickMock.Object);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_MyCardsThatCanWin_NoResults()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.KING_HEARTS,
                SheepCard.N8_HEARTS,
                SheepCard.N8_DIAMONDS,
                SheepCard.KING_SPADES,
                SheepCard.ACE_DIAMONDS,
                SheepCard.ACE_CLUBS
            });
            var cardsPlayed = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N7_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.N10_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_SPADES }
            };
            var trickMock = new Mock<ITrick>();
            trickMock.Setup(m => m.CardsByPlayer).Returns(cardsPlayed);

            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.ICanWinTrick(playerMock.Object, trickMock.Object);
            Assert.IsFalse(actual);
        }

        #endregion

        #region Unplayed Cards Beat Played Cards

        [TestMethod]
        public void GameStateAnalyzer_UnplayedCardsBeatPlayedCards_TrumpResults_True()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.KING_SPADES,
                SheepCard.ACE_DIAMONDS,
                SheepCard.N7_CLUBS,
                SheepCard.N9_CLUBS
            });
            var cardsPlayed1 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.ACE_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_CLUBS },
            };
            var cardsPlayed2 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N10_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.KING_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N9_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N7_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_HEARTS },
            };
            var cardsPlayed3 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N7_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N8_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS }
            };
            var trickMock1 = new Mock<ITrick>();
            var trickMock2 = new Mock<ITrick>();
            var trickMock3 = new Mock<ITrick>();
            trickMock1.Setup(m => m.CardsByPlayer).Returns(cardsPlayed1);
            trickMock2.Setup(m => m.CardsByPlayer).Returns(cardsPlayed2);
            trickMock3.Setup(m => m.CardsByPlayer).Returns(cardsPlayed3);
            var allTricks = new List<ITrick>()
            {
                trickMock1.Object,
                trickMock2.Object,
                trickMock3.Object
            };
            trickMock3.Setup(m => m.IHand.ITricks).Returns(allTricks);

            //Starting suite in the current trick is Spades.
            //All trump have been played or are in the current player's hand except jacks.
            //All spades have been played or are in the current player's hand except 9 and 10. 
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.UnplayedCardsBeatPlayedCards(playerMock.Object, trickMock3.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_UnplayedCardsBeatPlayedCards_OnlyFailResults_True()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.KING_SPADES,
                SheepCard.ACE_DIAMONDS,
                SheepCard.JACK_CLUBS,
                SheepCard.JACK_SPADES
            });
            var cardsPlayed1 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.ACE_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.JACK_HEARTS },
            };
            var cardsPlayed2 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.JACK_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N10_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.KING_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N9_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N7_DIAMONDS },
            };
            var cardsPlayed3 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N7_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N8_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS }
            };
            var trickMock1 = new Mock<ITrick>();
            var trickMock2 = new Mock<ITrick>();
            var trickMock3 = new Mock<ITrick>();
            trickMock1.Setup(m => m.CardsByPlayer).Returns(cardsPlayed1);
            trickMock2.Setup(m => m.CardsByPlayer).Returns(cardsPlayed2);
            trickMock3.Setup(m => m.CardsByPlayer).Returns(cardsPlayed3);
            var allTricks = new List<ITrick>()
            {
                trickMock1.Object,
                trickMock2.Object,
                trickMock3.Object
            };
            trickMock3.Setup(m => m.IHand.ITricks).Returns(allTricks);

            //Starting suite in the current trick is Spades.
            //All trump have been played or are in the current player's hand.
            //All spades have been played or are in the current player's hand except 9 and 10. 
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.UnplayedCardsBeatPlayedCards(playerMock.Object, trickMock3.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_UnplayedCardsBeatPlayedCards_False()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.KING_SPADES,
                SheepCard.ACE_DIAMONDS,
                SheepCard.JACK_CLUBS,
                SheepCard.JACK_SPADES
            });
            var cardsPlayed1 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.ACE_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.JACK_HEARTS },
            };
            var cardsPlayed2 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.JACK_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N10_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.KING_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N9_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N7_DIAMONDS },
            };
            var cardsPlayed3 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N7_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N10_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N9_SPADES }
            };
            var trickMock1 = new Mock<ITrick>();
            var trickMock2 = new Mock<ITrick>();
            var trickMock3 = new Mock<ITrick>();
            trickMock1.Setup(m => m.CardsByPlayer).Returns(cardsPlayed1);
            trickMock2.Setup(m => m.CardsByPlayer).Returns(cardsPlayed2);
            trickMock3.Setup(m => m.CardsByPlayer).Returns(cardsPlayed3);
            var allTricks = new List<ITrick>()
            {
                trickMock1.Object,
                trickMock2.Object,
                trickMock3.Object
            };
            trickMock3.Setup(m => m.IHand.ITricks).Returns(allTricks);

            //Starting suite in the current trick is Spades.
            //All trump have been played or are in the current player's hand.
            //All spades have been played or are in the current player's hand except 9 and 10. 
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.UnplayedCardsBeatPlayedCards(playerMock.Object, trickMock3.Object);
            Assert.AreEqual(false, actual);
        }

        #endregion

        #region Unplayed Cards Beat My Cards 

        [TestMethod]
        public void GameStateAnalyzer_UnplayedCardsBeatMyCards_PlayerMustPlayFail_True()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.KING_SPADES,
                SheepCard.ACE_DIAMONDS,
                SheepCard.N7_CLUBS,
                SheepCard.N9_CLUBS
            });
            var cardsPlayed1 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.ACE_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_CLUBS },
            };
            var cardsPlayed2 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N10_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.KING_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N9_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N7_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_HEARTS },
            };
            var cardsPlayed3 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N7_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N8_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS }
            };
            var trickMock1 = new Mock<ITrick>();
            var trickMock2 = new Mock<ITrick>();
            var trickMock3 = new Mock<ITrick>();
            trickMock1.Setup(m => m.CardsByPlayer).Returns(cardsPlayed1);
            trickMock2.Setup(m => m.CardsByPlayer).Returns(cardsPlayed2);
            trickMock3.Setup(m => m.CardsByPlayer).Returns(cardsPlayed3);
            var allTricks = new List<ITrick>()
            {
                trickMock1.Object,
                trickMock2.Object,
                trickMock3.Object
            };
            trickMock3.Setup(m => m.IHand.ITricks).Returns(allTricks);
            trickMock3
                .Setup(m => m.IsLegalAddition(It.IsAny<SheepCard>(), It.IsAny<IPlayer>()))
                .Returns((SheepCard c, IPlayer p) => c == SheepCard.KING_SPADES);

            //Starting suite in the current trick is Spades.
            //All the jacks are currently unplayed.
            //Jacks beat king of spades (ace of diamons is not legal).
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.UnplayedCardsBeatMyCards(playerMock.Object, trickMock3.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_UnplayedCardsBeatMyCards_PlayerMayPlayTrump_True()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.KING_HEARTS,
                SheepCard.ACE_DIAMONDS,
                SheepCard.N7_CLUBS,
                SheepCard.N9_CLUBS
            });
            var cardsPlayed1 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.ACE_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.KING_CLUBS },
            };
            var cardsPlayed2 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.ACE_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.N10_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.KING_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N9_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N7_DIAMONDS },
            };
            var cardsPlayed3 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N7_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N8_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.ACE_HEARTS }
            };
            var trickMock1 = new Mock<ITrick>();
            var trickMock2 = new Mock<ITrick>();
            var trickMock3 = new Mock<ITrick>();
            trickMock1.Setup(m => m.CardsByPlayer).Returns(cardsPlayed1);
            trickMock2.Setup(m => m.CardsByPlayer).Returns(cardsPlayed2);
            trickMock3.Setup(m => m.CardsByPlayer).Returns(cardsPlayed3);
            var allTricks = new List<ITrick>()
            {
                trickMock1.Object,
                trickMock2.Object,
                trickMock3.Object
            };
            trickMock3.Setup(m => m.IHand.ITricks).Returns(allTricks);
            trickMock3.Setup(m => m.IsLegalAddition(It.IsAny<SheepCard>(), It.IsAny<IPlayer>())).Returns(true);

            //Starting suite in the current trick is Spades.
            //All the jacks are currently unplayed.
            //Jacks beat ace of diamonds.
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.UnplayedCardsBeatMyCards(playerMock.Object, trickMock3.Object);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GameStateAnalyzer_UnplayedCardsBeatMyCards_False()
        {
            var playerMock = new Mock<IPlayer>();
            playerMock.Setup(m => m.Cards).Returns(new List<SheepCard>() {
                SheepCard.KING_SPADES,
                SheepCard.ACE_DIAMONDS,
                SheepCard.N7_DIAMONDS,
                SheepCard.ACE_SPADES
            });
            var cardsPlayed1 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.QUEEN_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.JACK_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.JACK_SPADES },
            };
            var cardsPlayed2 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.JACK_HEARTS },
                { new Mock<IPlayer>().Object, SheepCard.JACK_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N10_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.KING_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N9_DIAMONDS },
                { new Mock<IPlayer>().Object, SheepCard.N8_DIAMONDS },
            };
            var cardsPlayed3 = new Dictionary<IPlayer, SheepCard>() {
                { new Mock<IPlayer>().Object, SheepCard.N8_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N7_SPADES },
                { new Mock<IPlayer>().Object, SheepCard.N8_CLUBS },
                { new Mock<IPlayer>().Object, SheepCard.KING_HEARTS }
            };
            var trickMock1 = new Mock<ITrick>();
            var trickMock2 = new Mock<ITrick>();
            var trickMock3 = new Mock<ITrick>();
            trickMock1.Setup(m => m.CardsByPlayer).Returns(cardsPlayed1);
            trickMock2.Setup(m => m.CardsByPlayer).Returns(cardsPlayed2);
            trickMock3.Setup(m => m.CardsByPlayer).Returns(cardsPlayed3);
            var allTricks = new List<ITrick>()
            {
                trickMock1.Object,
                trickMock2.Object,
                trickMock3.Object
            };
            trickMock3.Setup(m => m.IHand.ITricks).Returns(allTricks);
            trickMock3
                .Setup(m => m.IsLegalAddition(It.IsAny<SheepCard>(), It.IsAny<IPlayer>()))
                .Returns((SheepCard c, IPlayer p) => c == SheepCard.KING_SPADES || c == SheepCard.ACE_SPADES);

            //Starting suite in the current trick is Spades.
            //All trump have been played or are in the current player's hand.
            //The most powerful unplayed spade is in the players hand. 
            var analyzer = new GameStateAnalyzer();
            var actual = analyzer.UnplayedCardsBeatMyCards(playerMock.Object, trickMock3.Object);
            Assert.AreEqual(false, actual);
        }

        #endregion
    }
}

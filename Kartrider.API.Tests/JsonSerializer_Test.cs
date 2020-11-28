using Kartrider.API.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Kartrider.API.Tests
{
    [TestClass]
    public class JsonSerializer_Test
    {
        [TestMethod("팀전 MatchDetail Deserialize/Serialize Test")]
        [DataRow("02190017e4d090b5")]
        [DataRow("02190017e4ce4aeb")]
        [DataRow("02190017e4cb67c9")]
        [DataRow("02190017e4c8c681")]
        [DataRow("02190017e4c633a1")]
        public void TeamMatchDetailSerializer(string matchId)
        {
            KartApi kartApi = KartApiSingleton.KartApi;
            MatchDetail matchDetail1 = kartApi.GetMatchDetail(matchId);
            string json = JsonSerializer.Serialize(matchDetail1);
            MatchDetail matchDetail2 = JsonSerializer.Deserialize<MatchDetail>(json);
            Assert.AreEqual(matchDetail1.ChannelName,matchDetail2.ChannelName);
            Assert.AreEqual(matchDetail1.EndTime,matchDetail2.EndTime);
            Assert.AreEqual(matchDetail1.GameSpeed,matchDetail2.GameSpeed);
            Assert.AreEqual(matchDetail1.MatchId,matchDetail2.MatchId);
            Assert.AreEqual(matchDetail1.MatchResult,matchDetail2.MatchResult);
            Assert.AreEqual(matchDetail1.MatchType,matchDetail2.MatchType);
            Assert.AreEqual(matchDetail1.PlayTime,matchDetail2.PlayTime);
            Assert.AreEqual(matchDetail1.StartTime,matchDetail2.StartTime);
            Assert.AreEqual(matchDetail1.TrackId,matchDetail2.TrackId);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < matchDetail1.Teams[i].Players.Count; j++)
                {
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].AccountNo, matchDetail2.Teams[i].Players[j].AccountNo);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].Character, matchDetail2.Teams[i].Players[j].Character);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].CharacterName, matchDetail2.Teams[i].Players[j].CharacterName);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].FlyingPet, matchDetail2.Teams[i].Players[j].FlyingPet);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].Kart, matchDetail2.Teams[i].Players[j].Kart);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].License, matchDetail2.Teams[i].Players[j].License);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].MatchRank, matchDetail2.Teams[i].Players[j].MatchRank);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].MatchRetired, matchDetail2.Teams[i].Players[j].MatchRetired);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].MatchTime, matchDetail2.Teams[i].Players[j].MatchTime);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].MatchWin, matchDetail2.Teams[i].Players[j].MatchWin);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].PartsEngine, matchDetail2.Teams[i].Players[j].PartsEngine);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].PartsHandle, matchDetail2.Teams[i].Players[j].PartsHandle);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].PartsKit, matchDetail2.Teams[i].Players[j].PartsKit);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].PartsWheel, matchDetail2.Teams[i].Players[j].PartsWheel);
                    Assert.AreEqual(matchDetail1.Teams[i].Players[j].Pet, matchDetail2.Teams[i].Players[j].Pet);
                }
            }
        }
        [TestMethod("개인전 MatchDetail Deserialize/Serialize Test")]
        [DataRow("00c4000ee4b92161")]
        [DataRow("00c4000ee4b6b478")]
        [DataRow("00c4000ee4b47209")]
        [DataRow("00c4000ee4b26244")]
        [DataRow("01c9000ee4afad84")]
        public void SoloMatchDetailSerializer(string matchId)
        {
            KartApi kartApi = KartApiSingleton.KartApi;
            MatchDetail matchDetail1 = kartApi.GetMatchDetail(matchId);
            string json = JsonSerializer.Serialize(matchDetail1);
            MatchDetail matchDetail2 = JsonSerializer.Deserialize<MatchDetail>(json);
            Assert.AreEqual(matchDetail1.ChannelName, matchDetail2.ChannelName);
            Assert.AreEqual(matchDetail1.EndTime, matchDetail2.EndTime);
            Assert.AreEqual(matchDetail1.GameSpeed, matchDetail2.GameSpeed);
            Assert.AreEqual(matchDetail1.MatchId, matchDetail2.MatchId);
            Assert.AreEqual(matchDetail1.MatchResult, matchDetail2.MatchResult);
            Assert.AreEqual(matchDetail1.MatchType, matchDetail2.MatchType);
            Assert.AreEqual(matchDetail1.PlayTime, matchDetail2.PlayTime);
            Assert.AreEqual(matchDetail1.StartTime, matchDetail2.StartTime);
            Assert.AreEqual(matchDetail1.TrackId, matchDetail2.TrackId);
                for (int i = 0; i < matchDetail1.Players.Count; i++)
                {
                    Assert.AreEqual(matchDetail1.Players[i].AccountNo, matchDetail2.Players[i].AccountNo);
                    Assert.AreEqual(matchDetail1.Players[i].Character, matchDetail2.Players[i].Character);
                    Assert.AreEqual(matchDetail1.Players[i].CharacterName, matchDetail2.Players[i].CharacterName);
                    Assert.AreEqual(matchDetail1.Players[i].FlyingPet, matchDetail2.Players[i].FlyingPet);
                    Assert.AreEqual(matchDetail1.Players[i].Kart, matchDetail2.Players[i].Kart);
                    Assert.AreEqual(matchDetail1.Players[i].License, matchDetail2.Players[i].License);
                    Assert.AreEqual(matchDetail1.Players[i].MatchRank, matchDetail2.Players[i].MatchRank);
                    Assert.AreEqual(matchDetail1.Players[i].MatchRetired, matchDetail2.Players[i].MatchRetired);
                    Assert.AreEqual(matchDetail1.Players[i].MatchTime, matchDetail2.Players[i].MatchTime);
                    Assert.AreEqual(matchDetail1.Players[i].MatchWin, matchDetail2.Players[i].MatchWin);
                    Assert.AreEqual(matchDetail1.Players[i].PartsEngine, matchDetail2.Players[i].PartsEngine);
                    Assert.AreEqual(matchDetail1.Players[i].PartsHandle, matchDetail2.Players[i].PartsHandle);
                    Assert.AreEqual(matchDetail1.Players[i].PartsKit, matchDetail2.Players[i].PartsKit);
                    Assert.AreEqual(matchDetail1.Players[i].PartsWheel, matchDetail2.Players[i].PartsWheel);
                    Assert.AreEqual(matchDetail1.Players[i].Pet, matchDetail2.Players[i].Pet);
                }
        }
    }
}

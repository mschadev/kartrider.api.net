using Kartrider.API.Exceptions;
using Kartrider.API.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;

namespace Kartrider.API.Tests
{
    [TestClass]
    public class KartAPI_Test
    {
        [TestMethod(displayName: "생성자 테스트")]
        public void Constructor()
        {
           new KartAPI(Define.API_KEY);
        }
        [TestMethod(displayName: "메타데이터 다운로드")]
        public void DownloadMetadata()
        {
            KartAPI.DownloadMetadata("metadata.zip");
            Assert.IsTrue(File.Exists("metadata.zip"));
            File.Delete("metadata.zip");
        }
        [TestMethod(displayName: "유저 고유 식별자로 라이더명 조회")]
        public void GetUserInfoByAccessId()
        {
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            UserInfo userInfo = kartAPI.GetUserInfoByAccessId("302575272");
            Assert.AreEqual(userInfo.AccessId, "302575272");
            Assert.AreEqual(userInfo.Nickname, "TTEESSTT");
        }
        [TestMethod(displayName: "라이더명으로 유저 정보 조회")]
        public void GetUserInfoByNickname()
        {
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            UserInfo userInfo = kartAPI.GetUserInfoByNickname("TTEESSTT");
            Assert.AreEqual(userInfo.AccessId, "302575272");
            Assert.AreEqual(userInfo.Nickname, "TTEESSTT");
        }
        [TestMethod(displayName: "유저 고유 식별자로 매치 리스트 조회")]
        public void GetMatchesAsAccessId()
        {
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.GetMatchesByAccessId("302575272", null, null, 0, 2, new string[]{
                "7b9f0fd5377c38514dbb78ebe63ac6c3b81009d5a31dd569d1cff8f005aa881a",
"effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e"
                   });
            kartAPI.GetMatchesByAccessId("302575272");
        }
        [TestMethod(displayName: "모든 매치 리스트 조회")]
        public void GetAllMatches()
        {
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.GetAllMatches(null, null, 0, 100, new string[]{
                "7b9f0fd5377c38514dbb78ebe63ac6c3b81009d5a31dd569d1cff8f005aa881a",
"effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e"
                  });
            DateTime startDate = DateTime.Now.AddMinutes(-1).AddSeconds(-10);
            DateTime endDate = DateTime.Now;
            kartAPI.GetAllMatches(startDate, endDate, 0, 200);
        }
        [TestMethod(displayName: "특정 매치의 상세 정보 조회")]
        public void GetMatchDetail()
        {
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            AllMatches allMatches = kartAPI.GetAllMatches(null, null, 0, 20);
            foreach (var matches in allMatches.Matches)
            {
                foreach (var matchId in matches.Matches)
                {
                    kartAPI.GetMatchDetail(matchId);
                }
                
            }
        }
        [TestMethod(displayName: "예외(Not Found)")]
        public void Exception1()
        {
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            bool exceptionCatch = false;
            try
            {
                kartAPI.GetUserInfoByNickname("qwdascqwe");
            }
            catch (KartAPIException e) when(e.StatusCode == KartAPIStatusCode.NotFound) 
            {
                exceptionCatch = true;
            }
            Assert.IsTrue(exceptionCatch);
        }
    }
}

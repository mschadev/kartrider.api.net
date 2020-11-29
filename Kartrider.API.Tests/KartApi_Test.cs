using Kartrider.API.Exceptions;
using Kartrider.API.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Linq;

namespace Kartrider.API.Tests
{
    [TestClass]
    public class KartApi_Test
    {
        [TestMethod(displayName: "생성자 테스트")]
        public void Constructor()
        {
           new KartApi(Define.API_KEY);
        }
        [TestMethod("GetMatchesByAccessId 인수 예외")]
        public void GetMatchesByAccessIdArgumentExceptionTest()
        {
            KartApi kartApi = KartApiSingleton.KartApi;
            bool exception = false;
            try
            {
                kartApi.GetMatchesByAccessId("302575272", null, null, -1, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }
            Assert.IsTrue(exception);

            exception = false;
            try
            {
                kartApi.GetMatchesByAccessId("302575272", null, null, 1,0);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }
            Assert.IsTrue(exception);
            exception = false;
            try
            {
                kartApi.GetMatchesByAccessId("302575272", null, null, 1, 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }
            Assert.IsFalse(exception);
        }
        [TestMethod("GetAllMatches 인수 테스트")]
        public void GetAllMatchesArgumentExceptionTest()
        {
            KartApi kartApi = KartApiSingleton.KartApi;
            bool exception = false;
            try
            {
                kartApi.GetAllMatches(null,null,-1,0);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }
            Assert.IsTrue(exception);

            exception = false;
            try
            {
                kartApi.GetAllMatches(null, null, 1, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }
            Assert.IsTrue(exception);
            exception = false;
            try
            {
                kartApi.GetAllMatches(null, null, 1, 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }
            Assert.IsFalse(exception);
        }
        [TestMethod(displayName: "메타데이터 다운로드")]
        public void DownloadMetadata()
        {
            KartApi.DownloadMetadata("metadata.zip");
            bool check = File.Exists("metadata.zip");
            File.Delete("metadata.zip");
            Assert.IsTrue(check);
        }
        [TestMethod(displayName: "유저 고유 식별자로 유저 정보 조회")]
        public void GetUserInfoByAccessId()
        {
            KartApi KartApi = KartApiSingleton.KartApi;
            UserInfo userInfo = KartApi.GetUserInfoByAccessId("302575272");
            Assert.AreEqual(userInfo.AccessId, "302575272");
            Assert.AreEqual(userInfo.Nickname, "TTEESSTT");
            Assert.AreEqual(userInfo.Level,40);
        }
        [TestMethod(displayName: "라이더명으로 유저 정보 조회")]
        public void GetUserInfoByNickname()
        {
            KartApi KartApi = KartApiSingleton.KartApi;
            UserInfo userInfo = KartApi.GetUserInfoByNickname("TTEESSTT");
            Assert.AreEqual(userInfo.AccessId, "302575272");
            Assert.AreEqual(userInfo.Nickname, "TTEESSTT");
            Assert.AreEqual(userInfo.Level, 40);
        }
        [TestMethod(displayName: "유저 고유 식별자로 매치 리스트 조회")]
        public void GetMatchesAsAccessId()
        {
            KartApi KartApi = KartApiSingleton.KartApi;
            KartApi.GetMatchesByAccessId("302575272", null, null, 0, 2, new string[]{
                "7b9f0fd5377c38514dbb78ebe63ac6c3b81009d5a31dd569d1cff8f005aa881a",
"effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e"
                   });
            KartApi.GetMatchesByAccessId("302575272");
        }
        [TestMethod(displayName: "모든 매치 리스트 조회")]
        public void GetAllMatches()
        {
            KartApi KartApi = KartApiSingleton.KartApi;
            AllMatches allMatches = KartApi.GetAllMatches(null, null, 0, 100, new string[]{
                "7b9f0fd5377c38514dbb78ebe63ac6c3b81009d5a31dd569d1cff8f005aa881a",
"effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e"
                  });
            int matchCount = allMatches.Matches.Select(p => p.Matches.Count()).Sum();
            Assert.AreEqual(matchCount, 100);
        }
        [TestMethod(displayName: "특정 매치의 상세 정보 조회")]
        public void GetMatchDetail()
        {
            KartApi KartApi = KartApiSingleton.KartApi;
            AllMatches allMatches = KartApi.GetAllMatches(null, null, 0, 20);
            foreach (var matches in allMatches.Matches)
            {
                foreach (var matchId in matches.Matches)
                {
                    KartApi.GetMatchDetail(matchId);
                }
                
            }
        }
        [TestMethod(displayName: "예외(Not Found)")]
        public void Exception1()
        {
            KartApi KartApi = KartApiSingleton.KartApi;
            bool exceptionCatch = false;
            try
            {
                KartApi.GetUserInfoByNickname("qwdascqwe");
            }
            catch (KartApiRequestException e) when(e.StatusCode == KartApiStatusCode.NotFound) 
            {
                exceptionCatch = true;
            }
            Assert.IsTrue(exceptionCatch);
        }
    }
}

using Kartrider.API.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;

namespace Kartrider.API.Tests
{
    [TestClass]
    public class Metadata_Test
    {
        private Metadata _metadata = MetadataSingleton.Metadata;
        [TestMethod(displayName: "생성자")]
        public void Constructor()
        {
            new Metadata(true);
        }
        [TestMethod(displayName: "업데이트")]
        public void MetadataUpdate()
        {
            API.Metadata metadata = new API.Metadata();
            try
            {
                metadata.Update("123.zip");
            }
            catch (FileNotFoundException)
            {
            }
            //Kartrider.API\Kartrider.API.Tests\file
            string path = Path.Combine("..", "..", "..", "file");
            //올바른 metadata zip파일 경로
            metadata.Update(Path.Combine(path, "metadata1.zip"));

            byte[] bytes = File.ReadAllBytes(Path.Combine(path, "metadata1.zip"));
            //올바른 metadata zip 파일 byte array
            metadata.Update(bytes);

            try
            {
                //올바르지 않은 metadata zip파일(일부 .json 파일 누락)
                metadata.Update(Path.Combine(path, "metadata2.zip"));
            }
            catch (FileNotFoundException)
            {
            }

            try
            {
                metadata.Update(null);
            }
            catch (ArgumentNullException)
            {
            }
        }
        [TestMethod(displayName: "엑세스")]
        public void MetadataAccess()
        {
            Metadata metadata = new Metadata();
            string path = Path.Combine("..", "..", "..", "file", "metadata1.zip");
            metadata.Update(path);
            _ = metadata[MetadataType.Character, "4c139477f1eef41ec9a1c7c50319c6f391abb074fa44242eb7a143007e7f7720"];
            _ = metadata[MetadataType.FlyingPet, "52960349bbbaed8cfe9999fc285824180ef1a423ed0c6b481cd1367c913e1ba9"];
            _ = metadata[MetadataType.GameType, "7ca6fd44026a2c8f5d939b60aa56b4b1714b9cc2355ec5e317154d4cf0675da0"];
            _ = metadata[MetadataType.Kart, "4a3d34d9958d54ab218513e2dc406a6a7bc30e529292895475a11a986550b437"];
            _ = metadata[MetadataType.Pet, "9ecb721ca7670d9c196341ca19ca19fcf7a60b9e8ffe010cc53a7105afaefc96"];
            _ = metadata[MetadataType.Track, "ace5823799b7627a033be069afabecf1d3ee9a9a90aae313b66fb405da724a93"];

            try
            {
                _ = metadata[MetadataType.Character, "123"];
            }
            catch (KeyNotFoundException)
            {
            }
        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(AllMatches)")]
        public void HashToString1()
        {
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            AllMatches allMatches = new AllMatches()
            {
                Matches = new List<MatchesByMatchType>() {
                      new MatchesByMatchType()
                      {
                          MatchType = "7ca6fd44026a2c8f5d939b60aa56b4b1714b9cc2355ec5e317154d4cf0675da0",
                      },
                      new MatchesByMatchType()
                      {
                          MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e"
                      }
                }
            };
            _metadata.HashToString(ref allMatches);
            Assert.AreEqual(allMatches.Matches[0].MatchType, "아이템 개인전");
            Assert.AreEqual(allMatches.Matches[1].MatchType, "플래그 개인전");
        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(Match)")]
        public void HashToString2()
        {
            Match match = new Match()
            {
                //플래그 개인전
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                Matches = new List<MatchInfo>()
                {
                    new MatchInfo()
                    {
                        //샤인
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //플래그 개인전
                        MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                        //차이나 서안 병마용 2
                        TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                        Player = new Player()
                        {
                            //샤인
                             Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                             //플라잉 미사일 블루
                              FlyingPet ="c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                              //골든 파라곤 9
                               Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                               //북극 곰탱이
                                Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                        }
                    }
                }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            _metadata.HashToString(ref match);
            Assert.AreEqual(match.MatchType, "플래그 개인전");
            Assert.AreEqual(match.Matches[0].Character, "샤인");
            Assert.AreEqual(match.Matches[0].MatchType, "플래그 개인전");
            Assert.AreEqual(match.Matches[0].Player.Character, "샤인");
            Assert.AreEqual(match.Matches[0].Player.FlyingPet, "플라잉 미사일 블루");
            Assert.AreEqual(match.Matches[0].Player.Kart, "골든 파라곤 9");
            Assert.AreEqual(match.Matches[0].Player.Pet, "북극 곰탱이");

        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(MatchDetail)")]
        public void HashToString3()
        {
            MatchDetail matchDetail = new MatchDetail()
            {
                // 플래그 개인전
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                // 차이나 서안 병마용 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Players = new List<Player>(),
                MatchResult = TeamId.Red,
                Teams = new List<Team>()
                 {
                     new Team()
                     {
                          Players = new List<Player>()
                          {
                               new Player()
                               {
                                   //샤인
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //플라잉 미사일 블루
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //골든 파라곤 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //북극 곰탱이
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               }
                          }
                     },
                     new Team()
                     {
                          Players = new List<Player>()
                          {
                              new Player(){
                                  //샤인
                               Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                               //플라잉 미사일 블루
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                     //골든 파라곤 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //북극 곰탱이
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                              }
                          }
                     }
                 }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            _metadata.HashToString(ref matchDetail);
            Assert.AreEqual(matchDetail.MatchType, "플래그 개인전");
            Assert.AreEqual(matchDetail.TrackId, "차이나 서안 병마용 2");
            foreach (var team in matchDetail.Teams)
            {
                foreach (var player in team.Players)
                {
                    Assert.AreEqual(player.Character, "샤인");
                    Assert.AreEqual(player.FlyingPet, "플라잉 미사일 블루");
                    Assert.AreEqual(player.Kart, "골든 파라곤 9");
                    Assert.AreEqual(player.Pet, "북극 곰탱이");
                }
            }
            matchDetail = new MatchDetail()
            {
                // 플래그 개인전
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                // 차이나 서안 병마용 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Teams = new List<Team>(),
                MatchResult = TeamId.Solo,
                Players = new List<Player>()
                {
                    new Player()
                    {
                        //샤인
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //플라잉 미사일 블루
                        FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                        //골든 파라곤 9
                        Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                        //북극 곰탱이
                        Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                    },
                    new Player()
                    {
                        //샤인
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //플라잉 미사일 블루
                        FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                        //골든 파라곤 9
                        Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                        //북극 곰탱이
                        Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                    }
                }
            };
            _metadata.HashToString(ref matchDetail);
            foreach (var player in matchDetail.Players)
            {
                Assert.AreEqual(player.Character, "샤인");
                Assert.AreEqual(player.FlyingPet, "플라잉 미사일 블루");
                Assert.AreEqual(player.Kart, "골든 파라곤 9");
                Assert.AreEqual(player.Pet, "북극 곰탱이");
            }

        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(MatchesByMatchType)")]
        public void HashToString4()
        {
            MatchesByMatchType matchesByMatchType = new MatchesByMatchType()
            {
                //스피드 팀전
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e"
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            _metadata.HashToString(ref matchesByMatchType);
            Assert.AreEqual(matchesByMatchType.MatchType, "스피드 팀전");
        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(MatchInfo)")]
        public void HashToString5()
        {
            MatchInfo matchInfo = new MatchInfo()
            {
                //스피드 팀전
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //샤인
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //차이나 서안 병마용 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //샤인
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //플라잉 미사일 블루
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //골든 파라곤 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //북극 곰탱이
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            _metadata.HashToString(ref matchInfo);
            Assert.AreEqual(matchInfo.MatchType, "스피드 팀전");
            Assert.AreEqual(matchInfo.Character, "샤인");
            Assert.AreEqual(matchInfo.TrackId, "차이나 서안 병마용 2");
            Assert.AreEqual(matchInfo.Player.Character, "샤인");
            Assert.AreEqual(matchInfo.Player.FlyingPet, "플라잉 미사일 블루");
            Assert.AreEqual(matchInfo.Player.Kart, "골든 파라곤 9");
            Assert.AreEqual(matchInfo.Player.Pet, "북극 곰탱이");
        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(MatchResponse)")]
        public void HashToString6()
        {
            MatchResponse matchResponse = new MatchResponse()
            {
                Matches = new List<Match>()
                 {
                     new Match()
                     {
                          MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                          Matches = new List<MatchInfo>()
                          {
                              new MatchInfo()
            {
                //스피드 팀전
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //샤인
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //차이나 서안 병마용 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //샤인
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //플라잉 미사일 블루
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //골든 파라곤 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //북극 곰탱이
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            },
                              new MatchInfo()
            {
                //스피드 팀전
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //샤인
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //차이나 서안 병마용 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //샤인
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //플라잉 미사일 블루
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //골든 파라곤 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //북극 곰탱이
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            }
        }
                     },
                     new Match()
                     {
                          MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                          Matches = new List<MatchInfo>()
                          {
                              new MatchInfo()
            {
                //스피드 팀전
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //샤인
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //차이나 서안 병마용 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //샤인
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //플라잉 미사일 블루
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //골든 파라곤 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //북극 곰탱이
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            },
                              new MatchInfo()
            {
                //스피드 팀전
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //샤인
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //차이나 서안 병마용 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //샤인
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //플라잉 미사일 블루
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //골든 파라곤 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //북극 곰탱이
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            }
        }
                     }
                 }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            _metadata.HashToString(ref matchResponse);
            foreach (var match in matchResponse.Matches)
            {
                Assert.AreEqual(match.MatchType, "스피드 팀전");
                foreach (var matchInfo in match.Matches)
                {
                    Assert.AreEqual(matchInfo.MatchType, "스피드 팀전");
                    Assert.AreEqual(matchInfo.Character, "샤인");
                    Assert.AreEqual(matchInfo.TrackId, "차이나 서안 병마용 2");
                    Assert.AreEqual(matchInfo.Player.Character, "샤인");
                    Assert.AreEqual(matchInfo.Player.FlyingPet, "플라잉 미사일 블루");
                    Assert.AreEqual(matchInfo.Player.Kart, "골든 파라곤 9");
                    Assert.AreEqual(matchInfo.Player.Pet, "북극 곰탱이");
                }
            }
        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(Player)")]
        public void HashToString7()
        {
            Player player = new Player()
            {
                //샤인
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //플라잉 미사일 블루
                FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                //골든 파라곤 9
                Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                //북극 곰탱이
                Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            _metadata.HashToString(ref player);
            Assert.AreEqual(player.Character, "샤인");
            Assert.AreEqual(player.FlyingPet, "플라잉 미사일 블루");
            Assert.AreEqual(player.Kart, "골든 파라곤 9");
            Assert.AreEqual(player.Pet, "북극 곰탱이");
        }
        [TestMethod(displayName: "해쉬에서 문자열로 변경(Team)")]
        public void HashToString8()
        {

            Team team = new Team()
            {
                Players = new List<Player>()
                          {
                               new Player()
                               {
                                   //샤인
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //플라잉 미사일 블루
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //골든 파라곤 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //북극 곰탱이
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               },
                                new Player()
                               {
                                   //샤인
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //플라잉 미사일 블루
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //골든 파라곤 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //북극 곰탱이
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               }
                          }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            _metadata.HashToString(ref team);
            foreach (var player in team.Players)
            {
                Assert.AreEqual(player.Character, "샤인");
                Assert.AreEqual(player.FlyingPet, "플라잉 미사일 블루");
                Assert.AreEqual(player.Kart, "골든 파라곤 9");
                Assert.AreEqual(player.Pet, "북극 곰탱이");
            }
        }
    }
}

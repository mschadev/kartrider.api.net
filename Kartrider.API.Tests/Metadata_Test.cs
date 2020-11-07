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

        [TestMethod(displayName: "¾÷µ¥ÀÌÆ®")]
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
            //¿Ã¹Ù¸¥ metadata zipÆÄÀÏ °æ·Î
            metadata.Update(Path.Combine(path, "metadata1.zip"));

            byte[] bytes = File.ReadAllBytes(Path.Combine(path, "metadata1.zip"));
            //¿Ã¹Ù¸¥ metadata zip ÆÄÀÏ byte array
            metadata.Update(bytes);

            try
            {
                //¿Ã¹Ù¸£Áö ¾ÊÀº metadata zipÆÄÀÏ(ÀÏºÎ .json ÆÄÀÏ ´©¶ô)
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
        [TestMethod(displayName: "¿¢¼¼½º")]
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
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(AllMatches)")]
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
            kartAPI.Metadata.HashToString(ref allMatches);
            Assert.AreEqual(allMatches.Matches[0].MatchType, "¾ÆÀÌÅÛ °³ÀÎÀü");
            Assert.AreEqual(allMatches.Matches[1].MatchType, "ÇÃ·¡±× °³ÀÎÀü");
        }
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(Match)")]
        public void HashToString2()
        {
            Match match = new Match()
            {
                //ÇÃ·¡±× °³ÀÎÀü
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                Matches = new List<MatchInfo>()
                {
                    new MatchInfo()
                    {
                        //»þÀÎ
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //ÇÃ·¡±× °³ÀÎÀü
                        MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                        //Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
                        TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                        Player = new Player()
                        {
                            //»þÀÎ
                             Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                             //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                              FlyingPet ="c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                              //°ñµç ÆÄ¶ó°ï 9
                               Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                               //ºÏ±Ø °õÅÊÀÌ
                                Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                        }
                    }
                }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref match);
            Assert.AreEqual(match.MatchType, "ÇÃ·¡±× °³ÀÎÀü");
            Assert.AreEqual(match.Matches[0].Character, "»þÀÎ");
            Assert.AreEqual(match.Matches[0].MatchType, "ÇÃ·¡±× °³ÀÎÀü");
            Assert.AreEqual(match.Matches[0].Player.Character, "»þÀÎ");
            Assert.AreEqual(match.Matches[0].Player.FlyingPet, "ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç");
            Assert.AreEqual(match.Matches[0].Player.Kart, "°ñµç ÆÄ¶ó°ï 9");
            Assert.AreEqual(match.Matches[0].Player.Pet, "ºÏ±Ø °õÅÊÀÌ");

        }
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(MatchDetail)")]
        public void HashToString3()
        {
            MatchDetail matchDetail = new MatchDetail()
            {
                // ÇÃ·¡±× °³ÀÎÀü
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                // Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
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
                                   //»þÀÎ
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //°ñµç ÆÄ¶ó°ï 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //ºÏ±Ø °õÅÊÀÌ
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               }
                          }
                     },
                     new Team()
                     {
                          Players = new List<Player>()
                          {
                              new Player(){
                                  //»þÀÎ
                               Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                               //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                     //°ñµç ÆÄ¶ó°ï 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //ºÏ±Ø °õÅÊÀÌ
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                              }
                          }
                     }
                 }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref matchDetail);
            Assert.AreEqual(matchDetail.MatchType, "ÇÃ·¡±× °³ÀÎÀü");
            Assert.AreEqual(matchDetail.TrackId, "Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2");
            foreach (var team in matchDetail.Teams)
            {
                foreach (var player in team.Players)
                {
                    Assert.AreEqual(player.Character, "»þÀÎ");
                    Assert.AreEqual(player.FlyingPet, "ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç");
                    Assert.AreEqual(player.Kart, "°ñµç ÆÄ¶ó°ï 9");
                    Assert.AreEqual(player.Pet, "ºÏ±Ø °õÅÊÀÌ");
                }
            }
            matchDetail = new MatchDetail()
            {
                // ÇÃ·¡±× °³ÀÎÀü
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                // Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Teams = new List<Team>(),
                MatchResult = TeamId.Solo,
                Players = new List<Player>()
                {
                    new Player()
                    {
                        //»þÀÎ
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                        FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                        //°ñµç ÆÄ¶ó°ï 9
                        Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                        //ºÏ±Ø °õÅÊÀÌ
                        Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                    },
                    new Player()
                    {
                        //»þÀÎ
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                        FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                        //°ñµç ÆÄ¶ó°ï 9
                        Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                        //ºÏ±Ø °õÅÊÀÌ
                        Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                    }
                }
            };
            kartAPI.Metadata.HashToString(ref matchDetail);
            foreach (var player in matchDetail.Players)
            {
                Assert.AreEqual(player.Character, "»þÀÎ");
                Assert.AreEqual(player.FlyingPet, "ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç");
                Assert.AreEqual(player.Kart, "°ñµç ÆÄ¶ó°ï 9");
                Assert.AreEqual(player.Pet, "ºÏ±Ø °õÅÊÀÌ");
            }

        }
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(MatchesByMatchType)")]
        public void HashToString4()
        {
            MatchesByMatchType matchesByMatchType = new MatchesByMatchType()
            {
                //½ºÇÇµå ÆÀÀü
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e"
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref matchesByMatchType);
            Assert.AreEqual(matchesByMatchType.MatchType, "½ºÇÇµå ÆÀÀü");
        }
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(MatchInfo)")]
        public void HashToString5()
        {
            MatchInfo matchInfo = new MatchInfo()
            {
                //½ºÇÇµå ÆÀÀü
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //»þÀÎ
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //»þÀÎ
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //°ñµç ÆÄ¶ó°ï 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //ºÏ±Ø °õÅÊÀÌ
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref matchInfo);
            Assert.AreEqual(matchInfo.MatchType, "½ºÇÇµå ÆÀÀü");
            Assert.AreEqual(matchInfo.Character, "»þÀÎ");
            Assert.AreEqual(matchInfo.TrackId, "Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2");
            Assert.AreEqual(matchInfo.Player.Character, "»þÀÎ");
            Assert.AreEqual(matchInfo.Player.FlyingPet, "ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç");
            Assert.AreEqual(matchInfo.Player.Kart, "°ñµç ÆÄ¶ó°ï 9");
            Assert.AreEqual(matchInfo.Player.Pet, "ºÏ±Ø °õÅÊÀÌ");
        }
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(MatchResponse)")]
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
                //½ºÇÇµå ÆÀÀü
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //»þÀÎ
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //»þÀÎ
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //°ñµç ÆÄ¶ó°ï 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //ºÏ±Ø °õÅÊÀÌ
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            },
                              new MatchInfo()
            {
                //½ºÇÇµå ÆÀÀü
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //»þÀÎ
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //»þÀÎ
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //°ñµç ÆÄ¶ó°ï 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //ºÏ±Ø °õÅÊÀÌ
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
                //½ºÇÇµå ÆÀÀü
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //»þÀÎ
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //»þÀÎ
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //°ñµç ÆÄ¶ó°ï 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //ºÏ±Ø °õÅÊÀÌ
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            },
                              new MatchInfo()
            {
                //½ºÇÇµå ÆÀÀü
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //»þÀÎ
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //»þÀÎ
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //°ñµç ÆÄ¶ó°ï 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //ºÏ±Ø °õÅÊÀÌ
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            }
        }
                     }
                 }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref matchResponse);
            foreach(var match in matchResponse.Matches)
            {
                Assert.AreEqual(match.MatchType, "½ºÇÇµå ÆÀÀü");
                foreach(var matchInfo in match.Matches)
                {
                    Assert.AreEqual(matchInfo.MatchType, "½ºÇÇµå ÆÀÀü");
                    Assert.AreEqual(matchInfo.Character, "»þÀÎ");
                    Assert.AreEqual(matchInfo.TrackId, "Â÷ÀÌ³ª ¼­¾È º´¸¶¿ë 2");
                    Assert.AreEqual(matchInfo.Player.Character, "»þÀÎ");
                    Assert.AreEqual(matchInfo.Player.FlyingPet, "ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç");
                    Assert.AreEqual(matchInfo.Player.Kart, "°ñµç ÆÄ¶ó°ï 9");
                    Assert.AreEqual(matchInfo.Player.Pet, "ºÏ±Ø °õÅÊÀÌ");
                }
            }
        }
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(Player)")]
        public void HashToString7()
        {
            Player player = new Player()
            {
                //»þÀÎ
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                //°ñµç ÆÄ¶ó°ï 9
                Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                //ºÏ±Ø °õÅÊÀÌ
                Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref player);
            Assert.AreEqual(player.Character, "»þÀÎ");
            Assert.AreEqual(player.FlyingPet, "ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç");
            Assert.AreEqual(player.Kart, "°ñµç ÆÄ¶ó°ï 9");
            Assert.AreEqual(player.Pet, "ºÏ±Ø °õÅÊÀÌ");
        }
        [TestMethod(displayName: "ÇØ½¬¿¡¼­ ¹®ÀÚ¿­·Î º¯°æ(Team)")]
        public void HashToString8()
        {

            Team team = new Team()
            {
                Players = new List<Player>()
                          {
                               new Player()
                               {
                                   //»þÀÎ
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //°ñµç ÆÄ¶ó°ï 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //ºÏ±Ø °õÅÊÀÌ
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               },
                                new Player()
                               {
                                   //»þÀÎ
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //°ñµç ÆÄ¶ó°ï 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //ºÏ±Ø °õÅÊÀÌ
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               }
                          }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref team);
            foreach(var player in team.Players)
            {
                Assert.AreEqual(player.Character, "»þÀÎ");
                Assert.AreEqual(player.FlyingPet, "ÇÃ¶óÀ× ¹Ì»çÀÏ ºí·ç");
                Assert.AreEqual(player.Kart, "°ñµç ÆÄ¶ó°ï 9");
                Assert.AreEqual(player.Pet, "ºÏ±Ø °õÅÊÀÌ");
            }
        }
    }
}

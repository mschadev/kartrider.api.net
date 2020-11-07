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

        [TestMethod(displayName: "������Ʈ")]
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
            //�ùٸ� metadata zip���� ���
            metadata.Update(Path.Combine(path, "metadata1.zip"));

            byte[] bytes = File.ReadAllBytes(Path.Combine(path, "metadata1.zip"));
            //�ùٸ� metadata zip ���� byte array
            metadata.Update(bytes);

            try
            {
                //�ùٸ��� ���� metadata zip����(�Ϻ� .json ���� ����)
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
        [TestMethod(displayName: "������")]
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
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(AllMatches)")]
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
            Assert.AreEqual(allMatches.Matches[0].MatchType, "������ ������");
            Assert.AreEqual(allMatches.Matches[1].MatchType, "�÷��� ������");
        }
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(Match)")]
        public void HashToString2()
        {
            Match match = new Match()
            {
                //�÷��� ������
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                Matches = new List<MatchInfo>()
                {
                    new MatchInfo()
                    {
                        //����
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //�÷��� ������
                        MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                        //���̳� ���� ������ 2
                        TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                        Player = new Player()
                        {
                            //����
                             Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                             //�ö��� �̻��� ���
                              FlyingPet ="c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                              //��� �Ķ�� 9
                               Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                               //�ϱ� ������
                                Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                        }
                    }
                }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref match);
            Assert.AreEqual(match.MatchType, "�÷��� ������");
            Assert.AreEqual(match.Matches[0].Character, "����");
            Assert.AreEqual(match.Matches[0].MatchType, "�÷��� ������");
            Assert.AreEqual(match.Matches[0].Player.Character, "����");
            Assert.AreEqual(match.Matches[0].Player.FlyingPet, "�ö��� �̻��� ���");
            Assert.AreEqual(match.Matches[0].Player.Kart, "��� �Ķ�� 9");
            Assert.AreEqual(match.Matches[0].Player.Pet, "�ϱ� ������");

        }
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(MatchDetail)")]
        public void HashToString3()
        {
            MatchDetail matchDetail = new MatchDetail()
            {
                // �÷��� ������
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                // ���̳� ���� ������ 2
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
                                   //����
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //�ö��� �̻��� ���
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //��� �Ķ�� 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //�ϱ� ������
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               }
                          }
                     },
                     new Team()
                     {
                          Players = new List<Player>()
                          {
                              new Player(){
                                  //����
                               Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                               //�ö��� �̻��� ���
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                     //��� �Ķ�� 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //�ϱ� ������
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                              }
                          }
                     }
                 }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref matchDetail);
            Assert.AreEqual(matchDetail.MatchType, "�÷��� ������");
            Assert.AreEqual(matchDetail.TrackId, "���̳� ���� ������ 2");
            foreach (var team in matchDetail.Teams)
            {
                foreach (var player in team.Players)
                {
                    Assert.AreEqual(player.Character, "����");
                    Assert.AreEqual(player.FlyingPet, "�ö��� �̻��� ���");
                    Assert.AreEqual(player.Kart, "��� �Ķ�� 9");
                    Assert.AreEqual(player.Pet, "�ϱ� ������");
                }
            }
            matchDetail = new MatchDetail()
            {
                // �÷��� ������
                MatchType = "224ab54ee8a63940f4df542524ee4059b94efbd3e8ce94f03707ed39294a0e2e",
                // ���̳� ���� ������ 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Teams = new List<Team>(),
                MatchResult = TeamId.Solo,
                Players = new List<Player>()
                {
                    new Player()
                    {
                        //����
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //�ö��� �̻��� ���
                        FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                        //��� �Ķ�� 9
                        Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                        //�ϱ� ������
                        Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                    },
                    new Player()
                    {
                        //����
                        Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                        //�ö��� �̻��� ���
                        FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                        //��� �Ķ�� 9
                        Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                        //�ϱ� ������
                        Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                    }
                }
            };
            kartAPI.Metadata.HashToString(ref matchDetail);
            foreach (var player in matchDetail.Players)
            {
                Assert.AreEqual(player.Character, "����");
                Assert.AreEqual(player.FlyingPet, "�ö��� �̻��� ���");
                Assert.AreEqual(player.Kart, "��� �Ķ�� 9");
                Assert.AreEqual(player.Pet, "�ϱ� ������");
            }

        }
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(MatchesByMatchType)")]
        public void HashToString4()
        {
            MatchesByMatchType matchesByMatchType = new MatchesByMatchType()
            {
                //���ǵ� ����
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e"
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref matchesByMatchType);
            Assert.AreEqual(matchesByMatchType.MatchType, "���ǵ� ����");
        }
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(MatchInfo)")]
        public void HashToString5()
        {
            MatchInfo matchInfo = new MatchInfo()
            {
                //���ǵ� ����
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //����
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //���̳� ���� ������ 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //����
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //�ö��� �̻��� ���
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //��� �Ķ�� 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //�ϱ� ������
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref matchInfo);
            Assert.AreEqual(matchInfo.MatchType, "���ǵ� ����");
            Assert.AreEqual(matchInfo.Character, "����");
            Assert.AreEqual(matchInfo.TrackId, "���̳� ���� ������ 2");
            Assert.AreEqual(matchInfo.Player.Character, "����");
            Assert.AreEqual(matchInfo.Player.FlyingPet, "�ö��� �̻��� ���");
            Assert.AreEqual(matchInfo.Player.Kart, "��� �Ķ�� 9");
            Assert.AreEqual(matchInfo.Player.Pet, "�ϱ� ������");
        }
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(MatchResponse)")]
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
                //���ǵ� ����
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //����
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //���̳� ���� ������ 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //����
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //�ö��� �̻��� ���
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //��� �Ķ�� 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //�ϱ� ������
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            },
                              new MatchInfo()
            {
                //���ǵ� ����
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //����
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //���̳� ���� ������ 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //����
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //�ö��� �̻��� ���
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //��� �Ķ�� 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //�ϱ� ������
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
                //���ǵ� ����
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //����
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //���̳� ���� ������ 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //����
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //�ö��� �̻��� ���
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //��� �Ķ�� 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //�ϱ� ������
                    Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
                }
            },
                              new MatchInfo()
            {
                //���ǵ� ����
                MatchType = "effd66758144a29868663aa50e85d3d95c5bc0147d7fdb9802691c2087f3416e",
                //����
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //���̳� ���� ������ 2
                TrackId = "67b33be0a18d7a045a6f1a4607b63ba90effad6a075f3238a2e4d098dd123805",
                Player = new Player()
                {
                    //����
                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                    //�ö��� �̻��� ���
                    FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                    //��� �Ķ�� 9
                    Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                    //�ϱ� ������
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
                Assert.AreEqual(match.MatchType, "���ǵ� ����");
                foreach(var matchInfo in match.Matches)
                {
                    Assert.AreEqual(matchInfo.MatchType, "���ǵ� ����");
                    Assert.AreEqual(matchInfo.Character, "����");
                    Assert.AreEqual(matchInfo.TrackId, "���̳� ���� ������ 2");
                    Assert.AreEqual(matchInfo.Player.Character, "����");
                    Assert.AreEqual(matchInfo.Player.FlyingPet, "�ö��� �̻��� ���");
                    Assert.AreEqual(matchInfo.Player.Kart, "��� �Ķ�� 9");
                    Assert.AreEqual(matchInfo.Player.Pet, "�ϱ� ������");
                }
            }
        }
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(Player)")]
        public void HashToString7()
        {
            Player player = new Player()
            {
                //����
                Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                //�ö��� �̻��� ���
                FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                //��� �Ķ�� 9
                Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                //�ϱ� ������
                Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f"
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref player);
            Assert.AreEqual(player.Character, "����");
            Assert.AreEqual(player.FlyingPet, "�ö��� �̻��� ���");
            Assert.AreEqual(player.Kart, "��� �Ķ�� 9");
            Assert.AreEqual(player.Pet, "�ϱ� ������");
        }
        [TestMethod(displayName: "�ؽ����� ���ڿ��� ����(Team)")]
        public void HashToString8()
        {

            Team team = new Team()
            {
                Players = new List<Player>()
                          {
                               new Player()
                               {
                                   //����
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //�ö��� �̻��� ���
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //��� �Ķ�� 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //�ϱ� ������
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               },
                                new Player()
                               {
                                   //����
                                    Character = "39593f7120cf68c9cb766df8021aa71034a877e6a04afd741a8d842231acd2d3",
                                    //�ö��� �̻��� ���
                                     FlyingPet = "c8f4efe77a8d14a5183050926ae0a7aaf6cabe6a1918716cbaf96763095197df",
                                                                           //��� �Ķ�� 9
                                      Kart = "ac8884ba0ee57debdb08a80523cb477a4d89a5981f75fd3398d13793a8dd4ead",
                                      //�ϱ� ������
                                       Pet = "96381e10913b82441e895139c83cff9f8364ed8d0ff5dd837adb01862be9365f",
                               }
                          }
            };
            KartAPI kartAPI = KartAPISingleton.KartAPI;
            kartAPI.Metadata.HashToString(ref team);
            foreach(var player in team.Players)
            {
                Assert.AreEqual(player.Character, "����");
                Assert.AreEqual(player.FlyingPet, "�ö��� �̻��� ���");
                Assert.AreEqual(player.Kart, "��� �Ķ�� 9");
                Assert.AreEqual(player.Pet, "�ϱ� ������");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 게임 속도
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum GameSpeed
    {
        /// <summary>
        /// 빠름
        /// </summary>
        [EnumMember(Value = "0")]
        Fast,
        /// <summary>
        /// 매우빠름
        /// </summary>
        [EnumMember(Value = "1")]
        Fastest,
        /// <summary>
        /// 가장 빠름
        /// </summary>
        [EnumMember(Value = "2")]
        Fastest2Enchant,
        /// <summary>
        /// 보통
        /// </summary>
        [EnumMember(Value = "3")]
        Normal,
        /// <summary>
        /// 무한 부스터
        /// </summary>
        [EnumMember(Value = "0")]
        Infinit
    }

    /// <summary>
    /// 특정 매치에 대한 정보
    /// </summary>
    public class MatchDetail

    {
        /// <summary>
        /// 매치 고유 식별자
        /// </summary>
        [JsonPropertyName(name: "matchId")]
        public string MatchId { get; set; }
        /// <summary>
        /// 매치 종류
        /// </summary>
        [JsonPropertyName(name: "matchType")]
        public string MatchType { get; set; }
        /// <summary>
        /// 게임 결과(블루팀 승리 or 레드팀 승리), 개인전인 경우 Solo를 반환
        /// </summary>
        [JsonPropertyName(name: "matchResult")]
        public TeamId MatchResult { get; set; }
        /// <summary>
        /// 게임 스피드 모드
        /// </summary>
        [JsonPropertyName(name: "gameSpeed")]
        public GameSpeed GameSpeed { get; set; }
        /// <summary>
        /// 게임 시작 시간
        /// </summary>
        [JsonPropertyName(name: "startTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 게임 종료 시간
        /// </summary>
        [JsonPropertyName(name: "endTime")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 게임 진행 시간, 항상 1등 기록 + 10초 값임
        /// </summary>
        [JsonPropertyName(name: "playTime")]
        public int PlayTime { get; set; }
        /// <summary>
        /// 채널 명
        /// </summary>
        [JsonPropertyName(name: "channelName")]
        public string ChannelName { get; set; }
        /// <summary>
        /// 트랙 고유 식별자
        /// </summary>
        [JsonPropertyName(name: "trackId")]
        public string TrackId { get; set; }
        /// <summary>
        /// 참가한 팀 목록(팀전일 경우에만, 그렇지 않으면 null)
        /// </summary>
        [JsonPropertyName(name: "teams")]
        public List<Team> Teams { get; set; }
        /// <summary>
        /// 참가한 플레이어 목록(개인전인 경우에만, 그렇지 않으면 null)
        /// </summary>
        [JsonPropertyName(name: "players")]
        public List<Player> Players { get; set; }

    }
}

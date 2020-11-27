using Kartrider.API.Json.Converter;

using System;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 매치 정보
    /// </summary>
    public class MatchInfo

    {
        /// <summary>
        /// 유저 고유 식별자
        /// </summary>
        [JsonPropertyName(name: "accountNo")]
        public string AccountNo { get; set; }
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
        /// 팀 종류
        /// </summary>
        [JsonPropertyName(name: "teamId")]
        public TeamId TeamId { get; set; }
        /// <summary>
        /// 사용한 캐릭터
        /// </summary>
        [JsonPropertyName(name: "character")]
        public string Character { get; set; }
        /// <summary>
        /// 승리시 true, 그렇지 않으면 false
        /// </summary>
        [JsonConverter(typeof(StringBoolToBoolConverter))]
        [JsonPropertyName(name: "matchResult")]
        public bool MatchResult { get; set; }

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
        /// 참가한 유저 수
        /// </summary>
        [JsonPropertyName(name: "playerCount")]
        public int PlayerCount { get; set; }
        /// <summary>
        /// 조회한 유저의 정보
        /// </summary>
        [JsonPropertyName(name: "player")]
        public Player Player { get; set; }
    }
}

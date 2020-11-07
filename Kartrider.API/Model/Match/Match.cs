using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 매치 타입에 해당하는 매치 정보가 담긴 클래스
    /// </summary>
    public class Match

    {
        /// <summary>
        /// 매치 종류
        /// </summary>
        [JsonPropertyName(name: "matchType")]
        public string MatchType { get; set; }
        /// <summary>
        /// 매치 상세 정보 목록 
        /// </summary>
        [JsonPropertyName(name: "matches")]
        public List<MatchInfo> Matches { get; set; }
    }
}

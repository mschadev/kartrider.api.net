using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 매치 타입에 해당하는 매치아이디 리스트가 담긴 클래스
    /// </summary>
    public class MatchesByMatchType

    {
        /// <summary>
        /// 매치 타입(GameType)
        /// </summary>
        [JsonPropertyName(name: "matchType")]
        public string MatchType { get; set; }

        /// <summary>
        /// 매치 리스트
        /// </summary>
        [JsonPropertyName(name: "matches")]
        public List<string> Matches { get; set; }

    }
}

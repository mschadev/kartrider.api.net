using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 해당 플레이어의 여러 매치 정보
    /// </summary>
    public class MatchResponse

    {
        /// <summary>
        /// 라이더명
        /// </summary>
        [JsonPropertyName(name: "nickName")]
        public string NickName { get; set; }
        /// <summary>
        /// 매치 정보 목록
        /// </summary>
        [JsonPropertyName(name: "matches")]
        public List<Match> Matches { get; set; }
    }
}

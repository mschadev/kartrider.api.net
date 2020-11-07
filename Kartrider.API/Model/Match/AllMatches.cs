using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 모든 매치별 매치아이디가 있는 클래스
    /// </summary>
    public class AllMatches

    {
        /// <summary>
        /// 타입별 매치리스트가 있는 오브젝트의 리스트
        /// </summary>
        [JsonPropertyName(name: "matches")]
        public List<MatchesByMatchType> Matches { get; set; }
    }
}

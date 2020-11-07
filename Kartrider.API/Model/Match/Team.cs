using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{

    /// <summary>
    /// 팀(블루 또는 레드) 정보
    /// </summary>
    public class Team

    {
        /// <summary>
        /// 팀전일 경우 Red or Blue, 개인전이면 Solo
        /// </summary>
        [JsonPropertyName(name: "teamId")]
        public TeamId Type { get; set; }

        /// <summary>
        /// 해당 팀의 플레이어 정보 리스트
        /// </summary>
        [JsonPropertyName(name: "players")]
        public List<Player> Players { get; set; }
    }
}

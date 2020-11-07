using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 유저 정보 클래스
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 유저 고유 식별자 
        /// </summary>
        [JsonPropertyName(name: "accessId")]
        public string AccessId { get; set; }
        /// <summary>
        /// 라이더명
        /// </summary>
        [JsonPropertyName(name: "name")]
        public string Nickname { get; set; }
        /// <summary>
        /// 유저 레벨
        /// </summary>
        [JsonPropertyName(name: "level")]
        public int Level { get; set; }
    }
}

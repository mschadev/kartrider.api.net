using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// 팀 종류
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum TeamId
    {
        /// <summary>
        /// 개인전일때만
        /// </summary>
        [EnumMember(Value = "0")]
        Solo,
        /// <summary>
        /// 레드 팀 승리
        /// </summary>
        [EnumMember(Value = "1")]
        Red,
        /// <summary>
        /// 블루 팀 승리
        /// </summary>
        [EnumMember(Value = "2")]
        Blue
    }
}

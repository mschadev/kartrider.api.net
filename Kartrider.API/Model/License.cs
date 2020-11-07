using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Kartrider.API.Model
{
    /// <summary>
    /// License enum
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum License
    {
        /// <summary>
        /// 알 수 없음
        /// </summary>
        [EnumMember(Value = "")]
        Unknown = -1,
        /// <summary>
        /// 라이센스 없음
        /// </summary>
        [EnumMember(Value = "0")]
        None = 0,
        /// <summary>
        /// 초보
        /// </summary>
        [EnumMember(Value = "1")]
        Beginner,
        /// <summary>
        /// 뉴비
        /// </summary>
        [EnumMember(Value = "2")]
        Newbie,
        /// <summary>
        /// L3
        /// </summary>
        [EnumMember(Value = "3")]
        L3,
        /// <summary>
        /// L2
        /// </summary>
        [EnumMember(Value = "4")]
        L2,
        /// <summary>
        /// L1
        /// </summary>
        [EnumMember(Value = "5")]
        L1,
        /// <summary>
        /// PRO
        /// </summary>
        [EnumMember(Value = "6")]
        PRO

    }
}

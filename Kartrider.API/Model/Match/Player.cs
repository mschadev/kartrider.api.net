using Kartrider.API.Json.Converter;
using System;
using System.Text.Json.Serialization;
namespace Kartrider.API.Model
{
    /// <summary>
    /// 해당 매치의 플레이어 정보
    /// </summary>
    public class Player

    {
        /// <summary>
        /// 유저 고유 식별자
        /// </summary>
        [JsonPropertyName(name: "accountNo")]
        public string AccountNo { get; set; }
        /// <summary>
        /// 사용한 캐릭터
        /// </summary>
        [JsonPropertyName(name: "character")]
        public string Character { get; set; }
        /// <summary>
        /// 탑승하고 있는 카트바디
        /// </summary>
        [JsonPropertyName(name: "kart")]
        public string Kart { get; set; }
        /// <summary>
        /// 플레이어의 라이센스
        /// </summary>
        [JsonPropertyName(name: "rankinggrade2")]
        public License License { get; set; }
        /// <summary>
        /// 사용하고 있는 펫
        /// </summary>
        [JsonPropertyName(name: "pet")]
        public string Pet { get; set; }
        /// <summary>
        /// 사용하고 있는 플라잉 펫
        /// </summary>
        [JsonPropertyName(name: "flyingPet")]
        public string FlyingPet { get; set; }
        /// <summary>
        /// 탑승하고 있는 카트바디의 엔진 파츠(9 엔진 이하)
        /// </summary>
        [JsonPropertyName(name: "partsEngine")]
        public string PartsEngine { get; set; }
        /// <summary>
        /// 탑승하고 있는 카트바디의 핸들 파츠(9 엔진 이하)
        /// </summary>
        [JsonPropertyName(name: "partsHandle")]
        public string PartsHandle { get; set; }
        /// <summary>
        /// 탑승하고 있는 카트바디의 휠 파츠(9 엔진 이하)
        /// </summary>
        [JsonPropertyName(name: "partsWheel")]
        public string PartsWheel { get; set; }
        /// <summary>
        /// 탑승하고 있는 카트바디의 킷 파츠(9 엔진 이하)
        /// </summary>
        [JsonPropertyName(name: "partsKit")]
        public string PartsKit { get; set; }
        /// <summary>
        /// 현재 매치에서 플레이어 순위
        /// </summary>
        [JsonConverter(typeof(StringIntToIntConverter))]
        [JsonPropertyName(name: "matchRank")]
        public int MatchRank { get; set; }
        /// <summary>
        /// 현재 매치의 승리 여부
        /// </summary>
        [JsonConverter(typeof(StringBoolToBoolConverter))]
        [JsonPropertyName(name: "matchWin")]
        public bool MatchWin { get; set; }
        /// <summary>
        /// 현재 매치의 기록
        /// </summary>
        [JsonConverter(typeof(StringToTimeSpanConverter))]
        [JsonPropertyName(name: "matchTime")]
        public TimeSpan MatchTime { get; set; }
        /// <summary>
        /// 리타이어 여부
        /// </summary>
        [JsonConverter(typeof(StringBoolToBoolConverter))]
        [JsonPropertyName(name: "matchRetired")]
        public bool MatchRetired { get; set; }
        //public string rankinggrade2 { get; set; }
        /// <summary>
        /// 플레이어 이름
        /// </summary>
        [JsonPropertyName(name: "characterName")]
        public string CharacterName { get; set; }
    }
}

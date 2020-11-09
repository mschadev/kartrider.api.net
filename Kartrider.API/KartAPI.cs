using Kartrider.API.Model;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Kartrider.API
{
    /// <summary>
    /// Kartrider OPEN API 클래스
    /// </summary>
    public class KartAPI
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// 메타데이터
        /// </summary>
        public Metadata Metadata { get; } = new Metadata();

        /// <summary>
        /// KartAPI 생성자
        /// </summary>
        /// <param name="token">API 토큰</param>
        /// <param name="initMetadata">true시 메타데이터 업데이트, 그렇지 않으면 업데이트 하지 않음(직접 해야함)</param>
        public KartAPI(string token, bool initMetadata = false)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Define.API_URL_V1),
            };
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            if (initMetadata)
            {
                var respone = _httpClient.GetByteArrayAsync(Define.METADATA_URL);
                respone.Wait();
                Metadata.Update(respone.Result);
            }
        }
        /// <summary>
        /// 메타데이터 압축 파일을 다운로드한다.
        /// </summary>
        /// <param name="path">저장할 경로</param>
        public static void DownloadMetadata(string path)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetByteArrayAsync(Define.METADATA_URL);
                response.Wait();
                byte[] data = response.Result;
                response.Dispose();
                File.WriteAllBytes(path, response.Result);
            }
        }
        /// <summary>
        /// 유저 고유 식별자로 유저의 정보를 조회한다.
        /// </summary>
        /// <param name="accessId">유저 고유 식별자</param>
        /// <returns></returns>
        public UserInfo GetUserInfoByAccessId(string accessId)
        {
            var respone = _httpClient.GetStringAsync($"users/{accessId}");
            respone.Wait();
            return JsonSerializer.Deserialize<UserInfo>(respone.Result);
        }
        /// <summary>
        /// 라이더명으로 유저의 정보를 조회한다.
        /// </summary>
        /// <param name="nickname">플레이어의 닉네임</param>
        /// <returns></returns>
        public UserInfo GetUserInfoByNickname(string nickname)
        {
            var respone = _httpClient.GetStringAsync($"users/nickname/{nickname}");
            respone.Wait();
            return JsonSerializer.Deserialize<UserInfo>(respone.Result);
        }
        /// <summary>
        /// 유저의 매치 리스트를 매치 타입별, startDate 기준 내림차순으로 반환한다.
        /// </summary>
        /// <param name="accessId">유저 고유 식별자</param>
        /// <param name="startDate">조회 시작 날짜 (UTC) (ex: 2019-02-15 01:00:00)</param>
        /// <param name="endDate">조회 끝 날짜 (UTC) (ex: 2019-02-15 02:00:00)</param>
        /// <param name="offset">오프셋</param>
        /// <param name="limit">조회 수</param>
        /// <param name="matchTypes">매치 타입 HashID 목록</param>
        /// <returns></returns>
        public MatchResponse GetMatchesByAccessId(string accessId, DateTime? startDate = null, DateTime? endDate = null, int offset = 0, int limit = 10, string[] matchTypes = null)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("Valid 'offset' Value Range: 0 < offset");
            }
            if (limit < 1 || 500 < limit)
            {
                throw new ArgumentOutOfRangeException("Valid 'limit' Value Range: 1 < limit < 500");
            }

            if (startDate != null)
            {
                startDate = startDate.Value.ToUniversalTime();
            }
            if (endDate != null)
            {
                endDate = endDate.Value.ToUniversalTime();
            }
            string matchTypeQueryValue = matchTypes == null ? "" : string.Join(",", matchTypes);
            string query = $"users/{accessId}" +
                $"/matches?start_date={(startDate == null ? "" : startDate.Value.ToString(Define.DATETIME_FORMAT))}" +
                $"&end_date={(endDate == null ? "" : endDate.Value.ToString(Define.DATETIME_FORMAT))} &offset={offset}&limit={limit}" +
                $"&match_types={matchTypeQueryValue}";
            var respone = _httpClient.GetStringAsync(query);
            respone.Wait();
            return JsonSerializer.Deserialize<MatchResponse>(respone.Result);
        }
        /// <summary>
        /// 모든 유저의 매치 리스트를 매치 타입별, start_date 기준 내림차순으로 반환한다.
        /// </summary>
        /// <param name="startDate">조회 시작 날짜 (UTC) (ex: 2019-02-15 01:00:00)</param>
        /// <param name="endDate">조회 끝 날짜 (UTC) (ex: 2019-02-15 02:00:00)</param>
        /// <param name="offset">오프셋</param>
        /// <param name="limit">조회 수</param>
        /// <param name="matchTypes">매치 타입 HashID 목록</param>
        /// <returns></returns>
        public AllMatches GetAllMatches(DateTime? startDate = null, DateTime? endDate = null, int offset = 0, int limit = 10, string[] matchTypes = null)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("Valid 'offset' Value Range: 0 < offset");
            }
            if (limit < 1 || 500 < limit)
            {
                throw new ArgumentOutOfRangeException("Valid 'limit' Value Range: 1 < limit < 500");
            }
            if (startDate != null)
            {
                startDate = startDate.Value.ToUniversalTime();
            }
            if (endDate != null)
            {
                endDate = endDate.Value.ToUniversalTime();
            }
            string matchTypeQueryValue = matchTypes == null ? "" : string.Join(",", matchTypes);
            string query = $"matches/all" +
                $"?start_date={(startDate == null ? "" : startDate.Value.ToString(Define.DATETIME_FORMAT))}" +
                $"&end_date={(endDate == null ? "" : endDate.Value.ToString(Define.DATETIME_FORMAT))}&offset={offset}&limit={limit}" +
                $"&match_types={matchTypeQueryValue}";
            var respone = _httpClient.GetStringAsync(query);
            respone.Wait();
            return JsonSerializer.Deserialize<AllMatches>(respone.Result);
        }
        /// <summary>
        /// 매치 고유 식별자로 특정 매치의 상세 정보를 조회한다.
        /// </summary>
        /// <param name="matchId">매치 고유 식별자</param>
        /// <returns></returns>
        public MatchDetail GetMatchDetail(string matchId)
        {
            string query = $"matches/{matchId}";
            var respone = _httpClient.GetStringAsync(query);
            respone.Wait();
            return JsonSerializer.Deserialize<MatchDetail>(respone.Result);
        }
    }
}

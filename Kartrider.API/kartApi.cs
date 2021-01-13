using Kartrider.API.Exceptions;
using Kartrider.API.Model;

using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Kartrider.API
{
    /// <summary>
    /// Kartrider OPEN API 클래스
    /// </summary>
    public class KartApi
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// KartApi 생성자
        /// </summary>
        /// <param name="apiKey">API 키</param>
        public KartApi(string apiKey)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Define.API_URL_V1),
            };
            _httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
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
            string query = $"users/{accessId}";
            string responseStr;
            try
            {
                var response = _httpClient.GetStringAsync(query);
                response.Wait();
                responseStr = response.Result;
            }
            catch(Exception e)
            {
                throw CreateKartApiRequestException(e, query);
            }
            return JsonSerializer.Deserialize<UserInfo>(responseStr);
        }
        /// <summary>
        /// 라이더명으로 유저의 정보를 조회한다.
        /// </summary>
        /// <param name="nickname">플레이어의 닉네임</param>
        /// <returns></returns>
        public UserInfo GetUserInfoByNickname(string nickname)
        {
            string query = $"users/nickname/{nickname}";
            string responseStr;
            try
            {
                var response = _httpClient.GetStringAsync($"users/nickname/{nickname}");
                response.Wait();
                responseStr = response.Result;
            }
            catch(Exception e)
            {
                throw CreateKartApiRequestException(e, query);
            }
            return JsonSerializer.Deserialize<UserInfo>(responseStr);
        }
        /// <summary>
        /// 유저의 매치 리스트를 매치 타입별, startDate 기준 내림차순으로 반환한다.
        /// </summary>
        /// <param name="accessId">유저 고유 식별자</param>
        /// <param name="startDate">조회 시작 날짜 (UTC 기준) (ex: 2019-02-15 01:00:00)</param>
        /// <param name="endDate">조회 끝 날짜 (UTC 기준) (ex: 2019-02-15 02:00:00)</param>
        /// <param name="offset">오프셋</param>
        /// <param name="limit">조회 수</param>
        /// <param name="matchTypes">매치 타입 HashID 목록</param>
        /// <returns></returns>
        public MatchResponse GetMatchesByAccessId(string accessId, DateTime? startDate = null, DateTime? endDate = null, int offset = 0, int limit = 10, string[] matchTypes = null)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset","Valid 'offset' Value Range: 0 < offset");
            }
            if (limit < 1 || 500 < limit)
            {
                throw new ArgumentOutOfRangeException("limit","Valid 'limit' Value Range: 1 < limit < 500");
            }
            string matchTypeQueryValue = matchTypes == null ? "" : string.Join(",", matchTypes);
            string query = $"users/{accessId}" +
                $"/matches?start_date={(startDate == null ? "" : startDate.Value.ToString(Define.DATETIME_FORMAT))}" +
                $"&end_date={(endDate == null ? "" : endDate.Value.ToString(Define.DATETIME_FORMAT))} &offset={offset}&limit={limit}" +
                $"&match_types={matchTypeQueryValue}";
            string responseStr;
            try
            {
                var response = _httpClient.GetStringAsync(query);
                response.Wait();
                responseStr = response.Result;
            }
            catch(Exception e)
            {
                throw CreateKartApiRequestException(e, query);
            }
            return JsonSerializer.Deserialize<MatchResponse>(responseStr);
        }
        /// <summary>
        /// 모든 유저의 매치 리스트를 매치 타입별, start_date 기준 내림차순으로 반환한다.
        /// </summary>
        /// <param name="startDate">조회 시작 날짜 (UTC 기준) (ex: 2019-02-15 01:00:00)</param>
        /// <param name="endDate">조회 끝 날짜 (UTC 기준) (ex: 2019-02-15 02:00:00)</param>
        /// <param name="offset">오프셋</param>
        /// <param name="limit">조회 수</param>
        /// <param name="matchTypes">매치 타입 HashID 목록</param>
        /// <returns></returns>
        public AllMatches GetAllMatches(DateTime? startDate = null, DateTime? endDate = null, int offset = 0, int limit = 10, string[] matchTypes = null)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset","Valid 'offset' Value Range: 0 < offset");
            }
            if (limit < 1 || 500 < limit)
            {
                throw new ArgumentOutOfRangeException("limit","Valid 'limit' Value Range: 1 < limit < 500");
            }
            string matchTypeQueryValue = matchTypes == null ? "" : string.Join(",", matchTypes);
            string query = $"matches/all" +
                $"?start_date={(startDate == null ? "" : startDate.Value.ToString(Define.DATETIME_FORMAT))}" +
                $"&end_date={(endDate == null ? "" : endDate.Value.ToString(Define.DATETIME_FORMAT))}&offset={offset}&limit={limit}" +
                $"&match_types={matchTypeQueryValue}";
            string responseStr;
            try
            {
                var response = _httpClient.GetStringAsync(query);
                response.Wait();
                responseStr = response.Result;
            }
            catch(Exception e)
            {
                throw CreateKartApiRequestException(e, query);
            }
            return JsonSerializer.Deserialize<AllMatches>(responseStr);
        }
        /// <summary>
        /// 매치 고유 식별자로 특정 매치의 상세 정보를 조회한다.
        /// </summary>
        /// <param name="matchId">매치 고유 식별자</param>
        /// <returns></returns>
        public MatchDetail GetMatchDetail(string matchId)
        {
            string query = $"matches/{matchId}";
            string responseStr;
            try
            {
                var response = _httpClient.GetStringAsync(query);
                response.Wait();
                responseStr = response.Result;
            }
            catch(Exception e)
            {
                throw CreateKartApiRequestException(e, query);
            }
            return JsonSerializer.Deserialize<MatchDetail>(responseStr);
        }
        private KartApiRequestException CreateKartApiRequestException(Exception e,string queryParameter)
        {
            string apiKey = _httpClient.DefaultRequestHeaders.Authorization.Scheme;
            string statusCode = Regex.Match(e.Message, @"\d{3}").Value;
            KartApiStatusCode KartApiStatusCode = (KartApiStatusCode)Enum.Parse(typeof(KartApiStatusCode), statusCode);
            string message = e.Message;
            return new KartApiRequestException(message, queryParameter, apiKey, KartApiStatusCode);
        }
    }
}

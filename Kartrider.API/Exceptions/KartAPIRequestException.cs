using System;
using System.Collections.Generic;
using System.Text;

namespace Kartrider.API.Exceptions
{
    /// <summary>
    /// API 호출 예외 클래스
    /// </summary>
    public class KartAPIRequestException : System.Exception
    {
        /// <summary>
        /// API Key 뒷자리에서 10자만 자른 키값
        /// </summary>
        public string APIKey { get; set; }
        /// <summary>
        /// 요청한 API Url에서 도메인을 제외한 파라미터 문자열
        /// </summary>
        public string QueryParameter { get; set; }
        /// <summary>
        /// 호출 결과 상태 코드
        /// </summary>
        public KartAPIStatusCode StatusCode { get; set; }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="message">메시지</param>
        /// <param name="queryParameter">API 호출 URL의 쿼리 파라미터</param>
        /// <param name="apiKey">API Key</param>
        /// <param name="kartAPIStatusCode">API 호출 결과 상태 코드</param>
        public KartAPIRequestException(string message,string queryParameter, string apiKey,KartAPIStatusCode kartAPIStatusCode) : base(message)
        {
            QueryParameter = queryParameter;
            StatusCode = kartAPIStatusCode;
            APIKey = apiKey?.Substring(apiKey.Length - 10, 10);
        }
       
    }
    /// <summary>
    /// API 호출 결과 상태 코드
    /// </summary>
    public enum KartAPIStatusCode
    {
        /// <summary>
        /// 요청 형식 오류 (잘못된 파라미터 입력)
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// 미승인 서비스 (미지원 service, service type)
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// 허용되지 않은 AccessToken(API Key) 사용
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// 해당 리소스가 존재하지 않음
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// 미지원 API
        /// </summary>
        MethodNotAllowed = 405,
        /// <summary>
        /// 너무 긴 요청 파라미터 입력
        /// </summary>
        RequestEntityTooLarge = 413,
        /// <summary>
        /// AccessToken의 요청 허용량(Rate Limit) 초과
        /// </summary>
        TooManyRequest = 429,
        /// <summary>
        /// 서버 내부 에러
        /// </summary>
        InternalServerError = 500,
        /// <summary>
        /// 서버 내부 처리 timeout
        /// </summary>
        GatewayTimeout = 504

    }
}

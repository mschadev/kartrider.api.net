# kartrider-api-net
[![standard-readme compliant](https://img.shields.io/badge/standard--readme-OK-green.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)
![Ubuntu .NET Core](https://github.com/zxc010613/kartrider.api.net/workflows/Ubuntu%20.NET%20Core/badge.svg)
![Nuget package version](https://img.shields.io/nuget/v/kartrider.api?label=nuget%20package)  
넥슨 개발자 센터에서 제공중인 API의 .NET용 라이브러리입니다.  
> 넥슨과 어떠한 연관도 없습니다.

**특징**   
+ 쉬운 API 사용
+ 메타데이터 지원
+ [HttpClient](https://docs.microsoft.com/ko-kr/dotnet/api/system.net.http.httpclient?view=netcore-3.1)클래스 사용
+ [NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)
## 목차
- [설치](#설치)
    - [패키지 매니저](#패키지-매니저)
    - [NET CLI](#NET-CLI)
    - [PackageReference](#PackageReference)
- [사용법](#사용법)
- [개발 및 테스트](#개발-및-테스트)
    - [파일](#파일)
    - [환경 변수](#환경-변수)
- [관리자](#관리자)
- [기여](#기여)
- [라이센스](#라이센스)

## 설치
### 패키지 매니저
```PM
Install-Package Kartrider.API -Version 1.0.3
```
### NET CLI
```bash
dotnet add package Kartrider.API --version 1.0.3
```
### PackageReference
```xml
<PackageReference Include="Kartrider.API" Version="1.0.3" />
```

## 사용법
```cs
using Kartrider.API;
using System;
namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "APIKEY";
            KartAPI kartAPI = new KartAPI(apiKey);
        }
    }
}
```
HTTP 통신은 [HttpClient](https://docs.microsoft.com/ko-kr/dotnet/api/system.net.http.httpclient?view=netcore-3.1)를 사용하였으므로 싱글톤 패턴으로 구현해서 사용하는 것을 추천합니다.
+ [싱글톤 패턴 위키](https://github.com/zxc010613/kartrider.api.net/wiki/%EC%8B%B1%EA%B8%80%ED%86%A4-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4%EC%97%90-%EB%8C%80%ED%95%B4)
## 개발 및 테스트
해당 프로젝트는 유닛 테스트로 MSTEST를 사용중입니다.  
테스트할 때 사용할 키는 **파일 방식**과 **환경 변수 방식**을 지원하고 있습니다.  
둘다 사용시, 우선 순위는 파일이 더 높습니다.
### 파일
![image1](/image/image1.jpg)  
유닛 테스트 프로젝트인 `Kartrider.API.Tests`에 `Key.txt`파일을 추가하시고, 파일에 해당 키를 작성하면 됩니다.  
> 참고: Key.txt 파일에는 개행이 없어야합니다.
### 환경 변수
**파워셀**
```ps
($env:UNIT_TEST_API_KEY="KEY") | dotnet test --configuration Release
```
**CMD**
```cmd
SET UNIT_TEST_API_KEY=KEY & dotnet test
```
## 관리자
[@zxc010613](https://github.com/zxc010613)

## 기여
이슈나 풀 리퀘스트 부담없이 해주세요.

## 라이센스
[MIT](./LICENSE)

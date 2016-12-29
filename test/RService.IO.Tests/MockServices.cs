﻿using Microsoft.AspNetCore.Authorization;
using RService.IO.Abstractions;

namespace RService.IO.Tests
{
    public class SvcWithMethodRoute : ServiceBase
    {
        public const string RoutePath = "/Foobar";
        public const string GetPath = "/Foobar/Method";
        public const string PostPath = "/Foobar/Method";
        public bool HasAnyBeenCalled { get; set; }
        public string GetResponse { get; set; }
        public int PostResponse { get; set; }

        [Route(RoutePath, RestVerbs.Get)]
        public object Any()
        {
            HasAnyBeenCalled = true;
            return null;
        }

        [Route(GetPath, RestVerbs.Get)]
        public string Get()
        {
            return GetResponse;
        }

        [Route(PostPath, RestVerbs.Post)]
        public int Post()
        {
            return PostResponse;
        }

        [Route(RoutePath, RestVerbs.Put)]
        public ResponseDto Put()
        {
            return new ResponseDto();
        }
    }

    public class SvcAuthRoutes : ServiceBase
    {
        public const string AuthorizedPath = "/Auth";
        public const string UnauthorizedPath = "/FailedAuth";

        [Route(AuthorizedPath)]
        [Authorize(Roles = "Administrator")]
        public object Authorized()
        {
            return null;
        }
        [Route(UnauthorizedPath)]
        [Authorize(Roles = "FakeRole")]
        public object Unuthorized()
        {
            return null;
        }
    }

    public class SvcBase : ServiceBase
    {
        public const string Path = "/SvcBase";

        [Route(Path)]
        public object Any()
        {
            return null;
        }
    }

    public class SvcWithMultMethodRoutes : ServiceBase
    {
        public const string RoutePath1 = "/Foobar/Llamas";
        public const string RoutePath2 = "/Foobar/Eats";

        [Route(RoutePath1, RestVerbs.Get)]
        [Route(RoutePath2, RestVerbs.Get)]
        public object Any()
        {
            return null;
        }
    }

    public class SvcWithParamRoute : ServiceBase
    {
        public const string RoutePath = "/Llamas";
        public const string RoutePathUri = "/Llamas/Foo/{Foobar}";
        public const string RoutePathUri2 = "/Llamas/Fizz/{Llama}";

        public object Get(DtoForParamRoute dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Foobar))
                return dto.Foobar;

            return dto.Llama;
        }

        public object Any(DtoForParamQueryRoute dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Foobar))
                return dto.Foobar;

            return dto.Llama;
        }

        public ResponseDto Put(RequestDto dto)
        {
            return new ResponseDto {Foobar = dto.Foobar};
        }
    }

    public class SvcWithMultParamRoutes : ServiceBase
    {
        public const string RoutePath1 = "/Llamas/Eats";
        public const string RoutePath2 = "/Llamas/Hands";

        public object Any(DtoForMultParamRoutes dto)
        {
            return null;
        }
    }

    public class SvcForMethods : ServiceBase
    {
        public const string PostPath = "/Methods";
        public const RestVerbs PostMethod = RestVerbs.Post;

        public const string MultiPath = "/Multi";
        public const RestVerbs MultiMethod = RestVerbs.Get | RestVerbs.Post;

        [Route(PostPath, PostMethod)]
        public object Post()
        {
            return null;
        }

        [Route(MultiPath, MultiMethod)]
        public object GetPost()
        {
            return null;
        }
    }

    [Route(SvcWithParamRoute.RoutePath, RestVerbs.Get)]
    public class DtoForParamRoute
    {
        public string Foobar { get; set; }
        public int Llama { get; set; }
    }

    [Route(SvcWithParamRoute.RoutePathUri, RestVerbs.Get)]
    [Route(SvcWithParamRoute.RoutePathUri2, RestVerbs.Get)]
    public class DtoForParamQueryRoute
    {
        public string Foobar { get; set; }
        public int Llama { get; set; }
    }

    [Route(SvcWithMultParamRoutes.RoutePath1, RestVerbs.Get)]
    [Route(SvcWithMultParamRoutes.RoutePath2, RestVerbs.Get)]
    public class DtoForMultParamRoutes
    {
        
    }

    [Route(SvcWithParamRoute.RoutePath, RestVerbs.Put)]
    public class RequestDto
    {
        public string Foobar { get; set; }
    }

    public class ResponseDto
    {
        public string Foobar { get; set; }
    }
}
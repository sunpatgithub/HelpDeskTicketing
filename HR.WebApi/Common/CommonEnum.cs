﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Common
{
    public class CommonEnum
    {
        public  enum StatusCode
        {
            Continue = 100,
            SwitchingProtocols = 101,
            OK = 200,
            Created = 201,
            Accepted = 202,
            NonAuthoritativeInformation = 203,
            NoContent = 204,
            ResetContent = 205,
            PartialContent = 206,
            Ambiguous = 300,
            Moved = 301,
            Found = 302,
            RedirectMethod = 303,
            NotModified = 304,
            UseProxy = 305,
            Unused = 306,
            RedirectKeepVerb = 307,
            MovedPermanently = 308,
            MultipleChoices = 309,
            Redirect = 310,
            SeeOther = 311,
            TemporaryRedirect = 312,
            BadRequest = 400,
            Unauthorized = 401,
            PaymentRequired = 402,
            Forbidden = 403,
            NotFound = 404,
            MethodNotAllowed = 405,
            NotAcceptable = 406,
            ProxyAuthenticationRequired = 407,
            RequestTimeout = 408,
            Conflict = 409,
            Gone = 410,
            LengthRequired = 411,
            PreconditionFailed = 412,
            RequestEntityTooLarge = 413,
            RequestUriTooLong = 414,
            UnsupportedMediaType = 415,
            RequestedRangeNotSatisfiable = 416,
            ExpectationFailed = 417,
            UpgradeRequired = 426,
            InternalServerError = 500,
            NotImplemented = 501,
            BadGateway = 502,
            ServiceUnavailable = 503,
            GatewayTimeout = 504,            
            HttpVersionNotSupported	= 505,
            Exception = 510,
            NotExtended = 513,
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace AppSonga.MessageHandlers
{
    public class ApiKeyMessageHandler: DelegatingHandler
    {
        //private const string APIKeytoCheck = "SoNgAApI18k12s20d19d22C";
        //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellation)
        //{
        //    bool validKey = false;
        //    IEnumerable<string> requestHeaders;
        //    var checkApiKeyExists = httpRequestMessage.Headers.TryGetValues("APIkey", out requestHeaders);
        //    if (checkApiKeyExists)
        //    {
        //        if (requestHeaders.FirstOrDefault().Equals(APIKeytoCheck))
        //        {
        //            validKey = true;
        //        }
        //    }
        //    if (!validKey)
        //    {
        //        return httpRequestMessage(HttpStatusCode.Forbidden, "Invalid Api Key");
        //    }
        //}
    }
}

using System;
using RestSharp;

namespace Timecards.Infrastructure
{
    public class ApiRequestFactory: IApiRequestFactory
    {
        private readonly Uri _baseUri;

        public ApiRequestFactory(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public IRestClient CreateClient()
        {
             return new RestClient(_baseUri);
        }
    }
}

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
             var client = new RestClient(_baseUri);

            return client;
        }
    }
}

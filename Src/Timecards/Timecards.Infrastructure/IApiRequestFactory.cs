using System;
using RestSharp;

namespace Timecards.Infrastructure
{
    public interface IApiRequestFactory
    {
        IRestClient CreateClient();
    }
}

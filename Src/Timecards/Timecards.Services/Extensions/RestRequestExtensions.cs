using RestSharp;
using Timecards.Application;

namespace Timecards.Services.Extensions
{
    public static class RestRequestExtensions
    {
        public static void AddAuthorizationHeader(this RestRequest restRequest)
        {
            restRequest.AddHeader("Authorization", $"Bearer {TokenStore.Login.Token}");
        }
    }
}
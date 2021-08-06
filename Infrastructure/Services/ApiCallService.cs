
using Infrastructure.DTO;
using Infrastructure.Services.Interfaces;
using Infrastructure.Settings;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ApiCallService : IApiCallService
    {
        private readonly RestClient _restClient;
        private readonly ApiSettings _apiSettings;

        public ApiCallService(
            ApiSettings apiSettings
            )
        {
            _apiSettings = apiSettings;

            _restClient = new RestClient(_apiSettings.MainUrl);
            _restClient.AddDefaultHeader("Content-Type", "application/json");

            _restClient.Authenticator = new HttpBasicAuthenticator(_apiSettings.Username, _apiSettings.Password);

            _restClient.Timeout = _apiSettings.Timeout;
        }

        public async Task<JsonFromRestApiResponse> SendJson(List<JsonForRestApi> jsonToSend)
        {
            RestRequest restRequest = new RestRequest($"{_apiSettings.Endpoint}");
            restRequest.AddJsonBody(jsonToSend);

            JsonFromRestApiResponse result = await _restClient.PostAsync<JsonFromRestApiResponse>(restRequest);

            return result;
        }
    }
}

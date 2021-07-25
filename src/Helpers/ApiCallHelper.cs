using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace PokeIndex.Helpers
{
    /// <summary>
    /// Helper to call the external web API and process results
    /// </summary>
    public class ApiCallHelper {

        /// <summary>
        /// Helper Task for calling external APIs
        /// </summary>
        public async Task<ClientHelperResponse> MakeCall(HttpClient httpClient, HttpRequestMessage httpRequest)
        {
            var responseModel = new ClientHelperResponse();
            var response = await httpClient.SendAsync(httpRequest);
                responseModel.Status = (int?) response.StatusCode;
                responseModel.Succeeded = response.IsSuccessStatusCode;

                if (responseModel.Succeeded)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var reader = new StreamReader(responseStream);
                    responseModel.APIResponse = reader.ReadToEnd();
                }
                else
                {
                    // Should log error here.
                 
                }
                return responseModel;

        }
    }
}

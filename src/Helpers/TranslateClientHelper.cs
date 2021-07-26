using System.Net.Http;
using PokeIndex.Models;
using PokeIndex.Constants;
using System.Web;
using System.Text;
using Newtonsoft.Json.Linq;


namespace PokeIndex.Helpers
{
    /// <summary>
    /// Helper to call the external web API and process results
    /// </summary>
    public class TranslateClientHelper {
        private readonly IHttpClientFactory _clientFactory;
        
        /// <summary>
        /// Constructor allows dependancy injection of named clients from the factory.
        /// </summary>
        public TranslateClientHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Function to call helper for Translation API
        /// and manage the results.
        /// </summary>
        public Pokemon TranslatePokemon(Pokemon pokemon, string translationType)
        {
            var content = pokemon.Description;
            content = "{" + $"\"text\":\"{content}\"" + "}";
            var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{translationType}.json"
            );
            request.Content = new StringContent(content, Encoding.UTF8, "application/json"); 

            var client = _clientFactory.CreateClient(APIs.TRANSLATE);
            var callHelper = new ApiCallHelper();
            var result = callHelper.MakeCall(client, request).Result;

            if(result.Succeeded) 
            {
                JObject jsonObject = JObject.Parse(result.APIResponse);
                pokemon.Description = jsonObject.SelectToken("contents.translated").ToString();
            } else {
                // log errors
                // Pokemon is returned in same state as it arrived.
            }
            return pokemon;
        }

    }

}
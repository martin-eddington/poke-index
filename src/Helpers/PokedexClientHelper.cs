using System.Net.Http;
using PokeIndex.Models;
using PokeIndex.Constants;
using Newtonsoft.Json.Linq;


namespace PokeIndex.Helpers
{
    /// <summary>
    /// Helper to call the external web API and process results
    /// </summary>
    public class PokedexClientHelper {
    private readonly IHttpClientFactory _clientFactory;
    
    /// <summary>
    /// Constructor allows dependancy injection of named clients from the factory.
    /// </summary>
    public PokedexClientHelper(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// Function to get the Pokemon from the external API
    /// </summary>
    public Pokemon GetPokemon(string PokemonName)
    {
        var populatedModel = new Pokemon();
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"pokemon/{PokemonName}"
            );

        var client = _clientFactory.CreateClient(APIs.POKEDEX);
        var callHelper = new ApiCallHelper();
        var result = callHelper.MakeCall(client, request).Result;

        // Check initial result
        if(result.Succeeded) {
            // get species Id
            JObject jsonObject = JObject.Parse(result.APIResponse);
            populatedModel.Name = jsonObject.SelectToken("species.name").ToString();

            var speciesId = jsonObject.SelectToken("id").ToString();
            // make new call to pokemon-species
            request = new HttpRequestMessage(
                HttpMethod.Get,
                $"pokemon-species/{speciesId}"
                );
            result = callHelper.MakeCall(client, request).Result;
            if(result.Succeeded)
            {
                jsonObject = JObject.Parse(result.APIResponse);
                populatedModel.IsLegendary= (bool?) jsonObject.SelectToken("is_legendary");
                populatedModel.Habitat = jsonObject.SelectToken("habitat.name").ToString();

                // ToDo - make sure we're using English if it's not first entry
                populatedModel.Description = jsonObject.SelectToken("flavor_text_entries[0].flavor_text").ToString();
                // quick clean-up of line breaks and form feeds from the description
                populatedModel.Description = populatedModel.Description
                                                .Replace("\n", " ")
                                                .Replace("\r", " ")
                                                .Replace("\f", " ");
            }
        }
        return populatedModel;
        }
       
    }

    
}

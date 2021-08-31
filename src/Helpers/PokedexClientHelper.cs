using System.Net.Http;
using PokeIndex.Models;
using PokeIndex.Constants;
using Newtonsoft.Json.Linq;
using System.Linq;


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

                // Make sure we're using English if it's not first entry
                // Not entirely sure LINQ is the best way - maybe there's a better way using NewtonSoft stuff.
                var flavorTextArray = (JArray)jsonObject.SelectToken("flavor_text_entries");
                var englishTextArray = from item in flavorTextArray.Children()
                where item["language"]["name"].ToString() == "en"
                select item["flavor_text"];

                populatedModel.Description = englishTextArray.First().ToString();
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

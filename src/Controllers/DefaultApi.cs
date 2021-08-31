/*
 * Pokemon Index API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PokeIndex.Attributes;

using Microsoft.AspNetCore.Authorization;
using PokeIndex.Models;
using PokeIndex.Helpers;
using PokeIndex.Constants;

namespace PokeIndex.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DefaultApiController : ControllerBase
    { 
        private readonly IHttpClientFactory _clientFactory;
    
        /// <summary>
        /// Constructor allows dependancy injection of named clients from the factory.
        /// </summary>
        public DefaultApiController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        /// <summary>
        /// Returns pokemon name, description, habitat and legendary status
        /// </summary>
        /// <param name="pokemonName">The name of the pokemon to retrieve</param>
        /// <response code="200">Expected response to a valid request</response>
        /// <response code="404">No such Pokemon</response>

        
        [HttpGet]
        [Route("/v1/pokemon/{pokemonName}")]
        [ValidateModelState]
        [SwaggerOperation("ShowPokemonByName")]
        [SwaggerResponse(statusCode: 200, type: typeof(Pokemon), description: "Expected response to a valid request")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "No such Pokemon")]


        public IActionResult ShowPokemonByName([FromRoute][Required]string pokemonName)
        { 
           try {
            var result = GetPokemon(pokemonName);
            return new ObjectResult(result);
           }
           catch (PokemonNotFoundException e)
           {
             return StatusCode(404,new Error(){Code=404, Message="No such Pokemon"}); 
           }
        }

        /// <summary>
        /// Returns pokemon name, translated description, habitat and legendary status
        /// </summary>
        /// <param name="pokemonName">The name of the pokemon to retrieve</param>
        /// <response code="200">Expected response to a valid request</response>
        /// <response code="404">No such Pokemon</response>
        [HttpGet]
        [Route("/v1/pokemon/translated/{pokemonName}")]
        [ValidateModelState]
        [SwaggerOperation("ShowTranslatedPokemonByName")]
        [SwaggerResponse(statusCode: 200, type: typeof(Pokemon), description: "Expected response to a valid request")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "No such Pokemon")]
        public virtual IActionResult ShowTranslatedPokemonByName([FromRoute][Required]string pokemonName)
        {   
         
           try {
            var result = GetPokemon(pokemonName);
            result = GetTranslation(result);
            return new ObjectResult(result);
           }
           catch (PokemonNotFoundException e)
           {
             return StatusCode(404,new Error(){Code=404, Message="No such Pokemon"}); 
           }
        }
        
        /// <summary>
        /// Calls a helper to return a Pokemon Object from the name
        /// </summary>
        /// <param name="pokemonName"></param>
        private Pokemon GetPokemon(string pokemonName)
        {
            var helper = new PokedexClientHelper(_clientFactory);
            return helper.GetPokemon(pokemonName);
        }

        /// <summary>
        /// Calls a helper to return a translated Pokemon Object from an untranslated one
        /// returns same untranslated pokemon object if unable to translate it
        /// </summary>
        /// <param name="pokemon"></param>
        private Pokemon GetTranslation(Pokemon pokemon)
        {
            var translatehelper = new TranslateClientHelper(_clientFactory);
            var translationType = TranslationTypes.SHAKESPEARE;
            
            if(pokemon.Habitat == "cave" || pokemon.IsLegendary == true)
            {
                translationType = TranslationTypes.YODA;
            }

            return translatehelper.TranslatePokemon(pokemon, translationType);
        }
    }
}

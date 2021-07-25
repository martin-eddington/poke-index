using PokeIndex.Models;

namespace PokeIndex.Helpers
{
    /// <summary>
    /// Helper class to return all important information from external API responses
    /// </summary>
    public class ClientHelperResponse 
    {
        /// <summary>
        /// Success status
        /// </summary>
        public bool Succeeded {get; set;}

        /// <summary>
        /// HTTP status
        /// </summary>
        public int? Status {get; set;}

        /// <summary>
        /// String containing JSON returned if all went well
        /// </summary>
        public string APIResponse {get; set;} 
    }
}
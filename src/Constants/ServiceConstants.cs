namespace PokeIndex.Constants
{
    /// <summary>
    /// Class to hold API client constants
    /// </summary>
    public class APIs {
        /// <constant> HttpClient for Pokemon data lookup API </constant> ///
        public const string POKEDEX = "pokedex";

        /// <constant> HttpClient for Translation API </constant> ///
        public const string TRANSLATE = "translate";

    }

     /// <summary>
    /// Class to hold API base URL constants
    /// </summary>
    public class BASE_URLS {
        /// <constant> BaseURL for Pokemon data lookup API </constant> ///
        public const string POKEDEX = "https://pokeapi.co/api/v2/";

        /// <constant> BaseURL for Translation API </constant> ///
        public const string TRANSLATE = "https://api.funtranslations.com/translate/";

    }

    /// <summary>
    /// Class to hold strings to define Translation API constants.
    /// </summary>

    public class TranslationTypes {
        /// <constant> Yoda translation type </constant> ///
        public const string YODA = "yoda"; 
        /// <constant> Shakespeare translation type </constant> ///
        public const string SHAKESPEARE = "shakespeare";

    }

}
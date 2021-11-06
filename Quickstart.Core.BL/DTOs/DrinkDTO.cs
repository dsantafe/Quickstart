using Newtonsoft.Json;
using System.Collections.Generic;

namespace Quickstart.Core.BL.DTOs
{
    public class DrinkDTO
    {
        [JsonProperty("idDrink")]
        public string IdDrink { get; set; }

        [JsonProperty("strDrink")]
        public string StrDrink { get; set; }

        [JsonProperty("strInstructions")]
        public string StrInstructions { get; set; }

        [JsonProperty("strDrinkThumb")]
        public string StrDrinkThumb { get; set; }
    }

    public class DrinksDTO
    {
        [JsonProperty("drinks")]
        public List<DrinkDTO> Drinks { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Quickstart.Core.BL.Controls
{
    public class SelectControl
    {
        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("text")]
        public object Text { get; set; }

        public static string GetSelect(List<SelectControl> datos)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach (var item in datos)
                sb.Append("{" + string.Format("id:'{0}', text:'{1}'", item.Id, item.Text) + "},");
            sb.Append("]");

            return sb.ToString();
        }
    }
}

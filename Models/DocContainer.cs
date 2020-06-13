using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebForm.Models
{
    public class DocContainer
    {
        [Display(Name = "File Upload")]
        [JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty("ContentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }
        [JsonProperty("CreateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset CreateTime { get; set; }

        [Display(Name = "Document Description")]
        [JsonProperty("DocumentDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentDescription { get; set; }

        [JsonProperty("Hashes", NullValueHandling = NullValueHandling.Ignore)]
        public object Hashes { get; set; }
        [JsonProperty("ObjectKey", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectKey ObjectKey { get; set; }

        public HttpPostedFileBase File { get; set; }

    }
}

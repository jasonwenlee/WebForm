using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebForm.Models
{
    public class User
    {
        [JsonProperty("ChangeSet", NullValueHandling = NullValueHandling.Ignore)]
        public ChangeSet ChangeSet { get; set; }
    }
    public class ChangeSet
    {
        [JsonProperty("Changes", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Changes { get; set; }

        [JsonProperty("LastEdit", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastEdit { get; set; }

        [JsonProperty("Updated", NullValueHandling = NullValueHandling.Ignore)]
        public Updated Updated { get; set; }
    }

    public class Updated
    {
        [JsonProperty("EntryDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset EntryDate { get; set; }

        [Display(Name = "Organisation Name")]
        [JsonProperty("OrganisationUnitID", NullValueHandling = NullValueHandling.Ignore)]
        public int OrganisationUnitID { get; set; }
        public IEnumerable<SelectListItem> OrganisationName { get; set; }

        [Required]
        [Display(Name = "Request Detail")]
        [JsonProperty("RequestDetail", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestDetail { get; set; }

        [Required]
        [Display(Name = "Requestor Name")]
        [JsonProperty("RequestorName", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestorName { get; set; }
    }
}

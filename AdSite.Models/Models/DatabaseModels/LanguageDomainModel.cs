using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class Language : RepositoryEntity
    {
        [Required]
        public int CultureId { get; set; }
        public string LanguageName { get; set; }
        public string LanguageShortName { get; set; }
    }
}

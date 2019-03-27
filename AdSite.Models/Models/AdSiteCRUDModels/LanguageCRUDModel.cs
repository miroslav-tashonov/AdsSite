using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class LanguageSelectViewModel : ILanguageSelectListItem
    {
        public int CultureId { get; set; }
        public string LanguageName { get; set; }
        public string LanguageShortName { get; set; }

        public int Value { get { return CultureId; } }
        public string Text { get { return LanguageName; } }
    }

    public interface ILanguageSelectListItem
    {
        int Value { get; }
        string Text { get; }
    }

    public class LanguageViewModel
    {
        public Guid ID{ get; set; }
        public int CultureId { get; set; }
        public string LanguageName { get; set; }
        public string LanguageShortName { get; set; }
    }

    public class LanguageCreateModel
    {
        [Required]
        public string CultureId { get; set; }
        public Guid CountryId { get; set; }
    }

    public class LanguageEditModel
    {
        [Required]
        public Guid ID { get; }
        [Required]
        public int CultureId { get; set; }
        [Required]
        public string LanguageName { get; set; }
        [Required]
        public string LanguageShortName { get; set; }
    }
}

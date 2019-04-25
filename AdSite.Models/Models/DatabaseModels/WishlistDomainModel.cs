using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class Wishlist : RepositoryEntity
    {
        #region Foreign Keys
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        #endregion

    }
}

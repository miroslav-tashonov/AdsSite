using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models
{
    public class RepositoryEntity
    {
        [Key]
        public Guid ID { get; set; }
    }
}

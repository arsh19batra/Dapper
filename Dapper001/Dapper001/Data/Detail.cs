using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper001.Data
{
    public class Detail
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        
    }

}
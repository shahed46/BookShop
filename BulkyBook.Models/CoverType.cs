﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class CoverType
    {
        [key]
        public int Id { get; set; }

        [Display(Name="Cover type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}

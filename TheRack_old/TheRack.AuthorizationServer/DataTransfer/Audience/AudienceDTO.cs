using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheRack.AuthorizationServer.DataTransfer
{
    public class AudienceDTO
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
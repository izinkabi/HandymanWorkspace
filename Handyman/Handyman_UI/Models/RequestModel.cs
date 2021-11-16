using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class RequestModel
    {
        [Key]
        public int Id { get; set; }
        public int ConsumerId { get; set; }
        public int ProvidersServiceId { get; set; }
    }
}
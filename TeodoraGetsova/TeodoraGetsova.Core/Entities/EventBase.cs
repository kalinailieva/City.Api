using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeodoraGetsova.Core.Entities
{
    public class EventBase
    {
        [Required]
        public Guid Id { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? SubTitle { get; set; }

        public string? Summary { get; set; }

        public string? Text { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}

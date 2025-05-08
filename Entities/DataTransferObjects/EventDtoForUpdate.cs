using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record EventDtoForUpdate : EventDtoForManipulation
    {
        [Required]
        public int Id { get; set; }
        
        public bool IsApproved {  get; set; }=false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.Dto
{
    public class GroupStatusUpdateDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
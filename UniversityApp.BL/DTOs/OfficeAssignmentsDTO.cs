using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityApp.BL.DTOs
{
   public class OfficeAssignmentsDTO
    {
        public int InstructorID { get; set; }
        public string Location { get; set; }
        public InstructorDTO Instructor { get; set; }
    }
}

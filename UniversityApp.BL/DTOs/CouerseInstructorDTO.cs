using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityApp.BL.DTOs
{
   public class CourseInstructorDTO
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int InstructorID { get; set; }
        public CourseDTO course { get; set; }
        public InstructorDTO instructor { get; set;}
    }
}

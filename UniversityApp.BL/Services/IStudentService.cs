using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;

namespace UniversityApp.BL.Services
{
  public interface IStudentService : IGenericRest<StudentDTO>
    {
    }
}

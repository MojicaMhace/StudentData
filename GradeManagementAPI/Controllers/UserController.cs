using GradeManagemenrBL;
using GradeManagementDL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GradeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly AcademicService _academicService;
        private readonly SqlDbData _sqlDbData;

        public UserController()
        {
            _academicService = new AcademicService(0);
            _sqlDbData = new SqlDbData();
        }

        [HttpGet]
        public IEnumerable<Credential> GetUsers()
        {
            var students = SqlDbData.GetList();
            List<Credential> credentials = new List<Credential>();

            foreach (var student in students)
            {
                credentials.Add(new Credential { StudentName = student.StudentName, CourseSection = student.CourseSection, Average = student.Average });
            }
            return credentials;
        }

        [HttpPost]

        public JsonResult AddUser(Credential request)
        {
            SqlDbData.AddData(request.StudentName, request.CourseSection, request.Average);
            return new JsonResult("Success");
        }

        [HttpDelete]
        public JsonResult DeleteData(Credential request)
        {
            SqlDbData.DeleteData(request.StudentName, request.CourseSection, request.Average);
            return new JsonResult("Success");
        }

        [HttpPatch]
        public JsonResult UpdateUser(Credential request)
        {
            SqlDbData.UpdateData(request.StudentName, request.CourseSection, request.Average);
            return new JsonResult("Success");
        }


    }
}

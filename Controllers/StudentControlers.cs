using Microsoft.AspNetCore.Mvc;
using _13_09web_student.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace _13_09web_student.Controllers
{
    public class StudentControlers : Controller
    {
        private List<Student> lstStudent ;
        private readonly IHostingEnvironment env ;
        public StudentControlers(IHostingEnvironment _env ) //tao ds
        {
            env = _env;
            lstStudent = new List<Student>()
            {
                new Student()
                {
                    Id = 101,
                    Name = "Hải Nam",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.IT,
                    Gender = Gender.Male,
                    IsRegular = true,
                    Address = "Hà Nội",
                    Email = "hainam@gmail.com"
                },
                new Student()
                {
                    Id = 102,
                    Name = "Minh Tú",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.CE,
                    Gender = Gender.Female,
                    IsRegular = true,
                    Address = "Thanh Hóa",
                    Email = "mtu@gmail.com"
                },
                new Student()
                {
                    Id = 103,
                    Name = "Hoàng Long",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.EE,
                    Gender = Gender.Male,
                    IsRegular = false,
                    Address = "Hải Phòng",
                    Email = "hlong@gmail.com"
                },
                new Student() {
                    Id = 105,
                    Name = "Lâm Hùng",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.BE,
                    Gender = Gender.Male,
                    IsRegular = false,
                    Address = "Nghệ An",
                    Email = "hung@gmail.com"
                },
            };

        }
        [Route("Admin/Student/List")]
        public IActionResult Index()
        {
            return View(lstStudent);
        }
        //[Route("Admin/Student/Add")]
         [HttpGet("/Admin/Student/Add")]
        public IActionResult Create()
        {

            //lay ds cac gia tri Gender de hien thi radio button tren form
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            //lay ds cac gia tri tren Branch de hien thi select-option tren form
            //de hien thi select-option tren view can dung List<SelectListItem>
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1"},
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }
        [HttpPost("/Admin/Student/Add")]
        //public IActionResult Create(Student student)
        //{
        //    student.Id = lstStudent.Last<Student>().Id +1;
        //    lstStudent.Add(student);
        //    return View("Index",lstStudent);
        //}
        public async Task<IActionResult> Craete(Student student)
        {
            if(student.Img != null)
            {
                var file = Path.Combine(env.ContentRootPath, "wwwroot\\FileImg", student.Img.FileName);
                using(FileStream fileStream = new FileStream(file, FileMode.Create))
                {
                    await student.Img.CopyToAsync(fileStream);
                }
            }
            student.Id = lstStudent.Last<Student>().Id + 1;
            lstStudent.Add(student);
            return View("Index",lstStudent);
        }

    }
}

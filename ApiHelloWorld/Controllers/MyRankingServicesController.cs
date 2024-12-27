using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiHelloWorld.Controllers
{
    //api/MyRankingServices
    [Route("api/[controller]")]
    [ApiController]
    public class MyRankingServicesController : ControllerBase
    {

        //객체 형태로 전달 - 데이터 - Json 형태로 반환        
        /*[HttpGet]
        public Subject Get()
        {
            return new Subject { Kor = 95, Eng = 100, Total = 195 };
        }*/

        //컬렉션 이니셜라이져 전달, Json 형태로 반환
        /*[HttpGet]
        public List<Student> Get()
        {
            var students = new List<Student> {
                new Student { Id = 1, Name = "홍길동", Score = 3 },
                new Student { Id = 2, Name = "백두산", Score = 2 },
                new Student { Id = 3, Name = "임꺽정", Score = 1 }
            };

            return students;
        }*/

        //복합 형식 전달
        [HttpGet]
        public MyRankingDto Get()
        {
            var subject = new Subject { Kor = 95, Eng = 100, Total = 195 };
            var students = new List<Student> {
                new Student { Id = 1, Name = "홍길동", Score = 3 },
                new Student { Id = 2, Name = "백두산", Score = 2 },
                new Student { Id = 3, Name = "임꺽정", Score = 1 }
            };

            return new MyRankingDto { Subject = subject, Students = students };
        }
    }

    /// <summary>
    /// 과목 - 모델 클래스
    /// </summary>
    public class Subject
    {
        public int Kor { get; set; }
        public int Eng { get; set; }
        public int Total { get; set; }
    }

    /// <summary>
    /// 학생
    /// </summary>
    public class  Student
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public int Score { get; set; }
    }

    /// <summary>
    /// 성적 정보: 복합 형식( 과목(객체) + 학생들(컬렉션) )
    /// </summary>
    public class MyRankingDto
    {
        public Subject Subject { get; set; }
        public List<Student> Students { get; set; }
    }


    //뷰 페이지
    public class MyRankingServiecsTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

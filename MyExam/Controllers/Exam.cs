using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace MyExam.Controllers
{
    public class Exam
    {
    }

    /// <summary>
    /// 리포지토리 인터페이스 - 기능정의
    /// </summary>
    public interface IQuestionRepository
    {
        Question Add(Question question);
        List<Question> GetAll();
        Question GetById(int id);
        Question Update(Question question);
        void Delete(int id);    
    }
    /// <summary>
    /// 리포지토리 클래스 - 기능 구현
    /// </summary>
    public class QuestionRepository : IQuestionRepository
    {
        private IConfiguration _config;
        private IDbConnection db;
        //생성자
        public QuestionRepository(IConfiguration config) 
        {
            _config = config;
            db = new SqlConnection(
                _config.GetSection("ConnectionStrings")                         //DB 연결 appsetting에서 땡겨오기
                    .GetSection("DefaultConnection").Value
                );
        }

        public Question Add(Question question)
        {
            string sql = @"
                 insert into Question (Title) values (@Title);
                select cast(SCOPE_IDENTITY() as int);
                ";
            var id = db.Query<int>(sql, question).Single();
            question.Id = id;
            return question;
        }

        public List<Question> GetAll()
        {
            string sql = @"
                        select * from Question order by desc;
                        ";
            return db.Query<Question>(sql).ToList();
        }

        public Question GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Question Update(Question question)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 웹 API 컨트롤러 클래스
    /// </summary>
    [Route("api/[controller]")]
    public class QuestionServiceController : ControllerBase
    {
        private IQuestionRepository _repository;

        //생성자
        public QuestionServiceController(IQuestionRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        /*public IActionResult Get()
        {

        }*/

        [HttpPost]
        public IActionResult Post([FromBody]Question model)
        {
            //예외처리
            if(model == null)
            {
                return BadRequest();
            }
            try
            {
                //예외처리
                if(model.Title == null || model.Title.Length <1)
                {
                    ModelState.AddModelError("Title", "문제를 입력해야 합니다");
                }
                //모델 유효성 검사
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //매개변수로 들어온 데이터를 리포지토리에 넘겨준다
                var newModel = new Question() { Id = model.Id, Title = model.Title };
                var m = _repository.Add(newModel);
                return Ok(m);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }

    /// <summary>
    /// 모델 클래스 - DB 테이블과 일대일 매핑
    /// </summary>
    public class Question
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(4000, ErrorMessage ="문제는 4000자 이하로 입력하세요")]
        public string Title { get; set; }
    }
}

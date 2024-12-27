using Microsoft.AspNetCore.Mvc;

namespace ApiHelloWorld.Components
{
    public class PointComponent
    {
        //Empty
    }

    /// <summary>
    /// 리포지토리 인터페이스 - Point와 관련된 기능 정의
    /// </summary>
    public interface IPointRepository
    {
        void Add(Point point);              //테이블에 데이터 Insert
        Point Get(int id);                  //테이블에 데이터 Select
        int GetTotalPointGetByUserId(int userId = 1234);
        PointStauts GetPointStautsByUser();
    }

    /// <summary>
    /// 리포지토리 클래스 - Point와 관련된 기능 구현
    /// </summary>
    public class PointRepository : IPointRepository
    {
        public int GetTotalPointGetByUserId(int userId = 1234)
        {
            //TODO : 실제 데이터베이스와 연동하는 코드

            return 1234;
        }

        public PointStauts GetPointStautsByUser()
        {
            return new PointStauts() { Gold = 0, Silver = 0, Bronze = 0 };
        }

        public void Add(Point point)
        {
            throw new NotImplementedException();    //데이터 Insert
        }

        public Point Get(int id)
        {
            throw new NotImplementedException();    //데이터 Select
        }
    }

    /// <summary>
    /// 리포지토리 인 메모리 클래스 - Point와 관련된 기능 구현
    /// </summary>
    public class PointRepositoryInMemory : IPointRepository
    {
        public int GetTotalPointGetByUserId(int userId = 1234)
        {
            return 9871;
        }

        public PointStauts GetPointStautsByUser()
        {
            return new PointStauts() { Gold = 10, Silver = 123, Bronze = 456 };
        }

        public void Add(Point point)
        {
            throw new NotImplementedException();    //데이터 Insert
        }

        public Point Get(int id)
        {
            throw new NotImplementedException();    //데이터 Select
        }
    }

    public interface IPointLogRepository
    {

    }

    public class PointLogRepository : IPointLogRepository
    {

    }

    /// <summary>
    /// Point 뷰 페이지
    /// </summary>
    public class PointController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    /// <summary>
    /// Point Web API 서비스
    /// </summary>
    [Route("api/[controller]")]
    public class PointServiceController : ControllerBase
    {
        private IPointRepository _repository;

        //생성자
        public PointServiceController(IPointRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var json = new { Point = 4567 };
            return Ok(json);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult Get(int userId)
        {
            //userId 를 입력받아 데이터 베이스에 있는 포인트를 반환시켜준다
            var myPoint = _repository.GetTotalPointGetByUserId(userId);
            var json = new { Point = myPoint };
            return Ok(json);
        }

    }

    /// <summary>
    /// Point Log 뷰 페이지
    /// </summary>
    public class PointLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    /// <summary>
    /// Point Log Web API 서비스
    /// </summary>
    public class PointLogServiceController : ControllerBase
    {

    }

    /// <summary>
    /// PointStatus 뷰 페이지
    /// </summary>
    public class PointStatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    /// <summary>
    /// PointStatus 에 대한 Web Api 서비스
    /// </summary>
    [Route("api/[controller]")]
    public class PointStatusServiceController : ControllerBase
    {
        private IPointRepository _repository;

        //생성자
        public PointStatusServiceController(IPointRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pointStatus = _repository.GetPointStautsByUser();
            return Ok(pointStatus);
        }
    }


    /// <summary>
    /// Point 모델 클래스 : Points 테이블과 일대일 매핑
    /// </summary>
    public class Point
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalPoint { get; set; }
    }

    /// <summary>
    /// PointLog 모델 클래스 : PointLogs 테이블과 일대일 매핑
    /// </summary>
    public class PointLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalPoint { get; set; }
        public DateTimeOffset Created { get; set; }
    }

    /// <summary>
    /// 포인트 상태 정보를 금 은 동 하는 모델 클래스
    /// </summary>
    public class PointStauts
    {
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
    }
}

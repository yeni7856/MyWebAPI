using Microsoft.AspNetCore.Mvc;

namespace ApiHelloWorld.Componts
{
    public class PointComponent
    {
    }

    /// <summary>
    /// 리포지토리 인터페이스 - Point와 관련된 기능 저의
    /// </summary>
    public interface IPointRepository
    {
        int GetTotalPointGetByUserId(int userId = 1234);
        PointStauts GetPointStautsByUser();
    }
    public interface IPointLogRepository
    {
        int GetTotalPointGetByUserId(int userId = 1234);
    }
    /// <summary>
    /// 리포지토리 인터페이스 - Point와 관련된 기능 구현 
    /// </summary>
    public class PointRepository : IPointRepository
    {
        public int GetTotalPointGetByUserId(int userId = 1234)
        {
            return 1234;
        }
        public PointStauts GetPointStautsByUser()
        {
            return new PointStauts() { Gold = 0, Silver = 0, Bronze = 0 };
        }
    }
    /// <summary>
    /// 리포지토리 인 메모리 클래스 - Point와 관련된 기능 구현
    /// </summary>
    public class PointLogRepository : IPointLogRepository
    {
        public int GetTotalPointGetByUserId(int userId = 1234)
        {
            return 9871;
        }
        public PointStauts GetPointStautsByUser()
        {
            return new PointStauts() { Gold = 0, Silver = 0, Bronze = 0 };
        }
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
        public IActionResult Get(int userid)
        {
            //userId를 입력받아 데이터 베이스에 있는 포인트를 반환시켜준다
            var myPoint = _repository.GetTotalPointGetByUserId(userid);
            var json = new {Point = myPoint};
            return Ok(json);
        }
    }
    /// <summary>
    /// PointLog Web API 서비스
    /// </summary>
    public class PointLogController : ControllerBase
    {

    }

    public class PointStautsController : ControllerBase
    {

    }
    /// <summary>
    /// PointStatus에 대한 Web Api 서비스
    /// </summary>
    public class PointStautsServiceController : ControllerBase
    {
        private IPointRepository _repository;

        //생성자
        public PointStautsServiceController(IPointRepository repository)
        {

        }
    }


    /// <summary>
    /// Point 모델 클래스 : Point 테이블과 일대일 매핑
    /// </summary>
    public class Point
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }

    }

    /// <summary>
    /// PointLog 모델 클래스 : PointLog 테이블과 일대일 매핑
    /// </summary>
    public class PointLog
    {

    }

    /// <summary>
    /// 포인트 상태 정보를 금 은 동으로 
    /// </summary>
    public class PointStauts
    {
        public int Gold { get; internal set; }
        public int Silver { get; internal set; }
        public int Bronze { get; internal set; }
    }
}

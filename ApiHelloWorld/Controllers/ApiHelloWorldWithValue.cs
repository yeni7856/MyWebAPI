using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiHelloWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiHelloWorldWithValueController : ControllerBase
    {
        // GET: api/<ApiHelloWorldWithValue>
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            //return new string[] { "안녕하세요", "홍길동입니다" };
            return new Value[] {
                new Value { Id = 1, Text = "안녕하세요" },
                new Value { Id = 2, Text = "홍길동입니다" }
            };
        }

        // GET api/<ApiHelloWorldWithValue>/5
        [HttpGet("{id:int}")]
        public Value Get(int id)
        {
            //return $"넘어온 값: {id}";
            return new Value { Id = id, Text = $"넘어온 값: {id}" };
        }

        // POST api/<ApiHelloWorldWithValue>
        [HttpPost]
        public IActionResult Post([FromBody] Value value)
        {
            //모델 유효성 검사
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  //400 에러 출력
            }

            //넘어온 json 데이터를 처리후 id 반환
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }

        // PUT api/<ApiHelloWorldWithValue>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiHelloWorldWithValue>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    // 새로운 클래스 Value를 만들어서 Value타입으로 데이터를 전송 (xml, json 형태)
    // 모델 클래스, 모델 유효성 검사
    public class Value
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Text속성은 필수 입력 값입니다")]
        public string Text { get; set; }
    }

    //View 페이지
    public class ApiHelloWorldDemoController : Controller
    {
        public IActionResult Index()
        {
            string html = @"
<html>
<head>
    <title>jQuery로 JSON사용하기 </title>
</head>
<body>
    <h1>Web API호출</h1>
    <div id='print'></div>
    <script src = 'https://code.jquery.com/jquery-1.12.4.min.js'></script>
    <script>
        var API_URI = '/api/ApiHelloWorldWithValue';
        $(function() {
            $.getJSON(API_URI, function(data) {
                var str = '<dl>';
                $.each(data, function(index, entry) {
                    str += '<dt>' + entry.id + '</dt>';
                    str += '<dd>' + entry.text + '</dd>';
                });
                str += '</dl>';                
                $('#print').html(str);
            });
        });
    </script>
</body>
</html>
";
            return new ContentResult()
            {
                Content = html,
                ContentType = "text/html; charset=utf-8"
            }; 
        }
    }

}

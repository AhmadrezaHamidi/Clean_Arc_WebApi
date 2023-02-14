using Microsoft.AspNetCore.Mvc;

namespace AhFramWork.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ITestService _testService;
        private readonly IUnitOfWork _unitOfWork;
        public TestController(IMediator mediator, IMapper mapper, ITestService testService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllTestsQuery();
            return Ok(await _mediator.Send(query));

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTestCommandDto request)
        {
            var command = _mapper.Map<CreateTestCommand>(request);
            return Ok(await _mediator.Send(command));
        }

    }
}

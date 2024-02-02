using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace KafkaTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
       KafkaService _kafkaService;

        private readonly ILogger<KafkaController> _logger;

        public KafkaController(ILogger<KafkaController> logger, KafkaService kafkaService)
        {
            _logger = logger;
            _kafkaService = kafkaService;
        }

        [HttpGet(Name = "SendDataToKafka")]
        public async  Task<IActionResult> Get()
        {
            var data = new KafkaMessage() { Text = "aref" ,MyProperty=DateTime.Now};
            try
            {
                await _kafkaService.SendMessage(data);
            }
            catch (Exception ex)
            {

                throw;
            }


            return Ok(data);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SmartHome_BackEnd.Data;
using SmartHome_BackEnd.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHome_BackEnd.Controllers
{
    [Route("api/sensor")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            this._context = context;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> PostSensorData([FromBody] Sensor sensorData)
        {
            Console.WriteLine($"Temperature: {sensorData.Temperature}, Humidity: {sensorData.Humidity}");
            if (sensorData == null)
            {
                return BadRequest("Sensor data is null.");
            }
            
            try
            {
                _context.Sensors.Add(sensorData); // Assuming Sensors is the DbSet<SensorData>
                await _context.SaveChangesAsync();

                return Ok(new { message = "Sensor data saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/<ValuesController>/sensor
        [HttpGet]
        public IActionResult GetSensorData()
        {
            var sensorDataList = _context.Sensors.ToList();
            return Ok(sensorDataList);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

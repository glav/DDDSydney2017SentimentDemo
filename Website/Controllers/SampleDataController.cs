using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Website.Data;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Whatevs", "yeah", "woot", "whatevs", "yo", "hoot", "snot", "crap", "yah", "blart"
        };

        [HttpGet("[action]")]
        public IEnumerable<EmailInformation> EmailSentiment()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new EmailInformation
            {
                id = Guid.NewGuid().ToString(),
                Body = "Body",
                SentimentScore = rng.Next(-20, 55),
                From = "test@test.com",
                TimeOfMail = DateTime.UtcNow
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}

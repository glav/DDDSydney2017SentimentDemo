using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Website.Data;
using Website.Domain;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private ViewModelEngine _viewEngine = new ViewModelEngine();

        [HttpGet("[action]")]
        public async Task<IEnumerable<EmailInformation>> EmailSentiment()
        {
            var emails = await _viewEngine.GetSentimentViewModelsAsync();
            return emails;

        }

    }
}

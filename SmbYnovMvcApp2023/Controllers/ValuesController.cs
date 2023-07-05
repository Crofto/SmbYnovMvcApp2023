using SmbYnovMvcApp2023.Data;
using SmbYnovMvcApp2023.Models;
using Microsoft.AspNetCore.Mvc;

namespace SmbYnovMvcApp2023.Controllers
{
    public class ValuesController : Controller
    {

        private AppYnovContext _dbContext;

        public ValuesController(AppYnovContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static Config conf = new Config
        {
            tempFreq = 5,
            connectionConfig = 2,
            connectionFreq = 30
        };

        [HttpGet]
        // GET api/values
        public IActionResult Get()
        {
            return Json(conf);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPut]
        // POST api/values
        public void Post([Microsoft.AspNetCore.Mvc.FromBody] string value)
        {
            var aaaaaa = "putain";
        }

        [HttpPost]
        // PUT api/values/5
        public void Put([Microsoft.AspNetCore.Mvc.FromBody] Payload pl)
        {
            conf = pl.config;
            var temperature = pl.temperatures;

            var device = _dbContext.Devices.FirstOrDefault(d => d.FonctionalId == "esp32");

            var uplinkmesage = new UplinkMessage
            {
                DeviceId = device.Id.ToString(),
                Device = device,
                RecieveIdAt = DateTimeOffset.Now,
                ConnectionFrequency = conf.connectionFreq,
                TypeFrequency = conf.connectionConfig,
            };
            _dbContext.UplinkMessages.Add(uplinkmesage);
            _dbContext.SaveChanges();

            _dbContext.TimeSeries.Add(new TimeSeries()
            {
                DateTimeOffset = DateTimeOffset.Now,
                Value = (float)temperature.Average(),
                DeviceId = device.Id,
                UplinkMessage = uplinkmesage,
                UplinkMessageId = uplinkmesage.Id                
            }) ;
            _dbContext.SaveChanges();

        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}

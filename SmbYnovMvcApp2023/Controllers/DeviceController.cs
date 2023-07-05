﻿using Microsoft.AspNetCore.Mvc;
using SmbYnovMvcApp2023.Data;
using SmbYnovMvcApp2023.Models;

namespace SmbYnovMvcApp2023.Controllers
{
    public class DeviceController : Controller
    {

        private const string K_TIME_SERIES_SOIL_TEMPERATURE = "SOIL_TEMPERATURE";
        private const string K_TIME_SERIES_SOIL_CONDUCTIVITY = "SOIL_CONDUCTIVITY";
        private const string K_TIME_SERIES_SOIL_HUMDITY = "SOIL_HUMIDITY";

        private AppYnovContext _dbContext;

        public DeviceController(AppYnovContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return getLIst();
        }

        public IActionResult getLIst()
        {
            var devices = _dbContext.Devices.ToList();
            var listWater = _dbContext.TimeSeries.Where(ts=> ts.TimeSeriesName == K_TIME_SERIES_SOIL_HUMDITY).ToList();
            var listCond = _dbContext.TimeSeries.Where(ts => ts.TimeSeriesName == K_TIME_SERIES_SOIL_CONDUCTIVITY).ToList();
            var listTempSoil = _dbContext.TimeSeries.Where(ts => ts.TimeSeriesName == K_TIME_SERIES_SOIL_TEMPERATURE).ToList();

            var dateWater = listWater.Select(s=> s.DateTimeOffset).ToList();
            var valueWater = listWater.Select(s => s.Value).ToList();

            var dateCond = listCond.Select(s=> s.DateTimeOffset).ToList();
            var valueCond = listCond.Select(s => s.Value).ToList();

            var dateTempSoil = listTempSoil.Select(s=> s.DateTimeOffset).ToList();
            var valueTempSoil = listTempSoil.Select(s => s.Value).ToList();

            var dataToDisplay = new DataToDisplay
            {
                dateWater=dateWater,
                valueWater=valueWater,

                dateCond=dateCond,
                valueCond=valueCond,

                dateTempSoil=dateTempSoil,
                valueTempSoil=valueTempSoil
            };
            return View(dataToDisplay);
        }
    }
}

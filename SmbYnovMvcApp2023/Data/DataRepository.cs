using System.Collections.Generic;
using System;
using System.Linq;
using SmbYnovMvcApp2023.Models;
using SmbYnovMvcApp2023.Data;
using Newtonsoft.Json;

namespace Ynov2023AppDemo.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly AppYnovContext _dbContext;
        public DataRepository(AppYnovContext dbcontext)
        {
            _dbContext = dbcontext;

        }

        //Device
        public IEnumerable<Device> AllDevices
        {
            get
            {
                return _dbContext.Devices;
            }
        }

        public Device? GetDeviceById(ulong id)
        {

            return _dbContext.Devices.FirstOrDefault(d => d.Id == id);
        }

        public Device? GetDeviceByFonctionalId(string fonctionalId)
        {
            return _dbContext.Devices.FirstOrDefault(d => d.FonctionalId == fonctionalId);
        }

        public Device? GetDeviceByName(string name)
        {
            return _dbContext.Devices.FirstOrDefault(d => d.Name == name);
        }

        public DateTimeOffset? MaxTimeSeriesDateForDevice(ulong deviceId)
        {
            return _dbContext.TimeSeries.Where(ts => ts.DeviceId == deviceId)?.Max(ts => ts.DateTimeOffset);
        }

        public DateTimeOffset? MinTimeSeriesDateForDevice(ulong deviceId)
        {
            return _dbContext.TimeSeries.Where(ts => ts.DeviceId == deviceId)?.Min(ts => ts.DateTimeOffset);
        }

        public void MinAndMaxTsDatesForDevice(ulong deviceId, out DateTimeOffset? minDate, out DateTimeOffset? maxDate)
        {
            minDate = null;
            maxDate = null;

            var query = _dbContext.TimeSeries.Where(ts => ts.DeviceId == deviceId);

            if (query.Count() > 0)
            {
                minDate = query.Min(ts => ts.DateTimeOffset);
                maxDate = query.Max(ts => ts.DateTimeOffset);
            }
        }



        public IEnumerable<TimeSeries> GetTimeSeriesForDeviceAtDate(ulong idDevice,
            DateTimeOffset? targetDate)
        {

            if (targetDate == null || targetDate.Value == DateTimeOffset.MinValue)
            {
                return new List<TimeSeries>(0);
            }
            DateTimeOffset dateMin = targetDate.Value.Date ;
            DateTimeOffset dateMax = dateMin.AddDays(1);


            return _dbContext.TimeSeries.
                Where(ts => ts.DeviceId == idDevice &&
                (ts.DateTimeOffset >= dateMin && ts.DateTimeOffset < dateMax));

        }

        public DateTimeOffset GetLastDateforService(string urlService)
        {
            if (string.IsNullOrEmpty(urlService)) { return DateTimeOffset.MinValue; }

            var service = _dbContext.ServerSettings.FirstOrDefault(s => s.Url == urlService);

            if (service == null || service.LastConnected == null) return DateTimeOffset.MinValue;

            return service.LastConnected.Value;
        }

        public ServerSettings? GetServerSettingsByUrl(string urlService)
        {
            return _dbContext.ServerSettings.FirstOrDefault(s => s.Url == urlService);
        }

        public void CreateOrUpdateServerSettings(string urlService, DateTimeOffset lastSuccesfulConnection)
        {
            if (string.IsNullOrEmpty(urlService)) { return; }

            ServerSettings? serverSettings = GetServerSettingsByUrl(urlService);
            if (serverSettings == null)
            {
                serverSettings = new ServerSettings();
                serverSettings.Url = urlService;
                _dbContext.ServerSettings.Add(serverSettings);
            }
            // Do not move back!
            if (serverSettings.LastConnected == null || lastSuccesfulConnection > serverSettings.LastConnected)
            {
                serverSettings.LastConnected = lastSuccesfulConnection;
            }

            _dbContext.SaveChanges();

        }

        //Uplink Message and time series

        public int CreateUplinkAndTimeSeriesForDevice(Device device, UplinkMessage uplinkMessage, DateTimeOffset receivedAt,
           params TimeSeries?[] timeSeries)
        {

            uplinkMessage.DeviceId = device.Id.ToString();
            uplinkMessage.Device = device;

            //Create device in database
            _dbContext.UplinkMessages.Add(uplinkMessage);

            if (timeSeries != null)
            {
                foreach (TimeSeries timeSerie in timeSeries)
                {
                    timeSerie.DeviceId = device.Id;
                    timeSerie.UplinkMessage = uplinkMessage;

                    _dbContext.TimeSeries.Add(timeSerie);
                }
            }
            return _dbContext.SaveChanges();
        }

        public void CreateUplinkAndTimeSeriesForTemp(string payload)
        {

            Payload payload1 = JsonConvert.DeserializeObject<Payload>(payload);

            var conf = payload1.config;
            var temperature = payload1.temperatures;

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
            });
            _dbContext.SaveChanges();
        }

    }
}

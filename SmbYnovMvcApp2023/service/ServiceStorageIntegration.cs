using Models;
using SmbYnovMvcApp2023.Models;
using System.Globalization;
using Ynov2023AppDemo.Data;

namespace SmbYnovMvcApp2023.service
{
    public class ServiceStorageIntegration
    {
        private IDataRepository _dataRepository;

        private const string K_TIME_SERIES_SOIL_TEMPERATURE = "SOIL_TEMPERATURE";
        private const string K_TIME_SERIES_SOIL_CONDUCTIVITY = "SOIL_CONDUCTIVITY";
        private const string K_TIME_SERIES_SOIL_HUMDITY = "SOIL_HUMIDITY";

        public ServiceStorageIntegration(IDataRepository dataRepository)
        {
             _dataRepository = dataRepository;
        }

        public int ManageUpLinkMessage(TTMlorawanResult message, string urlServer)
        {
            string devEUI = message.Result.EndDeviceIds.DevEui;
            Device? device = _dataRepository.GetDeviceByFonctionalId(devEUI);
            if (device == null) return 0;

            Models.UplinkMessage uplinkmessage = new Models.UplinkMessage();
            uplinkmessage.DeviceId = device.Id.ToString();
            uplinkmessage.Device = device;

            var linkMsg = message.Result.UplinkMessage; 
            uplinkmessage.RawPayLoad = linkMsg.FrmPayload;
            DateTimeOffset receivedAt = linkMsg.ReceivedAt;
            uplinkmessage.RecieveIdAt = receivedAt;


            CultureInfo culture = new CultureInfo("en-US");
            float.TryParse(message.Result.UplinkMessage.DecodedPayload.TempSoil.Trim(), NumberStyles.Float, culture, out var soil);
            float.TryParse(message.Result.UplinkMessage.DecodedPayload.WaterSoil.Trim(), NumberStyles.Float, culture, out var water);


            var series = new TimeSeries[] {
                new TimeSeries
                {
                     Value = soil,
                     TimeSeriesName = K_TIME_SERIES_SOIL_TEMPERATURE,
                     DateTimeOffset = receivedAt
                } ,
                new TimeSeries
                {
                     Value = message.Result.UplinkMessage.DecodedPayload.ConductSoil,
                     TimeSeriesName = K_TIME_SERIES_SOIL_CONDUCTIVITY,
                     DateTimeOffset = receivedAt
                } ,
                new TimeSeries
                {
                     Value = water,
                     TimeSeriesName = K_TIME_SERIES_SOIL_HUMDITY,
                     DateTimeOffset = receivedAt
                }
            };

            _dataRepository.CreateOrUpdateServerSettings(urlServer, linkMsg.ReceivedAt);
            int nbChanges = _dataRepository.CreateUplinkAndTimeSeriesForDevice(device, uplinkmessage, receivedAt, series);


            return nbChanges;
        }

        public DateTimeOffset GetLastAccessed(string url)
        {
            return _dataRepository.GetLastDateforService(url);
        }

        public void InsertTemp(string Payload)
        {
            _dataRepository.CreateUplinkAndTimeSeriesForTemp(Payload);
        }

    }
}

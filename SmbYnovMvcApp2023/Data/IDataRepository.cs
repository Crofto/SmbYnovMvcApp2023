namespace Ynov2023AppDemo.Data
{
   
    using System.Collections.Generic;
    using System;
    using SmbYnovMvcApp2023.Models;
    public interface IDataRepository
    {
        //Device
        IEnumerable<Device> AllDevices { get; }

        public Device? GetDeviceById(ulong id);

        public Device? GetDeviceByFonctionalId(string fonctionalId);


        public Device? GetDeviceByName(string name);

        //Time Series

        public DateTimeOffset? MaxTimeSeriesDateForDevice(ulong deviceId);
        public DateTimeOffset? MinTimeSeriesDateForDevice(ulong deviceId);

        public void MinAndMaxTsDatesForDevice(ulong deviceId, out DateTimeOffset? minDate, out DateTimeOffset? maxDate);


        public IEnumerable<TimeSeries> GetTimeSeriesForDeviceAtDate(ulong idDevice,
            DateTimeOffset? dateTime);


        //Service
        public DateTimeOffset GetLastDateforService(string urlService);

        public ServerSettings? GetServerSettingsByUrl(string urlService);

        public void CreateOrUpdateServerSettings(string urlService, DateTimeOffset lastSuccesfulConnection);

        //Uplink messages and TimeSeries
        public int CreateUplinkAndTimeSeriesForDevice(Device device, UplinkMessage uplinkMessage,
            DateTimeOffset receivedAt, params TimeSeries?[] timeSeries);

        public void CreateUplinkAndTimeSeriesForTemp(string payload);
    }
}


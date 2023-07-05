namespace Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TTMlorawanResult
    {
        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("end_device_ids")]
        public EndDeviceIds EndDeviceIds { get; set; }

        [JsonProperty("received_at")]
        public DateTimeOffset ReceivedAt { get; set; }

        [JsonProperty("uplink_message")]
        public UplinkMessage UplinkMessage { get; set; }
    }

    public partial class EndDeviceIds
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("application_ids")]
        public ApplicationIds ApplicationIds { get; set; }

        [JsonProperty("dev_eui")]
        public string DevEui { get; set; }

        [JsonProperty("dev_addr")]
        public string DevAddr { get; set; }
    }

    public partial class ApplicationIds
    {
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
    }

    public partial class UplinkMessage
    {
        [JsonProperty("f_port")]
        public long FPort { get; set; }

        [JsonProperty("f_cnt")]
        public long FCnt { get; set; }

        [JsonProperty("frm_payload")]
        public string FrmPayload { get; set; }

        [JsonProperty("decoded_payload")]
        public DecodedPayload DecodedPayload { get; set; }

        [JsonProperty("rx_metadata")]
        public RxMetadatum[] RxMetadata { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        [JsonProperty("received_at")]
        public DateTimeOffset ReceivedAt { get; set; }

        [JsonProperty("consumed_airtime")]
        public string ConsumedAirtime { get; set; }

        [JsonProperty("network_ids")]
        public NetworkIds NetworkIds { get; set; }
    }

    public partial class DecodedPayload
    {
        [JsonProperty("Bat")]
        public string Bat { get; set; }

        [JsonProperty("TempC_DS18B20")]
        public string TempCDs18B20 { get; set; }

        [JsonProperty("conduct_SOIL")]
        public long ConductSoil { get; set; }

        [JsonProperty("temp_SOIL")]
        public string TempSoil { get; set; }

        [JsonProperty("water_SOIL")]
        public string WaterSoil { get; set; }
    }

    public partial class NetworkIds
    {
        [JsonProperty("net_id")]
        public string NetId { get; set; }

        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("cluster_id")]
        public string ClusterId { get; set; }

        [JsonProperty("cluster_address")]
        public string ClusterAddress { get; set; }
    }

    public partial class RxMetadatum
    {
        [JsonProperty("gateway_ids")]
        public GatewayIds GatewayIds { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("rssi")]
        public long Rssi { get; set; }

        [JsonProperty("channel_rssi")]
        public long ChannelRssi { get; set; }

        [JsonProperty("snr")]
        public double Snr { get; set; }

        [JsonProperty("frequency_offset")]
        public long FrequencyOffset { get; set; }

        [JsonProperty("channel_index")]
        public long ChannelIndex { get; set; }

        [JsonProperty("received_at")]
        public DateTimeOffset ReceivedAt { get; set; }
    }

    public partial class GatewayIds
    {
        [JsonProperty("gateway_id")]
        public string GatewayId { get; set; }

        [JsonProperty("eui")]
        public string Eui { get; set; }
    }

    public partial class Settings
    {
        [JsonProperty("data_rate")]
        public DataRate DataRate { get; set; }

        [JsonProperty("frequency")]
        public long Frequency { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }
    }

    public partial class DataRate
    {
        [JsonProperty("lora")]
        public Lora Lora { get; set; }
    }

    public partial class Lora
    {
        [JsonProperty("bandwidth")]
        public long Bandwidth { get; set; }

        [JsonProperty("spreading_factor")]
        public long SpreadingFactor { get; set; }

        [JsonProperty("coding_rate")]
        public string CodingRate { get; set; }
    }
}

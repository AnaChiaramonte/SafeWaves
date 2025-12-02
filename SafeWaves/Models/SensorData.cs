using System;

namespace MQttIoT.Models
{
    public class SensorData
    {

        public DateTime Timestamp { get; set; }
        public string Topic { get; set; } = "";
        public string Payload { get; set; } = "";
    }
}
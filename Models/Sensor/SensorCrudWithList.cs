namespace GateEntryExit_MVC.Models.Sensor
{
    public class SensorCrudWithList
    {
        public GetAllSensorsDto Sensors { get; set; }

        public SensorDetailsDto SensorDetails { get; set; }

        public int PageNumber { get; set; }
    }
}

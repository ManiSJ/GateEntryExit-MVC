namespace GateEntryExit_MVC.Models.Sensor
{
    public class SensorCrudWithList
    {
        public GetAllSensorsDto AllSensors { get; set; }

        public SensorDetailsDto SensorDetails { get; set; }

        public int PageNumber { get; set; }
    }
}

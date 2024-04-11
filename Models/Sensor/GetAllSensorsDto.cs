namespace GateEntryExit_MVC.Models.Sensor
{
    public class GetAllSensorsDto
    {
        public int TotalCount { get; set; }

        public List<SensorDetailsDto> Items { get; set; }
    }
}

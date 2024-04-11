namespace GateEntryExit_MVC.Models.Sensor
{
    public class GetAllSensorWithDetailsOutputDto
    {
        public int TotalCount { get; set; }

        public List<SensorDetailsDto> Items { get; set; }
    }
}

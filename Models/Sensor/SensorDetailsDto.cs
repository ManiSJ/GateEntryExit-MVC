using GateEntryExit_MVC.Models.Gate;

namespace GateEntryExit_MVC.Models.Sensor
{
    public class SensorDetailsDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public GateDetailsDto GateDetails { get; set; }
    }
}

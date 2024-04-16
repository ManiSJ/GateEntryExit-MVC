using GateEntryExit_MVC.Models.Shared;

namespace GateEntryExit_MVC.Models.Sensor
{
    public class GetAllSensorWithDetailsInputDto : GetAllDto
    {
        public Guid[] GateIds { get; set; }

        public string GateIdsString { get; set; }

        public DateTime? FromDate { get; set; } 

        public DateTime? ToDate { get; set; }
    }
}

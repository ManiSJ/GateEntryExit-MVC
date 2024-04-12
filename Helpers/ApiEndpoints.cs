using static System.Net.WebRequestMethods;

namespace GateEntryExit_MVC.Helpers
{
    public static class ApiEndpoints
    {
        public static string baseUrl = "http://localhost:5058";

        // Gate
        public static string gateGetAll = "/api/gate/getAll";
        public static string gateGetAllById = "/api/gate/getAllById";
        public static string gateGetById = "/api/gate/getById/{id}";
        public static string gateCreate = "/api/gate/create";
        public static string gateEdit = "/api/gate/edit";
        public static string gateDelete = "/api/gate/delete/{id}";

        // Gate entry
        public static string gateEntryGetAll = "/api/gateEntry/getAll";
        public static string gateEntryCreate = "/api/gateEntry/create";
        public static string gateEntryEdit = "/api/gateEntry/edit";
        public static string gateEntryDelete = "/api/gateEntry/delete/{id}";

        // Gate exit
        public static string gateExitGetAll = "/api/gateExit/getAll";
        public static string gateExitCreate = "/api/gateExit/create";
        public static string gateExitEdit = "/api/gateExit/edit";
        public static string gateExitDelete = "/api/gateExit/delete/{id}";

        // Sensor
        public static string sensorGetAll = "/api/sensor/getAll";
        public static string sensorGetAllWithDetails = "/api/sensor/getAllWithDetails";
        public static string sensorExcelReport = "/api/sensor/getAllWithDetailsExcelReport";
        public static string sensorPdfReport = "/api/sensor/getAllWithDetailsPdfReport";
        public static string sensorCreate = "/api/sensor/create";
        public static string sensorEdit = "/api/sensor/edit";
        public static string sensorDelete = "/api/sensor/delete/{id}";
    }
}

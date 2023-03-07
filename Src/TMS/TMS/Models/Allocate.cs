using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class Allocate
    {
        [Key] public int AllocateId { get; set; }
        [ForeignKey("VehicleInfo")]
        public string? VehicleId { get; set; }
        public VehicleInfo ? VehicleInfo { get; set; }
        [ForeignKey("EmployeeInfo")]
        public int EmpId { get; set; }
        public EmployeeInfo ? EmployeeInfo { get; set; }
        [ForeignKey("RouteInfo")]
        public int RouteId { get; set; }
        public RouteInfo ?RouteInfo { get; set; }


        

    }
}

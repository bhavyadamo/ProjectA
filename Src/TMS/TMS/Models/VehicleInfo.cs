using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Models
{
    public class VehicleInfo
    {
        [Key] public string? VehicleId { get; set; }
        public int Capacity { get; set; }
        public int AvailSeats { get; set; }
        public bool Isoperable { get; set; }
        [ForeignKey("RouteInfo")]
        public int RouteId { get; set; }
        public RouteInfo? RouteInfo { get; set; }


    }
}

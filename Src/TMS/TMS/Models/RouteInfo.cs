using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Models
{
    public class RouteInfo
    {
        [Key]
        public int RouteId { get; set; }
        public string? RouteStopName1 { get; set; }
        public string? RouteStopName2 { get; set; }
        public string? RouteStopName3 { get; set; }


    }
}

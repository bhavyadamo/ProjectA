using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Models
{
    public class EmployeeInfo
    {
        [Key]
        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public int Age { get; set; }
        public string? Location { get; set; }

        public double Phone { get; set; }

        [ForeignKey("RouteInfo")]
        public int RouteId { get; set; }
        public RouteInfo ?RouteInfo { get; set; }

    }
}

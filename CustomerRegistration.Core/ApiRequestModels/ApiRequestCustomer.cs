using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.ApiRequestModels
{
    public class ApiRequestCustomer
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Documents { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfRegistration { get; set; }
        public bool? IsActive { get; set; }
        public string Error { get; set; }
    }
}

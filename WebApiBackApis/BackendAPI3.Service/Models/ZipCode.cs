using System.Diagnostics.CodeAnalysis;

namespace BackendAPI3.Service.Models
{
    [ExcludeFromCodeCoverage]
    public class ZipCode
    {
        public int Zip { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
    }
}

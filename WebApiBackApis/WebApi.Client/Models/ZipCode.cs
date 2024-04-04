using System.Diagnostics.CodeAnalysis;

namespace WebApi.Client.Models
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

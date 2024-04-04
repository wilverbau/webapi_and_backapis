using BackendAPI3.Service.Models;

namespace BackendAPI3.Service
{
    public class ZipCodesService : IZipCodesService
    {
        private List<ZipCode> _zipCodes = new()
        {
            new(){ City="Brentwood", County="Suffolk", State="NY", Zip=11717},
            new(){ City="Coram", County="Suffolk", State="NY", Zip=11727},
            new(){ City="Levittown", County="Nassau", State="NY", Zip=11756},
            new(){ City="Floral Park", County="Nassau", State="NY", Zip=11002},
            new(){ City="Ridgewood", County="Queens", State="NY", Zip=11385},
            new(){ City="Philadelphia", County="Philadelphia", State="PA", Zip=19146},
            new(){ City="Arlington", County="Arlington", State="VA", Zip=22222}
        };

        /// <inheritdoc>
        public bool AddZipCode(ZipCode zipCode)
        {
            if (_zipCodes.Any(z=>z.Zip==zipCode.Zip)) 
                return false;
            _zipCodes.Add(zipCode);
            return true;
        }

        /// <inheritdoc>
        public IEnumerable<ZipCode> GetAll()
        {
            return _zipCodes;
        }

        /// <inheritdoc>
        public ZipCode GetByZip(int zip)
        {
            if (!_zipCodes.Any(z => z.Zip == zip))
                return null;
            else
                return _zipCodes.Where(z => z.Zip == zip).First();
        }
    }
}

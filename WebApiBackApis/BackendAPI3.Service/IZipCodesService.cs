using BackendAPI3.Service.Models;

namespace BackendAPI3.Service
{
    public interface IZipCodesService
    {
        /// <summary>
        /// </summary>
        /// <returns>All zipcodes</returns>
        IEnumerable<ZipCode> GetAll();

        /// <summary>
        /// </summary>
        /// <param name="zip">zipcode</param>
        /// <returns>the zipcode information</returns>
        ZipCode GetByZip(int zip);

        /// <summary>
        /// adds a new zipcode
        /// </summary>
        /// <param name="weatherForecast"></param>
        /// <returns>true if the addition was successful</returns>
        bool AddZipCode(ZipCode zipCode);
    }
}

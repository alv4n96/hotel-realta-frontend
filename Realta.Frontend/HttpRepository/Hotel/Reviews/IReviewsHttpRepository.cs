using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.Facilities
{
    public interface IFacilitiesHttpRepository
    {
        Task<List<FacilitiesDto>> GetFacilities(int hotelId);
        Task<PagingResponse<FacilitiesDto>> GetFacilitiesPaging(FacilitiesParameters facilitiesParameter, int hotelId);
    }
}

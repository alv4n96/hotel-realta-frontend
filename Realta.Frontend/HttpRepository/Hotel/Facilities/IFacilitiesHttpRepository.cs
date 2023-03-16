using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;
using Realta.Contract.Models.v1.Hotels;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.Facilities
{
    public interface IFacilitiesHttpRepository
    {
        Task<HotelFaciAllDto> GetFacilities(int hotelId);
        //Task<PagingResponse<FacilitiesDto>> GetFacilitiesPaging(FacilitiesParameter facilitiesParameter);
    }
}

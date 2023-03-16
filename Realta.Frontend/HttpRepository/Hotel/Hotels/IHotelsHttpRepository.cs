using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.Hotels
{
    public interface IHotelsHttpRepository
    {
        Task<List<HotelsDto>> GetHotels();
        Task<HotelsDto?> GetHotelsById(int hotelId);
        Task<PagingResponse<HotelsDto>> GetHotelPaging(HotelsParameters hotelsParameters);
    }
}

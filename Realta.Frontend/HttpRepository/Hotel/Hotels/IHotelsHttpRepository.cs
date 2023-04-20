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
        Task CreateHotel(HotelsDto formHotelDto);
        Task EditHotel(HotelsDto formHotelDto);
        Task EditStatusHotel(HotelSwitchDto switchDto);
        Task DeleteHotel(int hotelId); 
        Task<PagingResponse<HotelsDto>> GetHotelPaging(HotelsParameters hotelsParameters);
    }
}

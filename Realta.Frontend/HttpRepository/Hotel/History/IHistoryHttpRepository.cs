using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.History
{
    public interface IHistoryHttpRepository
    {
        Task<List<FacilityPriceHistoryDto>> GetFacilitiesHistory(int hotelId, int faciId);
        Task<PagingResponse<FacilityPriceHistoryDto>> GetFacilitiesPaging(HistoryParameters historyParameters, int hotelId, int faciId);
    }
}

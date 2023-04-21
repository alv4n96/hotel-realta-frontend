using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.Reviews
{
    public interface IReviewsHttpRepository
    {
        Task<List<HotelReviewsDto>> GetReviews(int hotelId);
        Task<PagingResponse<HotelReviewsDto>> GetReviewsPaging(ReviewsParameters reviewsParameters, int hotelId);
        
        Task CreateReview(HotelReviewsDto formHotelDto);
        
        Task DeleteHotel(int hotelId, int horeId); 
    }
}

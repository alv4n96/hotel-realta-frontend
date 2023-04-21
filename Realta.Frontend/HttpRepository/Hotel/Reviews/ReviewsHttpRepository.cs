using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.Reviews
{
    public class ReviewsHttpRepository : IReviewsHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public ReviewsHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        public async Task<List<HotelReviewsDto>> GetReviews(int hotelId)
        {

            //Call api end point, e.g :https://localhost:7068/api/hotels 
                var response = await _httpClient.GetAsync($"HotelReviews/{hotelId}/reviews/");
                var content = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    var resErr = new List<HotelReviewsDto>();
                    return resErr;
                }

                var facilities = JsonSerializer.Deserialize<List<HotelReviewsDto>>(content, _options);


                return facilities;
        }

        public async Task<PagingResponse<HotelReviewsDto>> GetReviewsPaging(ReviewsParameters reviewsParameters, int hotelId)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = reviewsParameters.PageNumber.ToString(),
                ["searchTerm"] = reviewsParameters.SearchTerm == null ? "" : reviewsParameters.SearchTerm
                // ["orderBy"] = reviewsParameters.OrderBy
            };


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"HotelReviews/{hotelId}/reviews/pagelist",queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<HotelReviewsDto>
            {
                Items = JsonSerializer.Deserialize<List<HotelReviewsDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task CreateReview(HotelReviewsDto formHotelDto)
        {
             var content = JsonSerializer.Serialize(formHotelDto);
                        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                        var postResult = await _httpClient.PostAsync($"HotelReviews/{formHotelDto.HoreHotelId}/reviews/", bodyContent);
                        var postContent = await postResult.Content.ReadAsStringAsync();
                        
                        if (!postResult.IsSuccessStatusCode)
                        {
                            throw new ApplicationException(postContent);
                        }
        }

        public async Task DeleteHotel(int hotelId, int horeId)
        {
            var url = Path.Combine($"HotelReviews/{hotelId}/reviews/{horeId}");

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }



    }
}
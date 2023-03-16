using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.Hotels
{
    public class HotelsHttpRepository : IHotelsHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public HotelsHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<HotelsDto>> GetHotels()
        {
            //Call api end point, e.g :https://localhost:7068/api/hotels 
            var response = await _httpClient.GetAsync("hotels/");
            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var hotels = JsonSerializer.Deserialize<List<HotelsDto>>(content, _options);
            return hotels;

        }

        public async Task<HotelsDto?> GetHotelsById(int hotelId)
        {
            var response = await _httpClient.GetAsync($"hotels/{hotelId}");
            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var hotels = JsonSerializer.Deserialize<HotelsDto>(content, _options);
            
            return hotels;
        }

        public async Task<PagingResponse<HotelsDto>> GetHotelPaging(HotelsParameters hotelsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = hotelsParameters.PageNumber.ToString(),
                ["searchTerm"] = hotelsParameters.SearchTerm == null ? "" : hotelsParameters.SearchTerm
                // ["orderBy"] = hotelsParameters.OrderBy
            };


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("hotels/pageList",queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<HotelsDto>
            {
                Items = JsonSerializer.Deserialize<List<HotelsDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }
    }
}
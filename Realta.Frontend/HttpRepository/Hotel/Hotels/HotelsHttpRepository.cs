using System.Net.Http.Json;
using System.Text;
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

        public async Task CreateHotel(HotelsDto formHotelDto)
        {
            var content = JsonSerializer.Serialize(formHotelDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _httpClient.PostAsync("hotels", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task EditHotel(HotelsDto formHotelDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"hotels/{formHotelDto.HotelId}", formHotelDto);
            var content = await response.Content.ReadAsStringAsync();   
        
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task EditStatusHotel(HotelSwitchDto switchDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"hotels/switch/{switchDto.HotelId}", switchDto);
            var content = await response.Content.ReadAsStringAsync();   
        
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task DeleteHotel(int hotelId)
        {
            var url = Path.Combine("hotels", hotelId.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<PagingResponse<HotelsDto>> GetHotelPaging(HotelsParameters hotelsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = hotelsParameters.PageNumber.ToString(),
                ["searchTerm"] = hotelsParameters.SearchTerm == null ? "" : hotelsParameters.SearchTerm,
                ["orderBy"] = hotelsParameters.OrderBy,
                ["pageSize"] = hotelsParameters.PageSize.ToString()
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
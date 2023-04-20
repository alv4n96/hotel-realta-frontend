using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Hotel.Facilities
{
    public class FacilitiesHttpRepository : IFacilitiesHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public FacilitiesHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<FacilitiesDto>> GetFacilities(int hotelId)
        {
            //Call api end point, e.g :https://localhost:7068/api/hotels 
            var response = await _httpClient.GetAsync($"facilities/{hotelId}/facilities");
            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                var resErr = new List<FacilitiesDto>();
                return resErr;
            }

            var facilities = JsonSerializer.Deserialize<List<FacilitiesDto>>(content, _options);


            return facilities;
        }

        public async Task<FacilitiesDto?> GetFacilityById(int hotelId, int faciId)
        {
            var response = await _httpClient.GetAsync($"facilities/{hotelId}/facilities/{faciId}");
            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var hotels = JsonSerializer.Deserialize<FacilitiesDto>(content, _options);

            return hotels;
        }

        public async Task CreateFacility(FacilitiesDto formFaciDto)
        {
            var content = JsonSerializer.Serialize(formFaciDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _httpClient.PostAsync($"facilities/{formFaciDto.FaciHotelId}/facilities/", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task EditFacility(FacilitiesDto formFaciDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"facilities/{formFaciDto.FaciHotelId}/facilities/{formFaciDto.FaciId}", formFaciDto);
            var content = await response.Content.ReadAsStringAsync();   
        
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task DeleteFacility(int hotelId,int faciId)
        {
            var url = Path.Combine($"facilities/{hotelId}/facilities/{faciId}");

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }


        public async Task<PagingResponse<FacilitiesDto>> GetFacilitiesPaging(FacilitiesParameters facilitiesParameter,
            int hotelId)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = facilitiesParameter.PageNumber.ToString(),
                ["searchTerm"] = facilitiesParameter.SearchTerm == null ? "" : facilitiesParameter.SearchTerm,
                ["orderBy"] = facilitiesParameter.OrderBy,
                ["pageSize"] = facilitiesParameter.PageSize.ToString()
            };


            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString($"facilities/{hotelId}/facilities/pageList",
                    queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<FacilitiesDto>
            {
                Items = JsonSerializer.Deserialize<List<FacilitiesDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };

            return pagingResponse;
        }

        public async Task<List<CategoryGroupDto>> GetCategoryGroup()
        {
            //Call api end point, e.g :https://localhost:7068/api/hotels 
            var response = await _httpClient.GetAsync($"CategoryGroup/");
            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                var resErr = new List<CategoryGroupDto>();
                return resErr;
            }

            var facilities = JsonSerializer.Deserialize<List<CategoryGroupDto>>(content, _options);


            return facilities;
        }

    }
}
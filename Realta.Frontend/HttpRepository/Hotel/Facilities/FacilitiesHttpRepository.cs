using System.Text.Json;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Facilities;

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
                throw new ApplicationException(content);
            }

            var facilities = JsonSerializer.Deserialize<List<FacilitiesDto>>(content, _options);

            
            return facilities;
        }


/*--
        public Task<PagingResponse<FacilitiesDto>> GetFacilitiesPaging(FacilitiesParameter facilitiesParameter)
        {
        
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = facilitiesParameter.PageNumber.ToString(),
                ["searchTerm"] = facilitiesParameter.SearchTerm == null ? "" : facilitiesParameter.SearchTerm
                // ["orderBy"] = facilitiesParameter.OrderBy
            };


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("facilities/{:id}/facilities/pageList",queryStringParam));
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
        --*/
    }
    
}
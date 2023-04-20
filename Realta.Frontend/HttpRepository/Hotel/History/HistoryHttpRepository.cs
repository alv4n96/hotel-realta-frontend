using System.Text.Json;
using Realta.Contract.Models.v1;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.HttpRepository.Hotel.History
{
    public class HistoryHttpRepository : IHistoryHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public HistoryHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        

        public async Task<List<FacilityPriceHistoryDto>> GetFacilitiesHistory(int hotelId, int faciId)
        {
            // Call api end point, e.g :https://localhost:7068/api/hotels 
            var response = await _httpClient.GetAsync($"FacilityPriceHistory/{hotelId}/facility/{faciId}/history");
            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                var resErr = new List<FacilityPriceHistoryDto>();
                return resErr;
            }

            var history = JsonSerializer.Deserialize<List<FacilityPriceHistoryDto>>(content, _options);


            return history;
        }

        public async Task<PagingResponse<FacilityPriceHistoryDto>> GetFacilitiesPaging(HistoryParameters historyParameters,
            int hotelId, int faciId)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = historyParameters.PageNumber.ToString(),
                ["searchTerm"] = historyParameters.SearchTerm == null ? "" : historyParameters.SearchTerm
                // ["orderBy"] = historyParameters.OrderBy
            };


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"FacilityPriceHistory/{hotelId}/facility/{faciId}/history/pagelist",
                    queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<FacilityPriceHistoryDto>
            {
                Items = JsonSerializer.Deserialize<List<FacilityPriceHistoryDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }
    }
}
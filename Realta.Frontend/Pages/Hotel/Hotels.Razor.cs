using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.HttpRepository;
using Realta.Frontend.HttpRepository.Hotel.Hotels;

namespace Realta.Frontend.Pages.Hotel
{
    public partial class Hotels
    {
        [Inject]
        public IHotelsHttpRepository? HotelRepo { get; set; }
        [Parameter]
        public List<HotelsDto> HotelList { get; set; } = new List<HotelsDto>();
        public MetaData MetaData { get; set; } = new MetaData();

        private HotelsParameters _hotelsParameters = new HotelsParameters();

        [Inject]
        public IHotelsHttpRepository HotelsRepo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetProductPaging();
        }

        private async Task SelectedPage(int page)
        {
            _hotelsParameters.PageNumber = page;
            await GetProductPaging();
        }

        private async Task GetProductPaging()
        {
            var pagingResponse = await HotelsRepo.GetHotelPaging(_hotelsParameters);
            HotelList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        
        private async Task SearchChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _hotelsParameters.PageNumber = 1;
            _hotelsParameters.SearchTerm = searchTerm;
            await GetProductPaging();
        }
    }
}

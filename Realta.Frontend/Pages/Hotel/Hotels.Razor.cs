using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.HttpRepository.Hotel.Hotels;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Hotel
{
    public partial class Hotels
    {
        [Inject]
        public IHotelsHttpRepository? HotelRepo { get; set; }
        public List<HotelsDto> HotelList { get; set; } = new List<HotelsDto>();
        public MetaData MetaData { get; set; } = new MetaData();
        
        private HotelsParameters _hotelsParameters = new HotelsParameters();

        private HotelsDto _hotels = new HotelsDto();

        private HotelSwitchDto _hotelSwitchDto = new();
        
        private SuccessNotification _notification;

        private string orderBy = "";
        private string sortOrder = "asc";
        

        [Inject]
        public IHotelsHttpRepository HotelsRepo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetHotelPaging();
        }

        private async Task GetHotelPaging()
        {
            var pagingResponse = await HotelsRepo.GetHotelPaging(_hotelsParameters);
            HotelList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            
        }

        private async Task SelectedPage(int page)
        {
            _hotelsParameters.PageNumber = page;
            await GetHotelPaging();
        }
        
        private async Task SearchChange(string searchTerm)
        {
            _hotelsParameters.PageNumber = 1;
            _hotelsParameters.SearchTerm = searchTerm;
            await GetHotelPaging();
        }
        
        private async Task SortChanged(string columnName)
        {
            if (orderBy != columnName)
            {
                orderBy = columnName;
                sortOrder = "asc";
            }
            else
            {
                sortOrder = sortOrder == "asc" ? "desc" : "asc";
            }
            _hotelsParameters.OrderBy = orderBy + " " + sortOrder;
            await GetHotelPaging();
        }
        
        private async Task Create()
        {
            await HotelRepo.CreateHotel(_hotels);
            _notification.Show("hotel");
            await GetHotelPaging();
        }

        private async Task UpdateHotel(HotelsDto dataHotel)
        {
            _hotels.HotelId = dataHotel.HotelId;
            _hotels.HotelName = dataHotel.HotelName;
            _hotels.HotelPhonenumber = dataHotel.HotelPhonenumber;
            _hotels.HotelDescription = dataHotel.HotelDescription;
            _hotels.HotelAddrDescription = dataHotel.HotelAddrDescription == null ? "Jl Abu Ali 29" : dataHotel.HotelAddrDescription;
        }

        private async Task OnUpdateConfirm()
        {
            await HotelRepo.EditHotel(_hotels);
            _notification.Show("hotel");
            await GetHotelPaging();
        }
        
        private async Task UpdateSwitchStatusHotel(HotelsDto switchDto)
        {
            _hotelSwitchDto.HotelId = switchDto.HotelId;
            _hotelSwitchDto.HotelStatus = switchDto.HotelStatus;
            _hotelSwitchDto.HotelReasonStatus = switchDto.HotelReasonStatus == null ? "" : switchDto.HotelReasonStatus;
        }
        
        private async Task OnUpdateStatusHotelConfirm()
        {
            await HotelRepo.EditStatusHotel(_hotelSwitchDto);
            _notification.Show("hotel");
            await GetHotelPaging();
        
        }
        
        private async Task Delete(int id)
        {
            await HotelRepo.DeleteHotel(id);
            _hotelsParameters.PageNumber = 1;
            _notification.Show("hotel");
            await GetHotelPaging();;
        }

        private void Clear()
        {
            // reset nilai input
            _hotels =  new();

        }

        private async Task PageSizeChanged(int page)
        {
            _hotelsParameters.PageSize = page;
            await GetHotelPaging();
        }
            
    }
}

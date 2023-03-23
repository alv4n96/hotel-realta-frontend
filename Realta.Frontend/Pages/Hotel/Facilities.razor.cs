using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1;
using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.HttpRepository.Hotel.Facilities;
using Realta.Frontend.HttpRepository.Hotel.Hotels;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Hotel;
public partial class Facilities
{
    [Parameter] public int HotelId { get; set; } = 1;
    
    [Inject]
    public IFacilitiesHttpRepository? FaciRepo { get; set; }
    [Inject]
    public  IHotelsHttpRepository? HotelRepo { get; set; }
    
    public List<FacilitiesDto> FacilitiesData { get; set; } = new();
    
    public List<CategoryGroupDto> CategoryGroupData { get; set; } = new();
    
    public MetaData MetaData { get; set; } = new();
    
    public HotelsDto HotelData { get; set; } = new();
    
    private FacilitiesParameters _facilitiesParameters = new FacilitiesParameters();

    private FacilitiesDto _facilities = new();

    private SuccessNotification _notification;

    private string _orderBy = "";
    private string _sortOrder = "asc";


    
    protected override async Task OnInitializedAsync()
    {
        HotelData = await HotelRepo.GetHotelsById(HotelId);
        CategoryGroupData = await FaciRepo.GetCategoryGroup();
        await GetFaciPaging();
    }

    private async Task GetFaciPaging()
    {
        var pagingResponse = await FaciRepo.GetFacilitiesPaging(_facilitiesParameters,HotelId);
        FacilitiesData = pagingResponse.Items;
        MetaData = pagingResponse.MetaData;
    }
    
    private async Task SelectedPage(int page)
    {
        FacilitiesData = await FaciRepo.GetFacilities(HotelId);
        _facilitiesParameters.PageNumber = page;
        await GetFaciPaging();
    }
        
    private async Task SearchChange(string searchTerm)
    {
        _facilitiesParameters.PageNumber = 1;
        _facilitiesParameters.SearchTerm = searchTerm;
        await GetFaciPaging();
    }
    
    private async Task SortChanged(string columnName)
    {
        if (_orderBy != columnName)
        {
            _orderBy = columnName;
            _sortOrder = "asc";
        }
        else
        {
            _sortOrder = _sortOrder == "asc" ? "desc" : "asc";
        }
        _facilitiesParameters.OrderBy = _orderBy + " " + _sortOrder;
        await GetFaciPaging();
    }

    private async Task Create()
    {
        await FaciRepo.CreateFacility(_facilities);
        _notification.Show($"hotel/{_facilities.FaciHotelId}/facilities");
        await GetFaciPaging();
    }

    private async Task UpdateFacility(FacilitiesDto dataFaci)
    {
        _facilities.FaciId = dataFaci.FaciId;
        _facilities.FaciName = dataFaci.FaciName;
        _facilities.FaciDescription = dataFaci.FaciDescription;
        _facilities.FaciCagroId = dataFaci.FaciCagroId;
        _facilities.FaciRoomNumber = dataFaci.FaciRoomNumber;
        _facilities.FaciMaxNumber = dataFaci.FaciMaxNumber;
        _facilities.FaciLowPrice = dataFaci.FaciLowPrice;
        _facilities.FaciHighPrice = dataFaci.FaciHighPrice;
        _facilities.FaciDiscount = dataFaci.FaciDiscount;
        _facilities.FaciTaxRate = dataFaci.FaciTaxRate;
        _facilities.FaciStartdate = dataFaci.FaciStartdate;
        _facilities.FaciEndDate = dataFaci.FaciEndDate;
        _facilities.FaciExposePrice = dataFaci.FaciExposePrice;
        _facilities.FaciHotelId = HotelId;
    }

    private async Task OnUpdateConfirm()
    {
        await FaciRepo.EditFacility(_facilities);
        _notification.Show($"hotel/{_facilities.FaciHotelId}/facilities");
        await GetFaciPaging();
    }

    private async Task Delete(int hotelId, int faciId)
    {
        await FaciRepo.DeleteFacility(hotelId, faciId);
        _facilitiesParameters.PageNumber = 1;
        _notification.Show($"hotel/{hotelId}/facilities");
        await GetFaciPaging();;
        
    }
    
    
    private void Clear()
    {
        _facilities = new();
        _facilities.FaciUserId = 2;
        _facilities.FaciHotelId = HotelId;
        _facilities.FaciEndDate = DateTime.Now;
        _facilities.FaciStartdate = DateTime.Now;
    }

    private async Task PageSizeChanged(int page)
    {
        _facilitiesParameters.PageSize = page;
        await GetFaciPaging();
        Console.WriteLine(page);
    }

    
    
}

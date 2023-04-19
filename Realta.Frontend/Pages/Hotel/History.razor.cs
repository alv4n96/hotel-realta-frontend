using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.Repositories.v1;
using Realta.Domain.RequestFeatures;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.HttpRepository.Hotel.Facilities;
using Realta.Frontend.HttpRepository.Hotel.History;
using Realta.Frontend.HttpRepository.Hotel.Hotels;

namespace Realta.Frontend.Pages.Hotel;

public partial class History
{
    [Parameter]
    public int HotelId { get; set; }

    [Parameter]
    public int FaciId { get; set; }
    
    [Inject]
    public IFacilitiesHttpRepository? FaciRepo { get; set; }
    [Inject]
    public  IHotelsHttpRepository? HotelRepo { get; set; }
    [Inject]
    public  IHistoryHttpRepository HistoryRepo { get; set; }
    
    public FacilitiesDto FacilitiesData { get; set; } = new();

    public HotelsDto HotelData { get; set; } = new();

    public List<FacilityPriceHistoryDto> HistoryData { get; set; } = new();
    
    public MetaData MetaData { get; set; } = new();
    
    private HistoryParameters _historyParameters = new();

    
    
    protected override async Task OnInitializedAsync()
    {
        HotelData = await HotelRepo.GetHotelsById(HotelId);
        FacilitiesData = await FaciRepo.GetFacilityById(HotelId, FaciId);
        await GetProductPaging();
    }

    private async Task SelectedPage(int page)
    {
        HistoryData = await HistoryRepo.GetFacilitiesHistory(HotelId, FaciId);
        _historyParameters.PageNumber = page;
        await GetProductPaging();
    }

    private async Task GetProductPaging()
    {
        var pagingResponse = await HistoryRepo.GetFacilitiesPaging(_historyParameters, HotelId, FaciId);
        HistoryData = pagingResponse.Items;
        MetaData = pagingResponse.MetaData;
    }
        
    private async Task SearchChange(string searchTerm)
    {
        Console.WriteLine(searchTerm);
        _historyParameters.PageNumber = 1;
        _historyParameters.SearchTerm = searchTerm;
        await GetProductPaging();
    }


}
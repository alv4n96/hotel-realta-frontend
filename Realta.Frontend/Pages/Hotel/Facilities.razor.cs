using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1;
using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.HttpRepository.Hotel.Facilities;
using Realta.Frontend.HttpRepository.Hotel.Hotels;

namespace Realta.Frontend.Pages.Hotel;

public partial class Facilities
{
    [Parameter] public int HotelId { get; set; } = 1;
    
    [Inject]
    public IFacilitiesHttpRepository? FaciRepo { get; set; }
    [Inject]
    public  IHotelsHttpRepository? HotelRepo { get; set; }
    
    
    public List<FacilitiesDto> FacilitiesData { get; set; } = new();

    public HotelsDto HotelData { get; set; } = new();

    
    public MetaData MetaData { get; set; } = new();
    private FacilitiesParameters _facilitiesParameters = new FacilitiesParameters();

    
    protected override async Task OnInitializedAsync()
    {
        HotelData = await HotelRepo.GetHotelsById(HotelId);
        await GetProductPaging();
    }

    private async Task SelectedPage(int page)
    {
        FacilitiesData = await FaciRepo.GetFacilities(HotelId);
        _facilitiesParameters.PageNumber = page;
        await GetProductPaging();
    }

    private async Task GetProductPaging()
    {
        var pagingResponse = await FaciRepo.GetFacilitiesPaging(_facilitiesParameters,HotelId);
        FacilitiesData = pagingResponse.Items;
        MetaData = pagingResponse.MetaData;
    }
        
    private async Task SearchChange(string searchTerm)
    {
        Console.WriteLine(searchTerm);
        _facilitiesParameters.PageNumber = 1;
        _facilitiesParameters.SearchTerm = searchTerm;
        await GetProductPaging();
    }
}
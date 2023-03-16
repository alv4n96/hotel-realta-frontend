using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1;
using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Hotels;
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
    
    
    [Parameter]
    public List<FacilitiesDto> FacilitiesData { get; set; } = new();

    public HotelsDto HotelData { get; set; } = new();
    public MetaData MetaData { get; set; } = new();


    //private HotelsParameters _hotelsParameters = new HotelsParameters();

    protected override async Task OnInitializedAsync()
    {
        HotelData = await HotelRepo.GetHotelsById(HotelId);
        FacilitiesData = await FaciRepo.GetFacilities(HotelId);
    }
    
}
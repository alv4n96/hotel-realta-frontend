using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;
using Realta.Contract.Models.v1.Facilities;
using Realta.Frontend.HttpRepository.Hotel.Facilities;
using Realta.Frontend.HttpRepository.Hotel.Hotels;

namespace Realta.Frontend.Pages.Hotel;

public partial class Facilities
{
    [Parameter] public int HotelId { get; set; } = 1;
    
    [Inject]
    public IFacilitiesHttpRepository? FaciRepo { get; set; }
    
    
    [Parameter]
    public HotelFaciAllDto HotelFaciData { get; set; } = new();
    public MetaData MetaData { get; set; } = new();

    //private HotelsParameters _hotelsParameters = new HotelsParameters();

    protected override async Task OnInitializedAsync()
    {
        HotelFaciData = await FaciRepo.GetFacilities(HotelId);
    }
    
}
using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1.Hotels;

namespace Realta.Frontend.Components.Hotel.Facilities;

public partial class HotelFacilitiesTable
{
    [Parameter] 
    public HotelsDto HotelData { get; set; }
    
}
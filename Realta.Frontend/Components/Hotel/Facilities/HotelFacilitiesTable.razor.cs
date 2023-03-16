using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1.Facilities;

namespace Realta.Frontend.Components.Hotel.Facilities;

public partial class HotelFacilitiesTable
{
    [Parameter] 
    public  List<HotelFaciAllDto> HotelFaci { get; set; }
    
}
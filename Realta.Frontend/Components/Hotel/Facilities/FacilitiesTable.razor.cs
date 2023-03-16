using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1;

namespace Realta.Frontend.Components.Hotel.Facilities;

public partial class FacilitiesTable
{
    [Parameter]
    public int HotelId { get; set; }
    [Parameter]
    public List<FacilitiesDto> FacilitiesData { get; set; } 
    
    
}
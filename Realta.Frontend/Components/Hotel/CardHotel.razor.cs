using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1.Hotels;

namespace Realta.Frontend.Components.Hotel;

public partial class CardHotel
{
    [Parameter] 
    public HotelsDto HotelData { get; set; }
    
}
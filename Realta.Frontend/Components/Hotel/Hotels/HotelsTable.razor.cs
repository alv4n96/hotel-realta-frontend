using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1.Hotels;

namespace Realta.Frontend.Components.Hotel.Hotels
{
   public partial class HotelsTable
   {
      [Parameter] 
      public  List<HotelsDto> DataHotel { get; set; }
      
   } 
}
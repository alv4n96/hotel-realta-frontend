using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1.Hotels;

namespace Realta.Frontend.Components.Hotel.Hotels
{
    public partial class HotelsTable
    {
        [Parameter] public List<HotelsDto> DataHotel { get; set; }

        [Parameter]
        public EventCallback OnAdd { get; set; }

        private void ClearInput()
        {
            // panggil event OnAdd ketika tombol "Add" ditekan
            OnAdd.InvokeAsync();
        }

    }
}
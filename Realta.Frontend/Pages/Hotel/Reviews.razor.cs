using Microsoft.AspNetCore.Components;
using Realta.Contract.Models.v1;
using Realta.Contract.Models.v1.Hotels;
using Realta.Domain.RequestFeatures;
using Realta.Domain.RequestFeatures.HotelParameters;
using Realta.Frontend.HttpRepository.Hotel.Hotels;
using Realta.Frontend.HttpRepository.Hotel.Reviews;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Hotel;

public partial class Reviews
{
    [Parameter] public int HotelId { get; set; } = 1;

    [Inject] public IReviewsHttpRepository? ReviewsRepo { get; set; }
    [Inject] public IHotelsHttpRepository? HotelRepo { get; set; }

    private SuccessNotification _notification;
    public List<HotelReviewsDto> ReviewsData { get; set; } = new();

    private HotelReviewsDto _hotelReviewsDto = new();
    public HotelsDto HotelData { get; set; } = new();


    public MetaData MetaData { get; set; } = new();
    private ReviewsParameters _reviewsParameters = new ReviewsParameters();


    protected override async Task OnInitializedAsync()
    {
        HotelData = await HotelRepo.GetHotelsById(HotelId);
        await GetReviewsPaging();
    }

    private async Task SelectedPage(int page)
    {
        ReviewsData = await ReviewsRepo.GetReviews(HotelId);
        _reviewsParameters.PageNumber = page;
        await GetReviewsPaging();
    }

    private async Task GetReviewsPaging()
    {
        // var hotelResponse = await  HotelRepo .GetHotelsById(HotelId);
        var pagingResponse = await ReviewsRepo.GetReviewsPaging(_reviewsParameters, HotelId);
        ReviewsData = pagingResponse.Items;
        MetaData = pagingResponse.MetaData;
        
    }

    private async Task SearchChange(string searchTerm)
    {
        Console.WriteLine(searchTerm);
        _reviewsParameters.PageNumber = 1;
        _reviewsParameters.SearchTerm = searchTerm;
        await GetReviewsPaging();
    }

    private async Task Clear()
    {
        _hotelReviewsDto = new();
    }

    private async Task Create()
    {
        _hotelReviewsDto.HoreUserId = 5;
        _hotelReviewsDto.HoreHotelId = HotelId;
        _hotelReviewsDto.HoreUserReview =
            _hotelReviewsDto.HoreUserReview == null ? "" : _hotelReviewsDto.HoreUserReview;
        await ReviewsRepo.CreateReview(_hotelReviewsDto);
        
        _reviewsParameters.PageNumber = 1;
        _notification.Show($"hotel/{HotelId}/reviews");
        await GetReviewsPaging();
    }

    private async Task Delete(int hotelId, int horeId)
    {
        await ReviewsRepo.DeleteHotel(hotelId, horeId);
        _reviewsParameters.PageNumber = 1;
        _notification.Show($"hotel/{hotelId}/reviews");
        await GetReviewsPaging();
    }
}
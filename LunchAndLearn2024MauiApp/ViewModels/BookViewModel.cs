using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LunchAndLearn2024MauiApp.Services;
using LunchAndLearn2024Models;
using System.Collections.ObjectModel;

namespace LunchAndLearn2024MauiApp.ViewModels;
[QueryProperty(nameof(BookItem), "BookItem")]
public partial class BookViewModel(IApiService apiService, ITelemetry telemetry, IMediaPicker mediaPicker) : BaseViewModel
{
    private Book? bookItem;
    [ObservableProperty]
    string? status;
    [ObservableProperty]
    string? imageUrl;
    [ObservableProperty]
    ObservableCollection<string> statuses = [CurrentStatus.started.ToString(), CurrentStatus.next.ToString(), CurrentStatus.completed.ToString()];

    [RelayCommand]
    async Task AddUpdateBook()
    {
        IsBusy = true;
        try
        {
            if (BookItem != null && !string.IsNullOrWhiteSpace(BookItem.Title))
            {
                BookItem.Status = Status != null ? (CurrentStatus)(Enum.Parse(typeof(CurrentStatus), Status, true)) : BookItem.Status;
                var newImageUrl = ImageUrl != null && BookItem.ImageUrl != ImageUrl ? ImageUrl : null;
                BookItem = await apiService.AddUpdateBookAsync(BookItem);

                if (BookItem != null && !string.IsNullOrWhiteSpace(newImageUrl))
                {
                    await apiService.UploadBookImageAsync(BookItem.Id!, newImageUrl);
                }

                var queryParameters = new ShellNavigationQueryParameters
                {
                    { "RefreshBooks", true }
                };
                await Shell.Current.GoToAsync("..", true, queryParameters);
            }
        }
        catch (Exception ex)
        {
            telemetry.TrackException(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task TakePhoto()
    {
        if (mediaPicker.IsCaptureSupported)
        {
            FileResult? photo = await mediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                ImageUrl = localFilePath;
                Console.WriteLine(ImageUrl);
            }
        }
    }

    [RelayCommand]
    async Task GetImage()
    {
        FileResult? photo = await mediaPicker.PickPhotoAsync();

        if (photo != null)
        {
            //using Stream sourceStream = await photo.OpenReadAsync();
            ImageUrl = photo.FullPath;
        }
        
    }

    public Book? BookItem
    {
        get => bookItem;
        set
        {
            bookItem = value ?? new Book();
            Status = bookItem?.Status.ToString();
            ImageUrl = bookItem?.ImageUrl;
            OnPropertyChanged();
        }
    }
}

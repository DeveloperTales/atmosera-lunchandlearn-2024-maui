
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LunchAndLearn2024MauiApp.Services;
using LunchAndLearn2024Models;

namespace LunchAndLearn2024MauiApp.ViewModels;
[QueryProperty(nameof(RefreshBooks), "RefreshBooks")]
public partial class BooksViewModel(IApiService apiService, ITelemetry telemetry) : BaseViewModel
{
    private bool loading = false;
    private bool refreshBooks = false;

    [ObservableProperty]
    ObservableCollection<Book> books = [];

    [ObservableProperty]
    Book? selectedItem;

    public CurrentStatus CurrentStatus{ get; set; } = CurrentStatus.started;
    
    [RelayCommand]
    async Task SelectBook(Book item)
    {
        var queryParameters = new ShellNavigationQueryParameters
        {
            { "BookItem", item }
        };
        await AppShell.Current.GoToAsync("books/book", true, queryParameters);
        SelectedItem = null;
    }

    [RelayCommand]
    async Task AddUpdateBook()
    {
        var queryParameters = new ShellNavigationQueryParameters
        {
            { "BookItem", new Book() }
        };
        await Shell.Current.GoToAsync("books/book", true, queryParameters);
    }

    [RelayCommand]
    async Task LoadBooks()
    {
        if (loading)
        {
            return;
        }

        loading = true;
        IsBusy = true;

        try
        {
            Books.Clear();
            var booksByStatus = await apiService.GetBooksByStatusAsync(CurrentStatus);
            booksByStatus.ToList().ForEach(Books.Add);
            IsInitialized = true;
        }
        catch (Exception ex)
        {
            telemetry.TrackException(ex);
        }
        finally
        {
            loading = false;
            IsBusy = false;
        }
    }

    public bool RefreshBooks
    {
        get => refreshBooks;
        set
        {
            refreshBooks = value;
            if (refreshBooks)
            {
                IsInitialized = false;
            }
        }
    }
}

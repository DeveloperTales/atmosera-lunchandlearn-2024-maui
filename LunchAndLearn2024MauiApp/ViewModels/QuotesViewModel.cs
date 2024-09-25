
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LunchAndLearn2024MauiApp.Services;
using LunchAndLearn2024Models;
using System.Collections.ObjectModel;

namespace LunchAndLearn2024MauiApp.ViewModels;
public partial class QuotesViewModel(IApiService apiService, ITelemetry telemetry) : BaseViewModel
{
    private bool loading = false;

    [ObservableProperty]
    ObservableCollection<Quote> quotes = [];

    [RelayCommand]
    async Task LoadQuotes()
    {
        if (loading)
        {
            return;
        }

        loading = true;
        IsBusy = true;

        try
        {
            Quotes.Clear();
            var currentQuotes = await apiService.GetQuotesAsync();
            currentQuotes.ToList().ForEach(Quotes.Add);
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
}

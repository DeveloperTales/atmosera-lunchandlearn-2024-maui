using LunchAndLearn2024MauiApp.ViewModels;
using LunchAndLearn2024Models;

namespace LunchAndLearn2024MauiApp.Views;

public partial class QuotesPage : ContentPage
{
	public QuotesPage(QuotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is QuotesViewModel viewModel)
        {
            if (!viewModel.IsInitialized)
            {
                await viewModel.LoadQuotesCommand.ExecuteAsync(null);
            }
        }
    }
}
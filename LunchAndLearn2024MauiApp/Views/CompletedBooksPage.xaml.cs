using LunchAndLearn2024MauiApp.ViewModels;
using LunchAndLearn2024Models;

namespace LunchAndLearn2024MauiApp.Views;

public partial class CompletedBooksPage : ContentPage
{
	public CompletedBooksPage(BooksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BooksViewModel viewModel)
        {
            if (!viewModel.IsInitialized)
            {
				viewModel.CurrentStatus = CurrentStatus.completed;
                await viewModel.LoadBooksCommand.ExecuteAsync(null);
            }
        }
    }
}
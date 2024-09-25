using LunchAndLearn2024MauiApp.ViewModels;

namespace LunchAndLearn2024MauiApp.Views;

public partial class BookAddUpdatePage : ContentPage
{
	public BookAddUpdatePage(BookViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

using LunchAndLearn2024MauiApp.ViewModels;

namespace LunchAndLearn2024MauiApp.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (BindingContext is AboutViewModel aboutViewModel)
        {
            aboutViewModel.UpdateDeviceOrientationString();
        }
    }
}
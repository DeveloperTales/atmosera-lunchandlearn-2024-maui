using LunchAndLearn2024MauiApp.Views;

namespace LunchAndLearn2024MauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("books/book", typeof(BookAddUpdatePage));
    }
}

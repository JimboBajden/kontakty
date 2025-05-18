namespace kontakty.MVVM.Pages;
using MVVM.Models;
using System.Collections.ObjectModel;

public partial class MainPage : ContentPage
{
   public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();
    List<Person> tmp = new List<Person>();
    public MainPage()
	{
		InitializeComponent();
        People.Add(new Person { name = "Jan", surname = "Kowalski" });
        BindingContext = this;
    }

    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;
        var contact = clickedItem?.BindingContext as Person;

        Navigation.PushAsync(new EditPage(contact, People, CensusDisplay));


        CensusDisplay.SelectedItem = null;
    }

    private void CensusDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selection = ((CollectionView)sender);
        tmp = selection.SelectedItems.Cast<Person>().ToList();
    }

    private void MenuFlyoutItem_Clicked_1(object sender, EventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;
        var contact = clickedItem?.BindingContext as Person;
        if (contact != null)
        {
            People.Remove(contact);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddingPage(People, CensusDisplay));
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        foreach (var item in tmp)
        {
            People.Remove(item);
        }
    }
}
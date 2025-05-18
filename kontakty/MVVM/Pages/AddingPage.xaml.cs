using kontakty.MVVM.Models;
using System.Collections.ObjectModel;

namespace kontakty.MVVM.Pages;

public partial class AddingPage : ContentPage
{
    public Person _person = new Person();
    public ObservableCollection<Person> osoby;
    public CollectionView kolekcja;
    public AddingPage(ObservableCollection<Person> census, CollectionView tmp)
    {
        InitializeComponent();
        osoby = census;
        kolekcja = tmp;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) || string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        _person.name = FirstNameEntry.Text;
        _person.surname = LastNameEntry.Text;
        osoby.Add(_person);
        Navigation.RemovePage(this);
        kolekcja.BindingContext = null;
        kolekcja.ItemsSource = null;
        kolekcja.BindingContext = osoby;
        kolekcja.ItemsSource = osoby;
    }
}
using System.Collections.ObjectModel;

namespace kontakty.MVVM.Pages;
using MVVM.Models;
public partial class EditPage : ContentPage
{
    private readonly Person _person;
    private readonly ObservableCollection<Person> _census;
    private CollectionView kolekcja;
    public EditPage(Person person, ObservableCollection<Person> census, CollectionView tmp)
    {
        InitializeComponent();
        _person = person;
        _census = census;
        kolekcja = tmp;
        FirstNameEntry.Text = person.name;
        LastNameEntry.Text = person.surname;
    }
    private void OnSaveClicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(FirstNameEntry.Text) || string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        _person.name = FirstNameEntry.Text;
        _person.surname = LastNameEntry.Text;
        Navigation.RemovePage(this);
        kolekcja.BindingContext = null;
        kolekcja.ItemsSource = null;
        kolekcja.BindingContext = _census;
        kolekcja.ItemsSource = _census;
    }
}

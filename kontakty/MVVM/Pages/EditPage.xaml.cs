using System.Collections.ObjectModel;

namespace kontakty.MVVM.Pages;
using MVVM.Models;

public partial class EditPage : ContentPage
{
    private Person _person = new Person();
    private ObservableCollection<Person> _census;
    private CollectionView kolekcja;
    private int id;
    private int _page;
    public EditPage(Person person, ObservableCollection<Person> census, CollectionView tmp , int page)
    {
        InitializeComponent();
        _person = person;
        _census = census;
        kolekcja = tmp;
        FirstNameEntry.Text = person.name;
        LastNameEntry.Text = person.surname;
        id = person.id;
        _page = page;
    }
    private void OnSaveClicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(FirstNameEntry.Text) || string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        Baza baza = new Baza();
        baza.update(id, FirstNameEntry.Text, LastNameEntry.Text);
        _census.Clear();
        ObservableCollection<Person> test213 = new ObservableCollection<Person>();
        test213 = baza.GetPeople(_page);
        foreach (Person person in test213)
        {
            _census.Add(person);
        }
        Navigation.RemovePage(this);
        kolekcja.BindingContext = null;
        kolekcja.ItemsSource = null;
        kolekcja.BindingContext = _census;
        kolekcja.ItemsSource = _census;
    }
}

using System.Collections.ObjectModel;

namespace kontakty.MVVM.Pages;
using MVVM.Models;

public partial class EditPage : ContentPage
{
    public Person _person { get; set; } = new Person();
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
        id = person.id;
        _page = page;
        BindingContext = this;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(FirstNameEntry.Text) || string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        Baza baza = new Baza();
        baza.update(id, _person.name, _person.surname , _person.number);
        ObservableCollection<Person> test213 = new ObservableCollection<Person>();
        _census.Clear();
        test213 = baza.GetPeople(_page);
        foreach (Person person in test213)
        {
            _census.Add(person);
        }
        Navigation.RemovePage(this);
        
    }
}

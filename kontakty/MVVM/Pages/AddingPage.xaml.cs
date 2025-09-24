using kontakty.MVVM.Models;
using System.Collections.ObjectModel;

namespace kontakty.MVVM.Pages;

public partial class AddingPage : ContentPage
{
    Baza baza = new Baza();
    public Person _person { get; set; } = new Person();
    public ObservableCollection<Person> osoby;
    public CollectionView kolekcja;
    public int _strona;
    public AddingPage(ObservableCollection<Person> census, CollectionView tmp, int strona)
    {
        InitializeComponent();
        osoby = census;
        kolekcja = tmp;
        _strona = strona;
        _person = new Person();
        BindingContext = this;
    }
    
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) || string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        osoby.Clear();baza.Add(_person.name , _person.surname , _person.number);
        ObservableCollection<Person> test213 = new ObservableCollection<Person>();
        test213 = baza.GetPeople(_strona-1);
        foreach (Person person in test213)
        {
            osoby.Add(person);
        }
        
        Navigation.PopAsync();
        
    }
}
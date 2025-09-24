namespace kontakty.MVVM.Pages;
using MVVM.Models;
using System.Collections.ObjectModel;
public partial class MainPage : ContentPage
{
   public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();
    List<int> tmp = new List<int>();
    Baza baza = new Baza();
    int page = 1;
    int pageCount = 1;
    public MainPage()
	{
		InitializeComponent();
        BindingContext = this;
        ObservableCollection<Person> test213 = new ObservableCollection<Person>();
        test213 = baza.GetPeople(0);
        foreach (Person person in test213)
        {
            People.Add(person);
        }
        pageCount = baza.PageCount();
        pager.Text = page.ToString() + '/' + pageCount;
    }
    private void update()
    {
        People.Clear();
        ObservableCollection<Person> test213 = new ObservableCollection<Person>();
        test213 = baza.GetPeople(page);
        foreach (Person person in test213)
        {
            People.Add(person);
        }
        pageCount = baza.PageCount();
    }

    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;
        var contact = clickedItem?.BindingContext as Person;

        Navigation.PushAsync(new EditPage(contact , People, CensusDisplay,page));

    }

    private void CensusDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selection = ((CollectionView)sender);
        tmp.Clear(); // Optional: clear previous selection
        foreach (var item in selection.SelectedItems)
        {
            if (item is Person person)
            {
                tmp.Add(person.id);
            }
        }

    }

    private void MenuFlyoutItem_Clicked_1(object sender, EventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;
        var contact = clickedItem?.BindingContext as Person;
        if (contact.id != null)
        {
            baza.DeleteById(contact.id);
            People.Clear();
            if (page > baza.PageCount()) { page = baza.PageCount(); }
            pager.Text = page.ToString() + '/' + baza.PageCount();
            update();
        }

        
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
         Navigation.PushAsync(new AddingPage(People, CensusDisplay ,page ));

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        baza.DeleteMultiple(tmp);
        if (page > baza.PageCount()) { page = baza.PageCount(); }
        pager.Text = page.ToString() + '/' + baza.PageCount();
        update();
        tmp.Clear();
    }

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {
        var clickedItem = sender as Button;
        var contact = clickedItem?.BindingContext as Person;

        Navigation.PushAsync(new EditPage(contact, People, CensusDisplay , page));


        CensusDisplay.SelectedItem = null;
    }

    private void SwipeItem_Invoked_1(object sender, EventArgs e)
    {
        var clickedItem = sender as Button;
        var contact = clickedItem?.BindingContext as Person;
        if (contact != null)
        {
            baza.DeleteById(contact.id);
        }
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        pageCount = baza.PageCount();
        if(page < pageCount) { page++; update(); tmp.Clear(); }
        pageCount = baza.PageCount();
        pager.Text = page.ToString() + '/' + pageCount;

    }

    private void Button_Clicked_3(object sender, EventArgs e)
    {
        pageCount = baza.PageCount();
        if (page > 1) {  page--; update(); tmp.Clear(); }
        pageCount = baza.PageCount();
        pager.Text = page.ToString() + '/' + pageCount;

    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = sender as SearchBar;
        if (!string.IsNullOrEmpty(searchBar.Text))
        {
            People.Clear();
            ObservableCollection<Person> test213 = baza.Search(searchBar.Text);
            foreach (Person person in test213)
            {
                People.Add(person);
            }
            page = 1;
            pager.Text = "1/1";
            pageCount = 1;
        }else
        {
            update();
            pager.Text = page.ToString() + '/' + pageCount;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        pageCount = baza.PageCount();
        if (page > pageCount) { page = pageCount; }
        pager.Text = page.ToString() + '/' + pageCount;
        update();
        tmp.Clear();
    }
}
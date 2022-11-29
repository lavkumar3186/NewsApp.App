using NewsApp.Model;
using NewsApp.Services;

namespace NewsApp.Pages;

public partial class NewsPage : ContentPage
{
	private bool _isNextPage = false;
	public List<Article> Articles { get; set; }
	public List<Category> Categories { get; set; }
	public NewsPage()
	{
		InitializeComponent();
		Articles= new List<Article>();
		Categories= new List<Category>();
		LoadCategories();
		CvCategories.ItemsSource = Categories;
	}

	private void LoadCategories()
	{
		Categories.Add(new Category() { Name = "Breaking-news" });
		Categories.Add(new Category() { Name = "World" });
		Categories.Add(new Category() { Name = "Nation" });
		Categories.Add(new Category() { Name = "Business" });
		Categories.Add(new Category() { Name = "Technology" });
		Categories.Add(new Category() { Name = "Entertainment" });
		Categories.Add(new Category() { Name = "Sports" });
		Categories.Add(new Category() { Name = "Science" });
		Categories.Add(new Category() { Name = "Health" });
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (!_isNextPage)
		{
			await PassCategoryAsync("breaking-news");
		}
		
	}

	private async Task PassCategoryAsync(string category)
	{
		CvNews.ItemsSource = null;
		Articles.Clear();
        var apiService = new ApiService();
        var newsData = await apiService.GetNewsAsync(category);
        foreach (var item in newsData.Articles)
        {
            Articles.Add(item);
        }

        CvNews.ItemsSource = Articles;
    }

	private async void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedCategory = e.CurrentSelection.FirstOrDefault() as Category;
		if (selectedCategory != null)
		{
			await PassCategoryAsync(selectedCategory.Name);
			_isNextPage = true;
		}
		((CollectionView)sender).SelectedItem = null;
	}

    private void CvNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedArticle = e.CurrentSelection.FirstOrDefault() as Article;
		if (selectedArticle != null)
		{
			Navigation.PushAsync(new NewsDetailPage(selectedArticle));
		}
        ((CollectionView)sender).SelectedItem = null;
    }
}
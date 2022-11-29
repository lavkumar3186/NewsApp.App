using NewsApp.Model;

namespace NewsApp.Pages;

public partial class NewsDetailPage : ContentPage
{
    private string newsUrl;
	public NewsDetailPage(Article article)
	{
		InitializeComponent();
		ImgNews.Source = article.Image;
		LblTitle.Text = article.Title;
		LblContent.Text = article.Content;
        newsUrl = article.Url;
	}

    private async  void TbShare_Clicked(object sender, EventArgs e)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = newsUrl,
            Title = "Share"
        });
    }
}
// https://feeds.bbci.co.uk/news/rss.xml

using Microsoft.SemanticKernel;
using SimpleFeedReader;
using System.ComponentModel;

public class NewsPlugin
{
    [KernelFunction("get_news")] // recommendation to use snake case
    [Description("Get news items for today's date.")]
    [return: Description("A list of current news stories.")]
    public async Task<List<FeedItem>> GetNewsAsync(Kernel kernel, string category, int numberOfNews = 5)
    {
        var feedURL = $"https://feeds.bbci.co.uk/news/{category}/rss.xml";

        var reader = new FeedReader();

        var feed = await reader.RetrieveFeedAsync(feedURL);

        return feed.Take(numberOfNews).ToList();
    }
}
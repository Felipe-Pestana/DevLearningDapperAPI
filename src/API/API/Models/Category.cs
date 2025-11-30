namespace API.Models
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public string Summary { get; private set; }
        public int Order { get; private set; }
        public bool Featured { get; private set; }

        public Category(string title, string url, string summary, int order, bool featured)
        {
            Title = title;
            Url = url;
            Summary = summary;
            Order = order;
            Featured = featured;
        }
    }
}

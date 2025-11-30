using System;
using System.Text.Json.Serialization;

namespace DevLearningAPI.Models;

public class Career {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Url { get; private set; }
        public int DurationInMinutes { get; private set; }
        public bool Active { get; private set; } 
        public bool Featured { get; private set; }
        public string Tags { get; private set; }

    private readonly List<CareerItem> _items;
    public Career(Guid id, string title, string summary, string url, int durationInMinutes, bool active, bool featured, string tags)
    {
        Id = id;
        Title = title;
        Summary = summary;
        Url = url;
        DurationInMinutes = durationInMinutes;
        Active = active;
        Featured = featured;
        Tags = tags;
        _items = new List<CareerItem>();
    }

    public Career(string title, string summary, string url, string tags, bool featured)
    {
        Id = Guid.NewGuid();
        Title = title;
        Summary = summary;
        Url = url;
        Tags = tags;
        Featured = featured;
        Active = true;
        DurationInMinutes = 0;
        _items = new List<CareerItem>();
    }

    public IReadOnlyCollection<CareerItem> Items => _items;

    public void AddItem(CareerItem item) => _items.Add(item);

    public void Deactivate() => Active = false;
}

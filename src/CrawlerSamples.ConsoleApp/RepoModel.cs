using System.Text.Json.Serialization;

namespace CrawlerSamples
{
    internal readonly record struct RepoModel
    {
        public string? Url { get; init; }

        public string? Visibility { get; init; }

        public string? Title { get; init; }

        public string? Description { get; init; }

        public string? Language { get; init; }

        public string? License { get; init; }
    }

    [JsonSerializable(typeof(RepoModel))]
    [JsonSerializable(typeof(RepoModel[]))]
    internal partial class CustomJsonSerializerContext : JsonSerializerContext
    {

    }
}

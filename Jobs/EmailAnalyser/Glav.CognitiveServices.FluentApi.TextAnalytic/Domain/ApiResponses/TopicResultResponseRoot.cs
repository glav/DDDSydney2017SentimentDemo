﻿using Glav.CognitiveServices.FluentApi.Core.Contracts;

namespace Glav.CognitiveServices.FluentApi.TextAnalytic.Domain.ApiResponses
{
    public sealed class TopicResultResponseRoot : IActionResponseRoot
    {
        public string code { get; set; }
        public string message { get; set; }
        public TopicErrorItem innerError { get; set; }
    }

    public sealed class TopicErrorItem
    {
        public string code { get; set; }
        public string message { get; set; }
        public int minimumNumberOfDocuments { get; set; }

    }
}

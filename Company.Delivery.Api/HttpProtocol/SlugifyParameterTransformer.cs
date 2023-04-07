﻿using System.Text.RegularExpressions;

namespace Company.Delivery.Api.HttpProtocol;

internal class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value) => value is null
        ? null
        : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2", RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
}
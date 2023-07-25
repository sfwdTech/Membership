using Membership.Shared.ValueObjects;

namespace Membership.Blazor.HttpMessageHandlers;
internal class ExceptionDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            string source = null;
            string message = null;
            IEnumerable<MembershipError> errors = null;
            bool isValidProblemDetails = false;

            try
            {
                var JsonResponse = JsonSerializer
                    .Deserialize<JsonElement>(errorMessage);

                if (JsonResponse.TryGetProperty("instance", out JsonElement instanceValue))
                {
                    string value = instanceValue.ToString();
                    if (value.ToLower().StartsWith("problemdetails"))
                    {
                        source = value;
                        if (JsonResponse.TryGetProperty("title", out JsonElement titleValue))
                        {
                            message = titleValue.ToString();
                        }
                        if (JsonResponse.TryGetProperty("errors", out JsonElement errorsValue))
                        {
                            errors = JsonSerializer.Deserialize<IEnumerable<MembershipError>>(errorsValue);
                        }

                        isValidProblemDetails = true;
                    }
                }
            }
            catch { }

            if (!isValidProblemDetails)
            {
                message = errorMessage;
                source = null;
                errors = null;
            }

            var ex = new HttpRequestException(message, null,
                response.StatusCode);
            ex.Source = source;
            if (errors != null)
            {
                ex.Data.Add("Errors", errors);
            }
            throw ex;
        }

        return response;
    }
}

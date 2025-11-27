using Identity.UI.Dtos;
using Syac.Common.Cache;
using Syac.Common.Http;

namespace Identity.UI.Services;

public class ProfessionalService(IHttpRestClient httpRestClient, ISyacCache cache)
{
    public async Task<ProfessionalDto> GetProfessionalDataAsync(string professionalId, CancellationToken ct) => await cache.GetOrSetAsync(
            $"ProfessionalData_{professionalId}",
            async () => await httpRestClient.GetAsync<ProfessionalDto>($"/{professionalId}", "ProfessionalApiGet", cancellationToken: ct) ??
            throw new InvalidOperationException("No se obtuvo resultado del servcio Rest"),
            TimeSpan.FromMinutes(10),
            ct);
}

using System.Net.Http.Json;
using TransmetroConecta.Auth.Application.Interfaces;

namespace TransmetroConecta.Auth.Infrastructure.Integration;

public class WalletIntegrationService(HttpClient httpClient) : IWalletIntegrationService
{
    /// <summary>
    /// Emite una petición HTTP al server-client para crear la billetera virtual del usuario con 5 viajes de cortesía.
    /// </summary>
    public async Task<bool> InitializeWalletAsync(Guid userId)
    {
        var payload = new { UserId = userId, CourtesyTrips = 5, Balance = 20 };
        var response = await httpClient.PostAsJsonAsync("/TRANSMETRO-CONECTA-CLIENTE/v1/wallets/initialize", payload);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Emite una petición HTTP al server-client para acreditar el saldo recargado a la billetera virtual.
    /// </summary>
    public async Task<bool> AddFundsAsync(Guid userId, decimal amount)
    {
        var payload = new { UserId = userId, Amount = amount };
        var response = await httpClient.PostAsJsonAsync("/TRANSMETRO-CONECTA-CLIENTE/v1/wallets/recharge", payload);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Verifica si el usuario ya cuenta con una Tarjeta Ciudadana activa.
    /// </summary>
    public async Task<bool> HasCitizenCardAsync(Guid userId)
    {
        try
        {
            var response = await httpClient.GetAsync($"/TRANSMETRO-CONECTA-CLIENTE/v1/wallets/balance?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<WalletBalanceResponse>();
                return data?.Data?.HasCitizenCard ?? false;
            }
        }
        catch
        {
            // Fallback seguro en caso de error de conexión
        }
        return false;
    }
}

// Clases internas para deserializar la respuesta del Node.js
public class WalletBalanceResponse
{
    public bool Success { get; set; }
    public WalletBalanceData Data { get; set; }
}

public class WalletBalanceData
{
    public bool HasCitizenCard { get; set; }
}
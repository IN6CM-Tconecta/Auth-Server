namespace TransmetroConecta.Auth.Application.Interfaces;

public interface IWalletIntegrationService
{
    /// <summary>
    /// Emite una petición HTTP al server-client para crear la billetera virtual del usuario con 5 viajes de cortesía.
    /// </summary>
    Task<bool> InitializeWalletAsync(Guid userId);

    /// <summary>
    /// Emite una petición HTTP al server-client para acreditar el saldo recargado a la billetera virtual.
    /// </summary>
    Task<bool> AddFundsAsync(Guid userId, decimal amount);

    /// <summary>
    /// Verifica si el usuario ya cuenta con una Tarjeta Ciudadana activa.
    /// </summary>
    Task<bool> HasCitizenCardAsync(Guid userId);
}
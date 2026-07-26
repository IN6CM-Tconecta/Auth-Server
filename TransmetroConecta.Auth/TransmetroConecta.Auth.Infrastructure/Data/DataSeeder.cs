using Microsoft.EntityFrameworkCore;
using TransmetroConecta.Auth.Domain.Entities;
using TransmetroConecta.Auth.Domain.Enums;

namespace TransmetroConecta.Auth.Infrastructure.Data;

public static class DataSeeder
{
    /// <summary>
    /// Verifica la existencia de usuarios iniciales en la base de datos y los crea con credenciales predeterminadas si no existen.
    /// </summary>
    public static async Task SeedAdminAsync(AppDbContext context)
    {
        // 1. Admin Predeterminado 1
        var admin1Exists = await context.Users.AnyAsync(u => u.CUI == "0000000000000");
        if (!admin1Exists)
        {
            await context.Users.AddAsync(new User
            {
                Id = Guid.NewGuid(),
                CUI = "0000000000000",
                Email = "admin@transmetro.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("AdminTransmetro2026!"),
                Role = Role.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        // 2. Admin Predeterminado 2 (Guía de Pruebas)
        var admin2Exists = await context.Users.AnyAsync(u => u.CUI == "1000000000001");
        if (!admin2Exists)
        {
            await context.Users.AddAsync(new User
            {
                Id = Guid.NewGuid(),
                CUI = "1000000000001",
                Email = "admin@tconecta.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = Role.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        // 3. Usuario Ciudadano Predeterminado
        var userExists = await context.Users.AnyAsync(u => u.CUI == "2000000000002");
        if (!userExists)
        {
            await context.Users.AddAsync(new User
            {
                Id = Guid.NewGuid(),
                CUI = "2000000000002",
                Email = "usuario@correo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Usuario123!"),
                Role = Role.User,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        await context.SaveChangesAsync();
    }
}
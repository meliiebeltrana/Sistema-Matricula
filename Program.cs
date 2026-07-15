using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la conexión DefaultConnection."
    );

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

//
// Creación inicial de roles y asignación de usuarios
//
using (var scope = app.Services.CreateScope())
{
    var servicios = scope.ServiceProvider;

    var roleManager =
        servicios.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        servicios.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles =
    {
        "Administrador",
        "Estudiante"
    };

    foreach (var nombreRol in roles)
    {
        if (!await roleManager.RoleExistsAsync(nombreRol))
        {
            var resultadoRol = await roleManager.CreateAsync(
                new IdentityRole(nombreRol)
            );

            if (!resultadoRol.Succeeded)
            {
                throw new InvalidOperationException(
                    $"No fue posible crear el rol {nombreRol}."
                );
            }
        }
    }

    // Este correo será reconocido como administrador.
    var correoAdministrador = "admin@universidad.com";

    var usuarioAdministrador =
        await userManager.FindByEmailAsync(correoAdministrador);

    if (usuarioAdministrador != null)
    {
        if (await userManager.IsInRoleAsync(
                usuarioAdministrador,
                "Estudiante"))
        {
            await userManager.RemoveFromRoleAsync(
                usuarioAdministrador,
                "Estudiante"
            );
        }

        if (!await userManager.IsInRoleAsync(
                usuarioAdministrador,
                "Administrador"))
        {
            await userManager.AddToRoleAsync(
                usuarioAdministrador,
                "Administrador"
            );
        }
    }

    // Los demás usuarios registrados serán estudiantes.
    var usuarios = await userManager.Users.ToListAsync();

    foreach (var usuario in usuarios)
    {
        if (string.Equals(
                usuario.Email,
                correoAdministrador,
                StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }

        var rolesUsuario =
            await userManager.GetRolesAsync(usuario);

        if (rolesUsuario.Count == 0)
        {
            await userManager.AddToRoleAsync(
                usuario,
                "Estudiante"
            );
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
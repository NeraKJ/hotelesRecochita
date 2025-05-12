using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_presentaciones
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            // Presentaciones
            services.AddScoped<IEmpleadosPresentacion, EmpleadosPresentacion>();
            services.AddScoped<IEmpleados_ServiciosExtrasPresentacion, Empleados_ServiciosExtrasPresentacion>();
            services.AddScoped<IHotelesPresentacion, HotelesPresentacion>();
            services.AddScoped<IHuespedesPresentacion, HuespedesPresentacion>();
            services.AddScoped<IReservasPresentacion, ReservasPresentacion>();
            services.AddScoped<ISedesPresentacion, SedesPresentacion>();
            services.AddScoped<ISedes_ServiciosExtrasPresentacion, Sedes_ServiciosExtrasPresentacion>();
            services.AddScoped<IFacturasPresentacion, FacturasPresentacion>();
            services.AddScoped<IServiciosExtrasPresentacion, ServiciosExtrasPresentacion>();
            services.AddScoped<IEstadiasPresentacion, EstadiasPresentacion>();
            services.AddScoped<IHabitacionesPresentacion, HabitacionesPresentacion>();
            services.AddScoped<IReservas_HabitacionesPresentacion, Reservas_HabitacionesPresentacion>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();
            app.Run();
        }
    }
}
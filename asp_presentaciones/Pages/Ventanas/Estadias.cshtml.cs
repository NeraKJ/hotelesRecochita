using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class EstadiasModel : PageModel
    {
        private readonly IEstadiasPresentacion? iPresentacion;
        private readonly IReservasPresentacion? iReservasPresentacion;

        public EstadiasModel(IEstadiasPresentacion iPresentacion, IReservasPresentacion iReservasPresentacion)
        {
            this.iPresentacion = iPresentacion;
            this.iReservasPresentacion = iReservasPresentacion;
            Filtro = new Estadias();
        }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Estadias? Actual { get; set; }
        [BindProperty] public Estadias? Filtro { get; set; }
        [BindProperty] public List<Estadias>? Lista { get; set; }
        [BindProperty] public List<Reservas>? Reservas { get; set; }

        public async Task OnGetAsync()
        {
            await OnPostBtRefrescarAsync();
        }

        public async Task OnPostBtRefrescarAsync()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;

                Lista = await iPresentacion!.PorId(Filtro!);
                Actual = null;

                await CargarCombosAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtNuevoAsync()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Estadias();
                await CargarCombosAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtModificarAsync(string id)
        {
            try
            {
                await OnPostBtRefrescarAsync();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id_Estadia.ToString() == id);
                await CargarCombosAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtGuardarAsync()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                if (Actual!.Id_Estadia == 0)
                    Actual = await iPresentacion!.Guardar(Actual!);
                else
                    Actual = await iPresentacion!.Modificar(Actual!);

                Accion = Enumerables.Ventanas.Listas;
                await OnPostBtRefrescarAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtBorrarValAsync(string id)
        {
            try
            {
                await OnPostBtRefrescarAsync();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id_Estadia.ToString() == id);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtBorrarAsync()
        {
            try
            {
                Actual = await iPresentacion!.Borrar(Actual!);
                await OnPostBtRefrescarAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtCancelarAsync()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                await OnPostBtRefrescarAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task OnPostBtCerrarAsync()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    await OnPostBtRefrescarAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        private async Task CargarCombosAsync()
        {
            try
            {
                Reservas = await iReservasPresentacion!.Listar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}

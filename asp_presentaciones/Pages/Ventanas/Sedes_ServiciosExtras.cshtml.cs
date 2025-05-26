using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class Sedes_ServiciosExtrasModel : PageModel
    {
        private readonly ISedes_ServiciosExtrasPresentacion? iPresentacion;
        private readonly ISedesPresentacion? iSedesPresentacion;
        private readonly IServiciosExtrasPresentacion? iServiciosExtrasPresentacion;

        public Sedes_ServiciosExtrasModel(
            ISedes_ServiciosExtrasPresentacion iPresentacion,
            ISedesPresentacion iSedesPresentacion,
            IServiciosExtrasPresentacion? iServiciosExtrasPresentacion)
        {
            this.iPresentacion = iPresentacion;
            this.iSedesPresentacion = iSedesPresentacion;
            this.iServiciosExtrasPresentacion = iServiciosExtrasPresentacion;

            Filtro = new Sedes_ServiciosExtras();
        }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Sedes_ServiciosExtras? Actual { get; set; }
        [BindProperty] public Sedes_ServiciosExtras? Filtro { get; set; }
        [BindProperty] public List<Sedes_ServiciosExtras>? Lista { get; set; }
        [BindProperty] public List<Sedes>? Sedes { get; set; }
        [BindProperty] public List<ServiciosExtras>? ServiciosExtras { get; set; }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;

                var task = iPresentacion!.PorId(Filtro!);
                task.Wait();
                Lista = task.Result;
                Actual = null;

                CargarCombos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                Actual = new Sedes_ServiciosExtras
                {
                    ServicioExtra = new ServiciosExtras()
                };

                CargarCombos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;

                Actual = Lista!.FirstOrDefault(x => x.Id_Sedes_ServiciosExtras.ToString() == data);

                // Asegurar que ServicioExtra no sea null
                if (Actual != null && Actual.ServicioExtra == null)
                {
                    Actual.ServicioExtra = new ServiciosExtras();
                }

                CargarCombos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                // Asegurar que ServicioExtra esté inicializado
                if (Actual!.ServicioExtra == null)
                {
                    Actual.ServicioExtra = new ServiciosExtras();
                }

                Task<Sedes_ServiciosExtras> task;
                if (Actual!.Id_Sedes_ServiciosExtras == 0)
                    task = iPresentacion!.Guardar(Actual!)!;
                else
                    task = iPresentacion!.Modificar(Actual!)!;

                task.Wait();
                Actual = task.Result;

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id_Sedes_ServiciosExtras.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                var task = iPresentacion!.Borrar(Actual!);
                task.Wait();
                Actual = task.Result;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        private void CargarCombos()
        {
            try
            {
                var task = iSedesPresentacion!.Listar();
                task.Wait();
                Sedes = task.Result;

                var task2 = iServiciosExtrasPresentacion!.Listar();
                task2.Wait();
                ServiciosExtras = task2.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}

using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ServiciosExtrasModel : PageModel
    {
        private readonly IServiciosExtrasPresentacion? iPresentacion;
        private readonly ISedesPresentacion? iSedesPresentacion;
        

        public ServiciosExtrasModel(IServiciosExtrasPresentacion iPresentacion, ISedesPresentacion iSedesPresentacion)
        {

            this.iPresentacion = iPresentacion;
            this.iSedesPresentacion = iSedesPresentacion;
			Filtro = new ServiciosExtras();

		}

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public ServiciosExtras? Actual { get; set; }
        [BindProperty] public ServiciosExtras? Filtro { get; set; }
        [BindProperty] public List<ServiciosExtras>? Lista { get; set; }
        [BindProperty] public List<Sedes>? Sedes { get; set; }

        

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Filtro!.Id_ServicioExtra = Filtro!.Id_ServicioExtra;
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
                Actual = new ServiciosExtras();
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
                Actual = Lista!.FirstOrDefault(x => x.Id_ServicioExtra.ToString() == data);
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

                Task<ServiciosExtras> task;
                if (Actual!.Id_ServicioExtra == 0)
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
                Actual = Lista!.FirstOrDefault(x => x.Id_ServicioExtra.ToString() == data);
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
                LogConversor.Log2(ex, ViewData!);
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

               
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}

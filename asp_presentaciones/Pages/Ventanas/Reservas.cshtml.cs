using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private readonly IReservasPresentacion? iPresentacion;

        private readonly ISedesPresentacion? iSedesPresentacion;

        private readonly IHuespedesPresentacion? iHuespedesPresentacion;

        public ReservasModel(IReservasPresentacion iPresentacion, IHuespedesPresentacion iHuespedesPresentacion, ISedesPresentacion iSedesPresentacion)
        {
            this.iPresentacion = iPresentacion;
            this.iHuespedesPresentacion = iHuespedesPresentacion;
            this.iSedesPresentacion = iSedesPresentacion;
            Filtro = new Reservas();
        }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Reservas? Actual { get; set; }
        [BindProperty] public Reservas? Filtro { get; set; }
        [BindProperty] public List<Reservas>? Lista { get; set; }
        [BindProperty] public List<Huespedes>? Huespedes { get; set; }
        [BindProperty] public List<Sedes>? Sedes { get; set; }


        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Filtro!.Id_Reserva = Filtro!.Id_Reserva;
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
                Actual = new Reservas();
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
                Actual = Lista!.FirstOrDefault(x => x.Id_Reserva.ToString() == data);
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

                Task<Reservas> task;
                if (Actual!.Id_Reserva == 0)
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
                Actual = Lista!.FirstOrDefault(x => x.Id_Reserva.ToString() == data);
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
                var task = iHuespedesPresentacion!.Listar();
                task.Wait();
                Huespedes = task.Result;

                var task2 = iSedesPresentacion!.Listar();
                task2.Wait();
                Sedes = task2.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}

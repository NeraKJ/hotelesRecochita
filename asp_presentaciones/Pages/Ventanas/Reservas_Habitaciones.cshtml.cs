using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class Reservas_HabitacionesModel : PageModel
    {
        private  IReservas_HabitacionesPresentacion? iPresentacion;
        private  IReservasPresentacion? iReservasPresentacion;
        private  IHabitacionesPresentacion? iHabitacionesPresentacion; 

        public Reservas_HabitacionesModel(IReservas_HabitacionesPresentacion iPresentacion, IReservasPresentacion iReservasPresentacion, IHabitacionesPresentacion iHabitacionesPresentacion)
        {
            this.iPresentacion = iPresentacion;
            this.iReservasPresentacion = iReservasPresentacion;
            this.iHabitacionesPresentacion = iHabitacionesPresentacion;
            Filtro = new Reservas_Habitaciones();
        }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Reservas_Habitaciones? Actual { get; set; }
        [BindProperty] public Reservas_Habitaciones? Filtro { get; set; }
        [BindProperty] public List<Reservas_Habitaciones>? Lista { get; set; }
        [BindProperty] public List<Reservas>? Reservas { get; set; }
        [BindProperty] public List<Habitaciones>? Habitaciones { get; set; }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Filtro!.Id_Reserva_Habitacion = Filtro!.Id_Reserva_Habitacion;
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
                Actual = new Reservas_Habitaciones();
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
                Actual = Lista!.FirstOrDefault(x => x.Id_Reserva_Habitacion.ToString() == data);
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

                Task<Reservas_Habitaciones> task;
                if (Actual!.Id_Reserva_Habitacion == 0)
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
                Actual = Lista!.FirstOrDefault(x => x.Id_Reserva_Habitacion.ToString() == data);
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
                var task = iReservasPresentacion!.Listar();
                task.Wait();
                Reservas = task.Result;

                var task2 = iHabitacionesPresentacion!.Listar();
                task2.Wait();
                Habitaciones = task2.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}

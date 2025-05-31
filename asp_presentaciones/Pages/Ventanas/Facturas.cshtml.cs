using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class FacturasModel : PageModel
    {
        private readonly IFacturasPresentacion? iPresentacion = null;
        private readonly IServiciosExtrasPresentacion? iServiciosExtrasPresentacion = null;
        private readonly IReservasPresentacion? iReservasPresentacion = null;
        private readonly IEstadiasPresentacion? iEstadiasPresentacion = null;

        public FacturasModel(IFacturasPresentacion iPresentacion, IEstadiasPresentacion iEstadiasPresentacion, IServiciosExtrasPresentacion iServiciosExtrasPresentacion, IReservasPresentacion iReservasPresentacion)
        {
            this.iPresentacion = iPresentacion;
            this.iEstadiasPresentacion = iEstadiasPresentacion;
            this.iReservasPresentacion = iReservasPresentacion;
            this.iServiciosExtrasPresentacion = iServiciosExtrasPresentacion;
            Filtro = new Facturas();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Facturas? Actual { get; set; }
        [BindProperty] public Facturas? Filtro { get; set; }
        [BindProperty] public List<Facturas>? Lista { get; set; }
        [BindProperty] public List<ServiciosExtras>? ServiciosExtras { get; set; }
        [BindProperty] public List<Reservas>? Reservas { get; set; }
        [BindProperty] public List<Estadias>? Estadias { get; set; }


        public virtual void OnGet()
        { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Filtro!.Id_Factura = Filtro!.Id_Factura;
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

        public virtual void OnPostBtNuevo()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Facturas();
                CargarCombos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id_Factura.ToString() == data);
                CargarCombos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtGuardar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                Task<Facturas>? task;
                if (Actual!.Id_Factura == 0)
                    task = this.iPresentacion!.Guardar(Actual!)!;
                else
                    task = this.iPresentacion!.Modificar(Actual!)!;
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

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id_Factura.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            try
            {
                var task = iPresentacion!.Borrar(Actual!);
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
                var task = iServiciosExtrasPresentacion!.Listar();
                task.Wait();
                ServiciosExtras = task.Result;

                var task2 = iReservasPresentacion!.Listar();
                task2.Wait();
                Reservas = task2.Result;

                var task3 = iEstadiasPresentacion!.Listar();
                task3.Wait();
                Estadias = task3.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
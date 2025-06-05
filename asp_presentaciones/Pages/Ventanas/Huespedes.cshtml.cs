using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace asp_presentacion.Pages.Ventanas
{
    public class HuespedesModel : PageModel
    {
        private readonly IHuespedesPresentacion? iPresentacion = null;

        public HuespedesModel(IHuespedesPresentacion iPresentacion)
        {
            
                this.iPresentacion = iPresentacion;
                Filtro = new Huespedes();
            
        }

        
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Huespedes? Actual { get; set; }
        [BindProperty] public Huespedes? Filtro { get; set; }
        [BindProperty] public List<Huespedes>? Lista { get; set; }


        public virtual void OnGet() 
        { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Filtro!.Id_H = Filtro!.Id_H;
                Accion = Enumerables.Ventanas.Listas;

                var task = iPresentacion!.PorId(Filtro!);
                task.Wait();
                Lista = task.Result;
                Actual = null;

                
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
                Actual = new Huespedes();
                
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
                Actual = Lista!.FirstOrDefault(x => x.Id_Huesped.ToString() == data);
                
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

                Task<Huespedes> task;
                if (Actual!.Id_H == 0)
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

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id_Huesped.ToString() == data);
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
                var task = this.iPresentacion!.Borrar(Actual!);
               
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
    }
}


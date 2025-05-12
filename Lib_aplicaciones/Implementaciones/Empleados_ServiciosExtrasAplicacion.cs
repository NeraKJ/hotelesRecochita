using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Empleados_ServiciosExtrasAplicacion : IEmpleados_ServiciosExtrasAplicacion
    {
        private IConexion? IConexion = null;

        public Empleados_ServiciosExtrasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Empleados_ServiciosExtras? Borrar(Empleados_ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Empleado_ServicioExtra == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Empleados_ServiciosExtras!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Empleados_ServiciosExtras? Guardar(Empleados_ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Empleado_ServicioExtra != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Empleados_ServiciosExtras!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Empleados_ServiciosExtras> Listar()
        {
            return this.IConexion!.Empleados_ServiciosExtras!.Take(20).ToList();
        }

        public List<Empleados_ServiciosExtras> PorId(Empleados_ServiciosExtras? entidad)
        {
            return this.IConexion!.Empleados_ServiciosExtras!
                .Where(x => x.Id_Empleado_ServicioExtra == entidad!.Id_Empleado_ServicioExtra)
                .ToList();
        }

        public Empleados_ServiciosExtras? Modificar(Empleados_ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Empleado_ServicioExtra == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Empleados_ServiciosExtras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}

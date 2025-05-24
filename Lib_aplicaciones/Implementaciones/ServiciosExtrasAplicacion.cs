using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ServiciosExtrasAplicacion : IServiciosExtrasAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public ServiciosExtrasAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {


            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public ServiciosExtras? Borrar(ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_ServicioExtra == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.ServiciosExtras!.Remove(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "ServiciosExtras",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public ServiciosExtras? Guardar(ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_ServicioExtra != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.ServiciosExtras!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "ServiciosExtras",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<ServiciosExtras> Listar()
        {
            return this.IConexion!.ServiciosExtras!.Take(20).ToList();
        }

        public List<ServiciosExtras> PorId(ServiciosExtras? entidad)
        {
            return this.IConexion!.ServiciosExtras!
                .Where(x => x.Id_ServicioExtra == entidad!.Id_ServicioExtra)
                .ToList();
        }

        public ServiciosExtras? Modificar(ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_ServicioExtra == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<ServiciosExtras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "ServiciosExtras",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}


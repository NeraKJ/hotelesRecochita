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
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public ServiciosExtras? Borrar(ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.ServiciosExtras!.FirstOrDefault(x => x.Id_ServicioExtra == entidad.Id_ServicioExtra);
            if (existente == null)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.ServiciosExtras!.Remove(existente);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
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


            var existente = this.IConexion!.ServiciosExtras!.FirstOrDefault(x => x.Id_ServicioExtra == entidad.Id_ServicioExtra);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.ServiciosExtras!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "ServiciosExtras",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<ServiciosExtras> Listar()
        {
            return this.IConexion!.ServiciosExtras!.Take(20)
                 .Include(x => x.Sedes)
              .ThenInclude(H => H!.Hotel)
             
                  .ToList(); ;
        }

        public List<ServiciosExtras> PorId(ServiciosExtras? entidad)
        {
            if (entidad == null || entidad.Id_ServicioExtra == 0)
            {
                return this.IConexion!.ServiciosExtras!.Take(20).ToList();
            }
            return this.IConexion!.ServiciosExtras!
                .Where(x => x.Id_ServicioExtra == entidad!.Id_ServicioExtra)
                .Include(x => x.Sedes)
              .ThenInclude(H => H!.Hotel)
             
                  .ToList();

        }

        public ServiciosExtras? Modificar(ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_ServicioExtra == 0)
                throw new Exception("lbNoSe ,KLGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<ServiciosExtras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "ServiciosExtras",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}
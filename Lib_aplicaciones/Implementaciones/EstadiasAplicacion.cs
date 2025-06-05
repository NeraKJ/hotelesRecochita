
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class EstadiasAplicacion : IEstadiasAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public EstadiasAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Estadias? Borrar(Estadias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Estadias!.FirstOrDefault(x => x.Id_Estadia == entidad.Id_Estadia);
            if (existente == null)
                throw new Exception("lbNoExisteRegistro");

            this.IConexion!.Estadias!.Remove(existente);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Estadias",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public Estadias? Guardar(Estadias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Estadias!.FirstOrDefault(x => x.Id_Estadia == entidad.Id_Estadia);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Estadias!.Add(entidad);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Estadias",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public List<Estadias> Listar()
        {
            return this.IConexion!.Estadias!.Take(20)                
              .Include(x => x.Reservas)
              .ThenInclude(s => s!.Huespedes)
              .ToList();
            
        }

        public List<Estadias> PorId(Estadias? entidad)
        {
            if (entidad == null || entidad.Id_Estadia == 0)
            {
                return this.IConexion!.Estadias!.Take(20).ToList();
            }

            return this.IConexion!.Estadias!
                .Where(x => x.Id_Estadia == entidad.Id_Estadia)
                .Include(x => x.Reservas)
                .ThenInclude(s => s!.Huespedes)
                .ToList();
        }

        public Estadias? Modificar(Estadias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Estadia == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Estadias>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Estadias",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}

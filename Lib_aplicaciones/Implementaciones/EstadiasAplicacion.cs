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
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Estadias? Borrar(Estadias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Estadia == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Estadias!.Remove(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Estadias",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Estadias? Guardar(Estadias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Estadia != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Estadias!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Estadias",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Estadias> Listar()
        {
            return this.IConexion!.Estadias!.Take(20).ToList();
        }

        public List<Estadias> PorId(Estadias? entidad)
        {
            return this.IConexion!.Estadias!
                .Where(x => x.Id_Estadia == entidad!.Id_Estadia)
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
                Usuario = "admin",
                Entidad = "Estadias",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}

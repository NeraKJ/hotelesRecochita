using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public RolesAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Roles? Borrar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Rol == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Roles!.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Admin",
                Entidad = "Roles",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public Roles? Guardar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Rol != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Roles!.Add(entidad);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Admin",
                Entidad = "Roles",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public List<Roles> Listar()
        {
            return this.IConexion!.Roles!.Take(20).ToList();
        }

        public List<Roles> PorNombre(Roles? entidad)
        {
            return this.IConexion!.Roles!
                .Where(x => x.Nombre!.Contains(entidad!.Nombre!))
                .ToList();
        }

        public Roles? Modificar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Rol == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Admin",
                Entidad = "Roles",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });

            return entidad;
        }
    }
}

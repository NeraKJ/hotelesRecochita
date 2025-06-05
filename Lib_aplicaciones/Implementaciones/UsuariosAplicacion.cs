using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public UsuariosAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Usuarios? Borrar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Usuarios!.Remove(entidad);
            this.IConexion.SaveChanges();

            return entidad;
        }

        public Usuarios? Guardar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Usuarios!.Add(entidad);
            this.IConexion.SaveChanges();

            return entidad;
        }

        public List<Usuarios> Listar()
        {
            return this.IConexion!.Usuarios!
                .Take(20)
                .Include(x => x.Roles)
                .ToList();
        }

        public List<Usuarios> PorEmail(Usuarios? entidad)
        {
            return this.IConexion!.Usuarios!
                .Where(x => x.Email!.Contains(entidad!.Email!))
                .Include(x => x.Roles)
                .ToList();
        }

        public Usuarios? Modificar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}

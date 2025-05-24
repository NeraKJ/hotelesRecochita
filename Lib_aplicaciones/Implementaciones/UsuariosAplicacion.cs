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
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Usuarios? Borrar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Usuario == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Usuarios!.Remove(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Usuarios",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Usuarios? Guardar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Usuario != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Usuarios!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Usuarios",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Usuarios> Listar()
        {
            return this.IConexion!.Usuarios!.Take(20).ToList();
        }

        public List<Usuarios> PorId(Usuarios? entidad)
        {
            return this.IConexion!.Usuarios!
                .Where(x => x.Id_Usuario == entidad!.Id_Usuario)
                .ToList();
        }

        public Usuarios? Modificar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Usuario == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Usuarios",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}


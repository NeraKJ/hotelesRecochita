using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class HotelesAplicacion : IHotelesAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public HotelesAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion ?? throw new ArgumentNullException(nameof(iAuditoriasAplicacion));
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        
        }

        public Hoteles? Borrar(Hoteles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Hoteles!.FirstOrDefault(x => x.Id_Hotel == entidad.Id_Hotel);
            if (entidad!.Id_Hotel == 0)
                throw new Exception("lbNoSeGuardo");

          
            this.IConexion!.Hoteles!.Remove(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Hoteles",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Hoteles? Guardar(Hoteles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Hotel != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Hoteles!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Hoteles",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Hoteles> Listar()
        {
            return this.IConexion!.Hoteles!.Take(20).ToList();
        }

        public List<Hoteles> PorId(Hoteles? entidad)
        {
            if (entidad == null || entidad.Id_Hotel == 0)
            {
                return this.IConexion!.Hoteles!.Take(20).ToList();
            }

            return this.IConexion!.Hoteles!
                .Where(x => x.Id_Hotel == entidad!.Id_Hotel)
                .ToList();
        }

        public Hoteles? Modificar(Hoteles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Hotel == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Hoteles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Hoteles",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}

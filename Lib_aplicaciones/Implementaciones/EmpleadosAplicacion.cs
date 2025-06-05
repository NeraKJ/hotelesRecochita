
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class EmpleadosAplicacion : IEmpleadosAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public EmpleadosAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Empleados? Borrar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Empleados!.FirstOrDefault(x => x.Id_E == entidad.Id_E);
            if (existente == null)
                throw new Exception("lbNoExisteRegistro");

            this.IConexion!.Empleados!.Remove(existente);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Empleados",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public Empleados? Guardar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Empleados!.FirstOrDefault(x => x.Id_E == entidad.Id_E);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Empleados!.Add(entidad);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Empleados",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public List<Empleados> Listar()
        {
            return this.IConexion!.Empleados!.Take(20).ToList();
        }

        public List<Empleados> PorId(Empleados? entidad)
        {
            if (entidad == null || entidad.Id_E == 0)
            {
                return this.IConexion!.Empleados!.Take(20).ToList();
            }

            return this.IConexion!.Empleados!
                .Where(x => x.Id_E == entidad.Id_E)
                .ToList();
        }

        public Empleados? Modificar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_E == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Empleados>(entidad);
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

using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class HabitacionesAplicacion : IHabitacionesAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public HabitacionesAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Habitaciones? Borrar(Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Habitaciones!.FirstOrDefault(x => x.Id_Habitacion == entidad.Id_Habitacion);
            if (existente == null)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Habitaciones!.Remove(existente);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Habitaciones",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Habitaciones? Guardar(Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");


            var existente = this.IConexion!.Habitaciones!.FirstOrDefault(x => x.Id_Habitacion == entidad.Id_Habitacion);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Habitaciones!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Habitaciones",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Habitaciones> Listar()
        {
            return this.IConexion!.Habitaciones!.Take(20)
                 .Include(x => x.Sedes)
              .ThenInclude(H => H!.Hotel)
              .Include(x => x.Hoteles)
                  .ToList(); ;
        }

        public List<Habitaciones> PorId(Habitaciones? entidad)
        {
            if (entidad == null || entidad.Id_Habitacion == 0)
            {
                return this.IConexion!.Habitaciones!.Take(20).ToList();
            }
            return this.IConexion!.Habitaciones!
                .Where(x => x.Id_Habitacion == entidad!.Id_Habitacion)
                .Include(x => x.Sedes)
              .ThenInclude(H => H!.Hotel)
              .Include(x => x.Hoteles)
                  .ToList();
               
        }

        public Habitaciones? Modificar(Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Habitacion == 0)
                throw new Exception("lbNoSe ,KLGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Habitaciones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Habitaciones",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}

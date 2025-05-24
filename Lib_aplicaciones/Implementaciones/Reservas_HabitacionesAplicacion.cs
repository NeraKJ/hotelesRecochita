using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Reservas_HabitacionesAplicacion : IReservas_HabitacionesAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public Reservas_HabitacionesAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {


            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Reservas_Habitaciones? Borrar(Reservas_Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Reserva_Habitacion == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Reservas_Habitaciones!.Remove(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Reservas_Habitaciones",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Reservas_Habitaciones? Guardar(Reservas_Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Reserva_Habitacion != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Reservas_Habitaciones!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Reservas_Habitaciones",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Reservas_Habitaciones> Listar()
        {
            return this.IConexion!.Reservas_Habitaciones!.Take(20).ToList();
        }

        public List<Reservas_Habitaciones> PorId(Reservas_Habitaciones? entidad)
        {
            return this.IConexion!.Reservas_Habitaciones!
                .Where(x => x.Id_Reserva_Habitacion == entidad!.Id_Reserva_Habitacion)
                .ToList();
        }

        public Reservas_Habitaciones? Modificar(Reservas_Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Reserva_Habitacion == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Reservas_Habitaciones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Reservas_Habitaciones",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}

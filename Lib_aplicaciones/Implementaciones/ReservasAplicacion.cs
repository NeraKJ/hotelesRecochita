
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public ReservasAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Reservas? Borrar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Reservas!.FirstOrDefault(x => x.Id_Reserva == entidad.Id_Reserva);
            if (existente == null)
                throw new Exception("lbNoExisteRegistro");

            this.IConexion!.Reservas!.Remove(existente);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Reservas",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public Reservas? Guardar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Reservas!.FirstOrDefault(x => x.Id_Reserva == entidad.Id_Reserva);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Reservas!.Add(entidad);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Reservas",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public List<Reservas> Listar()
        {
            return this.IConexion!.Reservas!.Take(20).
                Include(x => x.Sedes)
                .ThenInclude(d => d!.Hotel)
                .Include(x => x.Huespedes)
               
                .ToList();
        }

        public List<Reservas> PorId(Reservas? entidad)
        {
            if (entidad == null || entidad.Id_Reserva == 0)
            {
                return this.IConexion!.Reservas!.Take(20).ToList();
            }

            return this.IConexion!.Reservas!
                .Where(x => x.Id_Reserva == entidad.Id_Reserva)
                . Include(x => x.Sedes)
                .ThenInclude(d => d!.Hotel)
                .Include(x => x.Huespedes)
                .ToList();
        }

        public Reservas? Modificar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Reserva == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Reservas",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}

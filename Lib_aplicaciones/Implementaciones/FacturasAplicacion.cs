
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class FacturasAplicacion : IFacturasAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public FacturasAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }
        public Facturas? Borrar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Facturas!.FirstOrDefault(x => x.Id_Factura == entidad.Id_Factura);
            if (existente == null)
                throw new Exception("lbNoExisteRegistro");

            this.IConexion!.Facturas!.Remove(existente);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Facturas",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Facturas? Guardar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Facturas!.FirstOrDefault(x => x.Id_Factura == entidad.Id_Factura);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Facturas!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Facturas",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Facturas> Listar()
        {
            return this.IConexion!.Facturas!.Take(20)

             .Include(x => x.ServiciosExtras)
                .ThenInclude(s => s!.Sedes)
                .Include(x => x.Estadias)
              .ThenInclude(R => R!.Reservas)
                .Include(x => x.Reserva)
                 .ThenInclude(h => h!.Huespedes)
                 .Include(x => x.Reserva)
                 .ThenInclude(l => l!.Sedes)

                .ToList();
        }

        public List<Facturas> PorId(Facturas? entidad)
        {
            if (entidad == null || entidad.Id_Factura == 0)
            {
                return this.IConexion!.Facturas!.Take(20).ToList();
            }
            return this.IConexion!.Facturas!
                .Where(x => x.Id_Factura == entidad!.Id_Factura)
                .Include(x => x.ServiciosExtras)
                .ThenInclude(s => s!.Sedes)
                .Include(x => x.Estadias)
              .ThenInclude(R => R!.Reservas)
                .Include(x => x.Reserva)
                 .ThenInclude(h => h!.Huespedes)
                 .Include(x => x.Reserva)
                 .ThenInclude(l => l!.Sedes)
                .ToList();
        }

        public Facturas? Modificar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Factura == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Facturas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Facturas",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}

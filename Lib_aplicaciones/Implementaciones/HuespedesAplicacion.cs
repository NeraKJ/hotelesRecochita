
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class HuespedesAplicacion : IHuespedesAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public HuespedesAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Huespedes? Borrar(Huespedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Huespedes!.FirstOrDefault(x => x.Id_H == entidad.Id_H);
            if (existente == null)
                throw new Exception("lbNoExisteRegistro");

            this.IConexion!.Huespedes!.Remove(existente);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Huespedes",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public Huespedes? Guardar(Huespedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Huespedes!.FirstOrDefault(x => x.Id_H == entidad.Id_H);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Huespedes!.Add(entidad);
            this.IConexion.SaveChanges();

            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Huespedes",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad),
                Fecha = DateTime.Now
            });

            return entidad;
        }

        public List<Huespedes> Listar()
        {
            return this.IConexion!.Huespedes!.Take(20).ToList();
        }

        public List<Huespedes> PorId(Huespedes? entidad)
        {
            if (entidad == null || entidad.Id_H == 0)
            {
                return this.IConexion!.Huespedes!.Take(20).ToList();
            }

            return this.IConexion!.Huespedes!
                .Where(x => x.Id_H == entidad.Id_H)
                .ToList();
        }

        public Huespedes? Modificar(Huespedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_H == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Huespedes>(entidad);
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

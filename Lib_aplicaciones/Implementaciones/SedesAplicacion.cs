using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class SedesAplicacion : ISedesAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public SedesAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Sedes? Borrar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Sedes!.FirstOrDefault(x => x.Id_Sede == entidad.Id_Sede);
            if (existente == null)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Sedes!.Remove(existente);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Sedes",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Sedes? Guardar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");


            var existente = this.IConexion!.Sedes!.FirstOrDefault(x => x.Id_Sede == entidad.Id_Sede);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Sedes!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Sedes",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Sedes> Listar()
        {
            return this.IConexion!.Sedes!.Take(20)
                

                  .ToList(); ;
        }

        public List<Sedes> PorId(Sedes? entidad)
        {
            if (entidad == null || entidad.Id_Sede == 0)
            {
                return this.IConexion!.Sedes!.Take(20).ToList();
            }
            return this.IConexion!.Sedes!
                .Where(x => x.Id_Sede == entidad!.Id_Sede)
                

                  .ToList();

        }

        public Sedes? Modificar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Sede == 0)
                throw new Exception("lbNoSe ,KLGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Sedes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "admin",
                Entidad = "Sedes",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}
﻿using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Sedes_ServiciosExtrasAplicacion : ISedes_ServiciosExtrasAplicacion
    {
        private IConexion? IConexion = null;
        private IAuditoriasAplicacion? IAuditoriasAplicacion = null;

        public Sedes_ServiciosExtrasAplicacion(IConexion iConexion, IAuditoriasAplicacion iAuditoriasAplicacion)
        {
            this.IConexion = iConexion;
            this.IAuditoriasAplicacion = iAuditoriasAplicacion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Sedes_ServiciosExtras? Borrar(Sedes_ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            var existente = this.IConexion!.Sedes_ServiciosExtras!.FirstOrDefault(x => x.Id_Sedes_ServiciosExtras == entidad.Id_Sedes_ServiciosExtras);
            if (existente == null)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Sedes_ServiciosExtras!.Remove(existente);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Sedes_ServiciosExtras",
                Operacion = "Borrar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Sedes_ServiciosExtras? Guardar(Sedes_ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");


            var existente = this.IConexion!.Sedes_ServiciosExtras!.FirstOrDefault(x => x.Id_Sedes_ServiciosExtras == entidad.Id_Sedes_ServiciosExtras);
            if (existente != null)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Sedes_ServiciosExtras!.Add(entidad);
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Sedes_ServiciosExtras",
                Operacion = "Guardar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public List<Sedes_ServiciosExtras> Listar()
        {
            return this.IConexion!.Sedes_ServiciosExtras!.Take(20)
                .Include(x => x.ServicioExtra)
                  .ThenInclude(s => s!.Sedes)
                  .Include(x => x.Sedes)
                  .ThenInclude(d => d!.Hotel)

                  .ToList(); ;
        }

        public List<Sedes_ServiciosExtras> PorId(Sedes_ServiciosExtras? entidad)
        {
            if (entidad == null || entidad.Id_Sedes_ServiciosExtras == 0)
            {
                return this.IConexion!.Sedes_ServiciosExtras!.Take(20).ToList();
            }
            return this.IConexion!.Sedes_ServiciosExtras!
                .Where(x => x.Id_Sedes_ServiciosExtras == entidad!.Id_Sedes_ServiciosExtras)
                  .Include(x => x.ServicioExtra)
                  .ThenInclude(s => s!.Sedes)
                  .Include(x => x.Sedes)
                  .ThenInclude(d => d!.Hotel)
                  .ToList();

        }

        public Sedes_ServiciosExtras? Modificar(Sedes_ServiciosExtras? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Sedes_ServiciosExtras == 0)
                throw new Exception("lbNoSe ,KLGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Sedes_ServiciosExtras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            this.IAuditoriasAplicacion!.Configurar(this.IConexion.StringConexion!);
            this.IAuditoriasAplicacion!.Guardar(new Auditorias
            {
                Usuario = "Empleado",
                Entidad = "Sedes_ServiciosExtras",
                Operacion = "Modificar",
                Datos = JsonConversor.ConvertirAString(entidad!),
                Fecha = DateTime.Now
            });
            return entidad;
        }
    }
}
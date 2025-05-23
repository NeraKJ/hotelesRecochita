﻿using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using pruebas_unitarias.Nucleo;

namespace TestProject1.Repositorios
{
    [TestClass]
    public class SedesPrueba
    {
        private readonly IConexion? iConexion;
        private List<Sedes>? lista;
        private Sedes? entidad;

        public SedesPrueba()
        {
            iConexion = new Conexion();//CREAMOS UNA INSTANCIA 
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Sedes!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Sedes()!;
            this.iConexion!.Sedes!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Locacion = "Bello";

            var entry = this.iConexion!.Entry<Sedes>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Sedes!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
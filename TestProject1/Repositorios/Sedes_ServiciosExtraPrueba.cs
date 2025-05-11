using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using pruebas_unitarias.Nucleo;

namespace TestProject1.Repositorios
{
    [TestClass]
    public class Sedes_ServiciosExtrasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Sedes_ServiciosExtras>? lista;
        private Sedes_ServiciosExtras? entidad;

        public Sedes_ServiciosExtrasPrueba()
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
            this.lista = this.iConexion!.Sedes_ServiciosExtras!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Sedes_ServiciosExtras()!;
            this.iConexion!.Sedes_ServiciosExtras!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Descuento_Sede = 7;

            var entry = this.iConexion!.Entry<Sedes_ServiciosExtras>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Sedes_ServiciosExtras!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
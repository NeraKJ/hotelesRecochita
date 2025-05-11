using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using pruebas_unitarias.Nucleo;

namespace TestProject1.Repositorios
{
    [TestClass]
    public class ServiciosExtraPrueba
    {
        private readonly IConexion? iConexion;
        private List<ServiciosExtras>? lista;
        private ServiciosExtras? entidad;

        public ServiciosExtraPrueba()
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
            this.lista = this.iConexion!.ServiciosExtras!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.ServiciosExtras()!;
            this.iConexion!.ServiciosExtras!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Limpieza = "No";
            this.entidad!.Restaurante = "No";
            this.entidad!.Piscina = "No";

            var entry = this.iConexion!.Entry<ServiciosExtras>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.ServiciosExtras!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
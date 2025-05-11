using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using pruebas_unitarias.Nucleo;

namespace TestProject1.Repositorios
{
    [TestClass]
    public class Empleados_ServiciosExtrasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Empleados_ServiciosExtras>? lista;
        private Empleados_ServiciosExtras? entidad;

        public Empleados_ServiciosExtrasPrueba()
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
            this.lista = this.iConexion!.Empleados_ServiciosExtras!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Empleados_ServiciosExtras()!;
            this.iConexion!.Empleados_ServiciosExtras!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Precio_Servicio = 20000;

            var entry = this.iConexion!.Entry<Empleados_ServiciosExtras>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Empleados_ServiciosExtras!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
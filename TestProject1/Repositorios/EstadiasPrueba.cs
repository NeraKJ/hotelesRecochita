using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using pruebas_unitarias.Nucleo;

namespace TestProject1.Repositorios
{
    [TestClass]
    public class EstadiasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Estadias>? lista;
        private Estadias? entidad;

        public EstadiasPrueba()
        {
            iConexion = new Conexion();
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
            this.lista = this.iConexion!.Estadias!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Estadias()!;
            this.iConexion!.Estadias!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Fecha_Salida = this.entidad.Fecha_Salida.AddDays(10);


            var entry = this.iConexion!.Entry<Estadias>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Estadias!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
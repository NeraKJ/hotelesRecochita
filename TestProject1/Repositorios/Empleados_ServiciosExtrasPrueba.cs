using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using pruebas_unitarias.Nucleo;

namespace TestProject1.Repositorios
{
    [TestClass]
    public class AuditoriaPrueba
    {
        private readonly IConexion? iConexion;
        private List<Auditoria>? lista;
        private Auditoria? entidad;

        public AuditoriaPrueba()
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
            this.lista = this.iConexion!.Auditoria!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Auditoria()!;
            this.iConexion!.Auditoria!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Precio_Servicio = 20000;

            var entry = this.iConexion!.Entry<Auditoria>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Auditoria!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
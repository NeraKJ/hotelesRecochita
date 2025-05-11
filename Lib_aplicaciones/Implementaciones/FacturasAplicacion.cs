using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class FacturasAplicacion : IFacturasAplicacion
    {
        private IConexion? IConexion = null;

        public FacturasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Facturas? Borrar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Factura == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Facturas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Facturas? Guardar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Factura != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Facturas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Facturas> Listar()
        {
            return this.IConexion!.Facturas!.Take(20).ToList();
        }

        public List<Facturas> PorId(Facturas? entidad)
        {
            return this.IConexion!.Facturas!
                .Where(x => x.Id_Factura!.Contains(entidad!.Id_Factura!))
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
            return entidad;
        }
    }
}

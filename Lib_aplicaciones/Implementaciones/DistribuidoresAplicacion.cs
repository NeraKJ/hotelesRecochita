using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class DistribuidoresAplicacion : IDistribuidoresAplicacion
    {
        private IConexion? IConexion = null;

        public DistribuidoresAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Distribuidores? Borrar(Distribuidores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Distribuidores!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Distribuidores? Guardar(Distribuidores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Distribuidores!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Distribuidores> Listar()
        {
            return this.IConexion!.Distribuidores!.Take(20).ToList();
        }

        public List<Distribuidores> PorCodigo(Distribuidores? entidad)
        {
            return this.IConexion!.Distribuidores!
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }

        public Distribuidores? Modificar(Distribuidores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Distribuidores>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}

using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class HuespedesAplicacion : IHuespedesAplicacion
    {
        private IConexion? IConexion = null;

        public HuespedesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Huespedes? Borrar(Huespedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Huesped == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Huespedes!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Huespedes? Guardar(Huespedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id_Huesped != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Huespedes!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Huespedes> Listar()
        {
            return this.IConexion!.Huespedes!.Take(20).ToList();
        }

        public List<Huespedes> PorId(Huespedes? entidad)
        {
            return this.IConexion!.Huespedes!
                .Where(x => x.Id_Huesped!.Contains(entidad!.Id_Huesped!))
                .ToList();
        }

        public Huespedes? Modificar(Huespedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id_Huesped == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Huespedes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}

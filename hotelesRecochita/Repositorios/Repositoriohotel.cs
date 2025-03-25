using cns_presentacion.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using lib_dominio.Entidades;
using lib_repositorios.implementaciones;
using Microsoft.EntityFrameworkCore;

namespace cns_presentacion.Repositorios
{
    public class Repositoriohotel{
        private string string_conexion = "server=EINSTEIN\\KAREN;database=Recochita;Integrated Security=True;TrustServerCertificate=true;";

        public void Select()
        {
            try
            {
                Console.WriteLine("MOSTRAR DATOS HOTEL");

                using (var conexion = new Conexion(string_conexion))  // Se pasa la conexión en el constructor
                {
                    var lista = conexion.Hoteles.ToList();

                    foreach (var elemento in lista)
                    {
                        Console.WriteLine($"{elemento.Id_Hotel} | {elemento.Nombre} | {elemento.Dueños}");
                    }
                }

                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los datos del hotel: {ex.Message}");
            }
        }

        public void Insert()
        {
            try
            {
                using (var conexion = new Conexion(string_conexion))  // Se pasa la conexión en el constructor
                {
                    var entidad = new Hoteles
                    {
                        Nombre = "Recochita",
                        Dueños = "Samuel y Karen"
                    };

                    conexion.Hoteles.Add(entidad);
                    conexion.SaveChanges();

                    Console.WriteLine("Hotel insertado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar hotel: {ex.Message}");
            }
        }

        public void Update()
        {
            try
            {
                using (var conexion = new Conexion(string_conexion))  // Se pasa la conexión en el constructor
                {
                    var entidad = conexion.Hoteles.FirstOrDefault(x => x.Nombre == "Recochita");
                    if (entidad == null)
                    {
                        Console.WriteLine("No se encontró el hotel para actualizar.");
                        return;
                    }

                    entidad.Dueños = "Samuel y Karen Actualizado";

                    conexion.Entry(entidad).State = EntityState.Modified;
                    conexion.SaveChanges();

                    Console.WriteLine("Hotel actualizado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar hotel: {ex.Message}");
            }
        }

        public void Delete()
        {
            try
            {
                using (var conexion = new Conexion(string_conexion))  // Se pasa la conexión en el constructor
                {
                    var entidad = conexion.Hoteles.FirstOrDefault(x => x.Nombre == "Recochita,");

                    if (entidad == null)
                    {
                        Console.WriteLine("No se encontró el hotel para eliminar.");
                        return;
                    }

                    conexion.Hoteles.Remove(entidad);
                    conexion.SaveChanges();

                    Console.WriteLine("Hotel eliminado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar hotel: {ex.Message}");
            }
        }
    }
}

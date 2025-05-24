using lib_dominio.Entidades;
using System.Threading.Tasks.Dataflow;

namespace pruebas_unitarias.Nucleo
{
    public class EntidadesNucleo
    {


        public static Hoteles? Hoteles()
        {
            var entidad = new Hoteles();
            entidad.Nombre = "Pruebas karen";
            entidad.Dueños = "Dueña";
            return entidad;


        }

        public static Huespedes? Huespedes()
        {

            var entidad = new Huespedes();
            entidad.Id_Huesped = 1023626115;
           
            entidad.Nombre = "Pruebas-";
            entidad.Apellido = "Pruebas";
            entidad.Fecha_Naci = DateTime.Now;
            Console.WriteLine($"Edad calculada: {entidad.Edad}");
            entidad.Sexo = "M";
            entidad.Telefono = "Pruebas";
            entidad.Correo = "Pruebas";
            entidad.Historial_Reserva = "Pruebas";
            return entidad;
        }

        public static Sedes? Sedes()
        {
            var entidad = new Sedes();

            entidad.Id_Hotel = 1;
            entidad.Direccion = "Pruebas";
            entidad.Locacion = "Anori";

            return entidad;
        }

        public static Empleados? Empleados()
        {
            var entidad = new Empleados();
            entidad.Nombre = "Prueba";
            entidad.Id_Empleado = 1000;
            entidad.Apellido = "Pruebas ape";
            entidad.Fecha_Naci = DateTime.Now;
            entidad.Sexo = "M";
            entidad.Fecha_Contratacion = DateTime.Now;
            Console.WriteLine($"Edad calculada: {entidad.Edad}");
            entidad.Rol = "Recepcionista";
            entidad.Id_Hotel = 1;
            entidad.Id_Sede = 3;
            return entidad;
        }

        public static Habitaciones? Habitaciones()
        {
            var entidad = new Habitaciones();
            entidad.Id_Hotel = 1;
            entidad.Id_Sede = 1;
            entidad.Numero_Habitacion = "000";
            entidad.Precio_Habitacion = 1000;
            entidad.Camas = 10;
            entidad.Estado = "Disponible";
            entidad.Capacidad = "Preuba capacidad 2";

            return entidad;
        }

        public static Reservas? Reservas()
        {
            var entidad = new Reservas();
            entidad.Id_Sede = 1;
            entidad.Id_Huesped = 1023888288;
            entidad.Estado_Actual = "Confirmado";
            entidad.Fecha_Reserva = DateTime.Now;
            entidad.Numero_Huespedes = "30";
            return entidad;
        }

        public static Reservas_Habitaciones? Reservas_Habitaciones()
        {
            var entidad = new Reservas_Habitaciones();
            entidad.Id_Reserva = 1;
            entidad.Id_Habitacion = 1;
            entidad.Precio_Dia = 20000;
            return entidad;
        }

        public static ServiciosExtras? ServiciosExtras()
        {
            var entidad = new ServiciosExtras();
            entidad.Id_Sede = 3;
            entidad.Limpieza = "Si";
            entidad.Piscina = "Si";
            entidad.Restaurante = "Si";
            entidad.Mantenimiento = "Si";
            entidad.Gimnasio = "Si";
            entidad.Jacuzzi = "Si";

            return entidad;
        }

        public static Empleados_ServiciosExtras? Empleados_ServiciosExtras()
        {
            var entidad = new Empleados_ServiciosExtras();
            entidad.Id_empleado = 1001;
            entidad.Id_ServicioExtra = 1;
            entidad.Pago_Servicio = 250;
            entidad.Precio_Servicio = 1000;
            return entidad;
        }

        public static Sedes_ServiciosExtras? Sedes_ServiciosExtras()
        {
            var entidad = new Sedes_ServiciosExtras();
            entidad.Id_Sede = 1;
            entidad.Id_ServicioExtra = 1;
            entidad.Descuento_Sede = 5;
            return entidad;
        }

        public static Estadias? Estadias()
        {
            var entidad = new Estadias();
            entidad.Id_Reserva = 1;
            entidad.Fecha_Entrada = DateTime.Now;
            entidad.Fecha_Salida = DateTime.Now;
            return entidad;
        }

        public static Facturas? Facturas()
        {
            var entidad = new Facturas();
            entidad.Id_Estadia = 1;
            entidad.Id_Reserva = 1;
            entidad.Id_ServicioExtra = 1;
            entidad.Metodo_Pago = "Tarjeta de credito";
            entidad.Cargos_Extra = "Servicio de lavanderia";
            entidad.Reseña = "Mala";
            entidad.Total = 1000;


            return entidad;
        }

        
        public static Usuarios? Usuarios()
        {
            var entidad = new Usuarios();
            entidad.Id_Usuario = 1;
            entidad.Nombre = "Prueba";
            entidad.Contraseña = 777;
           
            return entidad;
    }
}
}
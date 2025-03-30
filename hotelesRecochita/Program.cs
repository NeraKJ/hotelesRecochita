using cns_presentacion.Repositorios;
Console.WriteLine("Proyecto consola");

var conexion = new Repositoriohotel();
conexion.Insert();
conexion.Select();

conexion.Update();
conexion.Select();

conexion.Delete();
conexion.Select();
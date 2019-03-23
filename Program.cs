using System;
using System.Collections.Generic;
using CentralTelefonica.Entidades;
using CentralTelefonica.App;
using CentralTelefonica.Util;
namespace CentralTelefonica
{
    class Program
    {

        static void Main(string[] args)
        {
            /*try
            {*/
                MenuPrincipal menu = new MenuPrincipal(); //se instancia el objeto
                menu.MostrarMenu();
            /*}
            catch (OpcionMenuException e)
            {
                Console.WriteLine(e.Message);
            }*/

            DateTime fecha = DateTime.Now;
            Console.WriteLine($"Fecha: {fecha} ");
            Console.WriteLine($"Dia: {fecha.DayOfWeek} ");
            Console.WriteLine($"Hora: {fecha.Hour}");
            Console.WriteLine($"Minutos: {fecha.Minute}");
           
        }
    }
}
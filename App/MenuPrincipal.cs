using System;
using static System.Console; //es para no estar escribiendo la palabra console
using CentralTelefonica.Entidades;
using System.Collections.Generic; // Se importan la libreria
using CentralTelefonica.Util;

namespace CentralTelefonica.App

{
    public class MenuPrincipal
    {
        //constante
        private const float precioUnoDepartamental = 0.65f; //se realiza el cast de double a float
        private const float precioDosDepartamental = 0.85f; //Una constante nunca va a cambiar de valor
        private const float precioTresDepartamental = 0.98f;
        private const float precioLocal = 1.25f;

        //Almacenar una coleccion a nivel de la clase (Listas Genericas)
        public List<Llamada> ListaDeLlamadas { get; set; }//Encapsulamiento *Cuando no utilizar este tipo de encapsulamiento cuando no se va a validar que este vacia etc

        public MenuPrincipal()
        {
            this.ListaDeLlamadas = new List<Llamada>();
        }
        public void MostrarMenu()
        {
            int opcion = 100;
            do
            {//Sentencia de control do..While
             //console.writeLine("1. Registrar llamada Local");
                try
                {
                    //Clear();
                    WriteLine("1. Registrar llamada Local");
                    WriteLine("2. Registrar llamada departamental");
                    WriteLine("3. Costo total de las llamadas locales");
                    WriteLine("4. Costo total de las llamdas departamentales");
                    WriteLine("5. Costo total de las llamdas");
                    WriteLine("6. Mostrar Resumen");
                    WriteLine("0. Salir");
                    WriteLine("Ingrese su opción===>");
                    string valor = ReadLine();
                    //opcion = Convert.ToInt16(valor);
                    //Sentencias de Control if
                    if (EsNumero(valor) == true)
                    {
                        opcion = Convert.ToInt16(valor);
                    }
                    if (opcion == 1)
                    {
                        RegistrarLlamada(opcion);
                    }
                    else if (opcion == 2)
                    {
                        RegistrarLlamada(opcion);
                    }
                    else if (opcion == 3)
                    {
                        MontrarCostoLlamadasLocales();
                    }
                    else if (opcion == 4)
                    {
                        MostrarDetalleLlamadasDepto();
                    }
                    else if (opcion == 6)
                    {
                        //DateTime fecha = new DateTime().Date;
                        MostrarDetalle(); //Cambio de ciclos
                    }
                }
                catch (OpcionMenuException e)
                {
                    WriteLine(e.Message);
                }
                /*   catch (Exception e)
                  {
                      //WriteLine($"Error:{e.Message}");
                      throw new OpcionMenuException();
                      //opcion = 100;
                  }*/
            } while (opcion != 0);
        }
        public Boolean EsNumero(string valor)
        {
            Boolean resultado = false;
            try
            {
                int numero;
                numero = Convert.ToInt16(valor);
                resultado = true;
            }
            catch (Exception e)
            {
                throw new OpcionMenuException();
            }
            return resultado;
        }


        //Metodo
        public void RegistrarLlamada(int opcion)
        { //crear parametro
          //Crear variables a nivel de la clase
            string numeroOrigen = "";
            string numeroDestino = "";
            string duracion = "";
            //string tipo = "";
            Llamada llamada = null;
            WriteLine("Ingrese el número de origen");
            numeroOrigen = ReadLine();
            WriteLine("Ingrese el número de destino");
            numeroDestino = ReadLine();
            WriteLine("Duración de la llamada");
            duracion = ReadLine();

            /*WriteLine("Tipo de Llamada: \n1. \"Local \"\n2. Depto"); //caracter salto de linea o de escape
            tipo = ReadLine();*/

            if (opcion == 1)
            {
                llamada = new LlamadaLocal(numeroOrigen, numeroDestino, Convert.ToDouble(duracion));
                ((LlamadaLocal)llamada).Precio = precioLocal;
            }
            else if (opcion == 2)
            {
                llamada = new LlamadaDepartamental(numeroOrigen, numeroDestino, Convert.ToDouble(duracion));
                ((LlamadaDepartamental)llamada).PrecioUno = precioUnoDepartamental;
                ((LlamadaDepartamental)llamada).PrecioDos = precioDosDepartamental;
                ((LlamadaDepartamental)llamada).PrecioTres = precioTresDepartamental;
                ((LlamadaDepartamental)llamada).Franja = calcularFranja(DateTime.Now); //Regla de Negocio Franja 0: L(6:00)-V(21:59)  Franja 1: L(22:00) - V(5:59) Franja 3: V(22:00) - L (5:59)

                //Aqui se va a llamar el metoto Calcular Franja

            }
            else
            {
                WriteLine("Tipo de llamada no registrada");
            }
            this.ListaDeLlamadas.Add(llamada);
        }
        /* public void MostrarDetalleWhile()
         {
             int i = 0;
             while (this.ListaDeLlamadas.Count > i)
             {
                 WriteLine(this.ListaDeLlamadas[i]);
                 i = i + 1;
                 //i+=1;
                 //i++;
             }
         }
         public void MostrarDetalleDoWhile()
         {
             int i = 0;
             do
             {
                 WriteLine(this.ListaDeLlamadas[i]);
                 i++;
             } while (this.ListaDeLlamadas.Count > i);
         }
         public void MostrarDetalleFor()
         {
             for (int i = 0; i < this.ListaDeLlamadas.Count; i++)
             {
                 WriteLine(this.ListaDeLlamadas[i]);
             }
         } */
        //public void MostrarDetalleForeach()
        public void MostrarDetalle()
        {
            foreach (var llamada in this.ListaDeLlamadas)
            {
                WriteLine(llamada);
            }
        }
        public void MontrarCostoLlamadasLocales()
        {
            double tiempoLlamada = 0;
            double costoTotal = 0.0;

            foreach (var elemento in ListaDeLlamadas)
            {
                if (elemento.GetType() == typeof(LlamadaLocal))
                {
                    tiempoLlamada += elemento.Duracion;
                    costoTotal += elemento.CalcularPrecio();
                }
            }
            WriteLine($"Costo minuto: {precioLocal}");
            WriteLine($"Tiempo total de llamada: {tiempoLlamada}");
            WriteLine($"Cosoto total: {costoTotal}");
        }

        public void MostrarDetalleLlamadasDepto()
        {
            double tiempoLlamadaFranjaUno = 0;
            double tiempoLlamadaFranjaDos = 0;
            double tiempoLlamadaFranjaTres = 0;
            double costoTotalFranjaUno = 0.0;
            double costoTotalFranjaDos = 0.0;
            double costoTotalFranjaTres = 0.0;


            foreach (var elemento in ListaDeLlamadas)
            {
                if (elemento.GetType() == typeof(LlamadaDepartamental))
                {
                    switch (((LlamadaDepartamental)elemento).Franja)
                    {
                        case 0:
                            tiempoLlamadaFranjaUno += elemento.Duracion;
                            costoTotalFranjaUno += elemento.CalcularPrecio();
                            break;
                        case 1:
                            tiempoLlamadaFranjaDos += elemento.Duracion;
                            costoTotalFranjaDos += elemento.CalcularPrecio();
                            break;
                        case 2:
                            tiempoLlamadaFranjaTres += elemento.Duracion;
                            costoTotalFranjaTres += elemento.CalcularPrecio();
                            break;
                    }
                }
            }

            WriteLine("Franja: 1");
            WriteLine($"Costo minuto: {precioUnoDepartamental}");
            WriteLine($"Tiempo total de llamada: {tiempoLlamadaFranjaUno}");
            WriteLine($"Cosoto total: {costoTotalFranjaUno}");

            WriteLine("Franja: 2");
            WriteLine($"Costo minuto: {precioDosDepartamental}");
            WriteLine($"Tiempo total de llamada: {tiempoLlamadaFranjaDos}");
            WriteLine($"Cosoto total: {costoTotalFranjaDos}");

            WriteLine("Franja: 3");
            WriteLine($"Costo minuto: {precioTresDepartamental}");
            WriteLine($"Tiempo total de llamada: {tiempoLlamadaFranjaTres}");
            WriteLine($"Cosoto total: {costoTotalFranjaTres}");
        }
        public int calcularFranja(DateTime fecha){            
            Int16 resultado = 0;
            fecha = DateTime.Now;                     
            int numeroDia =(int)fecha.DayOfWeek;
            int hora = fecha.Hour;
            int minutos = fecha.Minute;

            if (hora >= 6 && hora <= 22)  //hora entre 6 am a 10pm
            {
                if (numeroDia >= 1 && numeroDia <= 5) // de lunes a viernes
                {
                    resultado = 0;  // Franja 0           
                }
                else if (numeroDia == 6 || numeroDia == 0 ) // fin de semana incluyendo lunes
                {
                    resultado = 2; // Franja 2 
                }
            }
            else if(hora >= 22 || hora <= 6) // horario entre 22 pm horas hasta las 6 am
            { 
                if ((numeroDia >= 1 && numeroDia <= 5) & hora >=22 ) //de lunes a viernes
                { 
                    resultado = 1; // Franja 1 
                }
                else if (numeroDia >= 2 && numeroDia <= 5) // de martes a viernes
                {
                    resultado = 1; // Franja 1 
                }
                else if(numeroDia == 5 || numeroDia == 6 || numeroDia == 0 || numeroDia == 1 )
                {
                    resultado = 2; // Franja 2 
                }
                
            }

return resultado;
        }
    }
}
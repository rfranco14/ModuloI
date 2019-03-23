using System;
namespace CentralTelefonica.Util
{
    public class OpcionMenuException : Exception
    {
        private string message  = "Error, debe ingresar una opcion valida";
        public override string Message
        {
            get { return message;}
        }       
    }
}
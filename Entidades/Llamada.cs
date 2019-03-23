namespace CentralTelefonica.Entidades
{
    public abstract class Llamada
    {
        private string numeroOrigen;
        public string NumeroOrigen
        {
            get { return numeroOrigen; }
            set { numeroOrigen = value; }
        }

        private string numeroDestino;
        public string NumeroDestino
        {
            get { return numeroDestino; }
            set { numeroDestino = value; }
        }
        private double duracion;
        public double Duracion
        {
            get { return duracion; }
            set { duracion = value; }
        }
        public Llamada() {} //Constructor nulo o constructor por defecto
        public Llamada(string numeroOrigen, string numeroDestino, double duracion)
         => (NumeroOrigen, NumeroDestino, Duracion) = 
             (numeroOrigen, numeroDestino, duracion);
        //Constructor solo es una linea / Propiedades de la clase // Parametros del  Constructor

        public abstract double CalcularPrecio();
        public override string ToString(){
            return $" #Origen: {this.numeroOrigen} - #Destino: {this.numeroDestino} - Duracion: {this.duracion}";
        }
    }
}
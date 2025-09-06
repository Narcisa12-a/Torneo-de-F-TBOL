using System;
namespace TorneoFutbol
{
    class Jugador
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Posicion { get; set; }
        public Jugador(string nombre, int edad, string posicion)
        {
            Nombre = nombre;
            Edad = edad;
            Posicion = posicion;
        }
        public override string ToString()
        {
            return $"{Nombre} - {Posicion} ({Edad} años)";
        }
        //Evitar duplicados en HashSet (jugadores iguales = mismo nombre, edad y posición)
        public override bool Equals(object obj)
        {
            if (obj is Jugador jugador)
            {
                return Nombre == jugador.Nombre && Edad == jugador.Edad && Posicion == jugador.Posicion;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Edad, Posicion);
        }
    }
}
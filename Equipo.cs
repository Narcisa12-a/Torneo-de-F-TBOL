using System;
using System.Collections.Generic;
using System.Linq;
namespace TorneoFutbol
{
    class Equipo
    {
        public string Nombre { get; set; }
        public HashSet<Jugador> Jugadores { get; set; }
        public Equipo(string nombre)
        {
            Nombre = nombre;
            Jugadores = new HashSet<Jugador>();
        }
        //Agregar jugador evitando duplicados
        public bool AgregarJugador(Jugador jugador)
        {
            return Jugadores.Add(jugador);
        }
        //Mostrar jugadores en orden aleatorio
        public void MostrarJugadores()
        {
            Console.WriteLine($"\nJugadores del equipo '{Nombre}':");

            Random random = new Random();
            var listaAleatoria = Jugadores.OrderBy(j => random.Next()).ToList();

            foreach (var jugador in listaAleatoria)
            {
                Console.WriteLine(jugador);
            }
        }
    }
}
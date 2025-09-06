using System;
using System.Collections.Generic;
namespace TorneoFutbol
{
    class Program
    {
        static Dictionary<string, Equipo> equipos = new Dictionary<string, Equipo>();
        static Random random = new Random();
        static int contadorJugadores = 1; // Para nombrar jugadores como Jugador 1, Jugador 2, etc.
        static void Main(string[] args)
        {
            //=============================
            // DATOS PRECARGADOS
            //=============================
            RegistrarEquipo("Bastard Munchen");
            RegistrarEquipo("Manshine City");
            RegistrarEquipo("Blue Lock Eleven");
            RegistrarEquipo("FC Barcha");
            //Generar automáticamente 11 jugadores por equipo
            foreach (var equipo in equipos.Values)
            {
                while (equipo.Jugadores.Count < 11)
                {
                    equipo.AgregarJugador(GenerarJugadorAleatorio());
                }
            }
            //=============================
            // MENÚ INTERACTIVO
            //=============================
            int opcion;
            do
            {
                Console.WriteLine("\n--- TORNEO DE FÚTBOL ---");
                Console.WriteLine("1. Registrar equipo");
                Console.WriteLine("2. Mostrar todos los equipos");
                Console.WriteLine("3. Mostrar jugadores de un equipo");
                Console.WriteLine("4. Simular torneo completo");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor ingrese un número válido.");
                    continue;
                }
                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el nombre del nuevo equipo: ");
                        string nombreEquipo = Console.ReadLine();
                        RegistrarEquipo(nombreEquipo);
                        //Generar automáticamente 11 jugadores para el nuevo equipo
                        while (equipos[nombreEquipo].Jugadores.Count < 11)
                        {
                            equipos[nombreEquipo].AgregarJugador(GenerarJugadorAleatorio());
                        }
                        break;
                    case 2:
                        MostrarEquipos();
                        break;
                    case 3:
                        Console.Write("Ingrese el nombre del equipo: ");
                        nombreEquipo = Console.ReadLine();
                        if (equipos.ContainsKey(nombreEquipo))
                            equipos[nombreEquipo].MostrarJugadores();
                        else
                            Console.WriteLine($"El equipo '{nombreEquipo}' no existe.");
                        break;
                    case 4:
                        SimularTorneo();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

            } while (opcion != 5);
        }
        //Registrar un equipo en el diccionario
        static void RegistrarEquipo(string nombreEquipo)
        {
            if (!equipos.ContainsKey(nombreEquipo))
            {
                equipos[nombreEquipo] = new Equipo(nombreEquipo);
                Console.WriteLine($"Equipo '{nombreEquipo}' registrado con éxito.");
            }
            else
            {
                Console.WriteLine($"El equipo '{nombreEquipo}' ya existe.");
            }
        }
        //Mostrar equipos registrados
        static void MostrarEquipos()
        {
            Console.WriteLine("\nEquipos registrados:");
            foreach (var equipo in equipos.Keys)
            {
                Console.WriteLine($"- {equipo}");
            }
        }
        //Generar jugador con nombre "Jugador X"
        static Jugador GenerarJugadorAleatorio()
        {
            string[] posiciones = { "Portero", "Defensa", "Mediocampista", "Delantero" };

            string nombre = "Jugador " + contadorJugadores;
            int edad = random.Next(18, 36);
            string posicion = posiciones[random.Next(posiciones.Length)];

            contadorJugadores++;
            return new Jugador(nombre, edad, posicion);
        }
        //Simular torneo con eliminación directa
        static void SimularTorneo()
        {
            if (equipos.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos 2 equipos para un torneo.");
                return;
            }
            List<string> rondaEquipos = new List<string>(equipos.Keys);
            int ronda = 1;
            while (rondaEquipos.Count > 1)
            {
                Console.WriteLine($"\n--- RONDA {ronda} ---");
                List<string> ganadores = new List<string>();
                for (int i = 0; i < rondaEquipos.Count; i += 2)
                {
                    if (i + 1 < rondaEquipos.Count)
                    {
                        string equipo1 = rondaEquipos[i];
                        string equipo2 = rondaEquipos[i + 1];
                        int goles1 = random.Next(0, 5);
                        int goles2 = random.Next(0, 5);
                        Console.WriteLine($"{equipo1} {goles1} - {goles2} {equipo2}");

                        if (goles1 > goles2)
                        {
                            ganadores.Add(equipo1);
                        }
                        else if (goles2 > goles1)
                        {
                            ganadores.Add(equipo2);
                        }
                        else
                        {
                            Console.WriteLine("Empate, se define por penales...");
                            if (random.Next(0, 2) == 0)
                            {
                                Console.WriteLine($"Ganador por penales: {equipo1}");
                                ganadores.Add(equipo1);
                            }
                            else
                            {
                                Console.WriteLine($"Ganador por penales: {equipo2}");
                                ganadores.Add(equipo2);
                            }
                        }
                    }
                    else
                    {
                        ganadores.Add(rondaEquipos[i]);
                        Console.WriteLine($"{rondaEquipos[i]} pasa automáticamente a la siguiente ronda.");
                    }
                }
                rondaEquipos = ganadores;
                ronda++;
            }
            Console.WriteLine($"\n🏆 Campeón del Torneo: {rondaEquipos[0]} 🏆");
        }
    }
}

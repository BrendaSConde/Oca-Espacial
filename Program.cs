using System;

namespace Parcial_1
{
    class Jugador
    {
        public string nombre;
        public int casillero = 0;
        public int color = 0;
        public bool campoDeAsteroides = false;
    }
    class Program
    {
        static void PresioneEnter()
        {
            ConsoleKeyInfo tecla;
            Console.WriteLine("Por favor, presiona Enter para tirar el dado");
            do
            {
                tecla = Console.ReadKey();
            } while (tecla.Key != ConsoleKey.Enter);
        }
        static int TirarDado() 
        {
            Random dado = new Random();
            int cara = dado.Next(1, 7);
            return cara;
        }
        //Cambio de colores
        static void CambioColor(int color)
        {
            switch (color)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        static int VerificarCasillero(Jugador jugador)
        {
            // Portales y agujeros negros
            switch (jugador.casillero)
            {
                case 6:
                    Console.WriteLine($"Te encuentras en el portal del casillero {jugador.casillero} seras transportado al casillero 12");
                    jugador.casillero = 12;
                    return jugador.casillero;
                case 12:
                    Console.WriteLine($"Te encuentras en el portal del casillero {jugador.casillero} seras transportado al casillero 6");
                    jugador.casillero = 6;
                    return jugador.casillero;
                case 24:
                    Console.WriteLine($"Te encuentras en el portal del casillero {jugador.casillero} seras transportado al casillero 48");
                    jugador.casillero = 48;
                    return jugador.casillero;
                case 48:
                    Console.WriteLine($"Te encuentras en el portal del casillero {jugador.casillero} seras transportado al casillero 24");
                    jugador.casillero = 24;
                    return jugador.casillero;
               
                case 32:
                    Console.WriteLine($"Parece que caiste en el agujero negro del casillero {jugador.casillero} seras transportado al casillero inicial");
                    jugador.casillero = 0;
                    return jugador.casillero;
                case 38:
                    Console.WriteLine($"Parece que caiste en el agujero negro del casillero {jugador.casillero} seras transportado al casillero inicial");
                    jugador.casillero = 0;
                    return jugador.casillero;
                case 53:
                    Console.WriteLine($"Parece que caiste en el agujero negro del casillero {jugador.casillero} seras transportado al casillero inicial");
                    jugador.casillero = 0;
                    return jugador.casillero;
            }
            // Relays
            if (jugador.casillero == 10 || jugador.casillero == 20 || jugador.casillero == 30 || jugador.casillero == 40 || jugador.casillero == 50)
            {
                Console.WriteLine($"Buenas noticias! Encontraste el relay del casillero {jugador.casillero} puedes avanzar 10 casilleros");
                jugador.casillero += 10;
                return jugador.casillero;
            }
            // Campo de asteroides
            else if (jugador.casillero == 5 || jugador.casillero == 15 || jugador.casillero == 25 || jugador.casillero == 35 || jugador.casillero == 45 || jugador.casillero == 55)
            {
                Console.WriteLine($"Desafortunadamente, te topaste con el campo de asteroides del casillero {jugador.casillero}." +
                    $" Intenta escapar en tu próximo turno");
                jugador.campoDeAsteroides = true;
                return jugador.casillero;
            }
            
            // Casillero sin efecto
            Console.WriteLine($"Llegaste al casillero {jugador.casillero}");
            return jugador.casillero;
         
        }
        static bool CampoDeAsteroides()
        {
            Console.WriteLine("*************************");
            Console.WriteLine("*  Campo de asteroides  *");
            Console.WriteLine("*************************");
            PresioneEnter();
            int maniobras = TirarDado();
            Console.WriteLine($"Para escapar, tendras que lanzar el dado {maniobras} veces. " +
                $"Si la suma de los valores obtenidos es divisible por la cantidad de maniobras realizadas" +
                $" (cantidad de veces que lanzaste el dado), podrás escapar!\n");
            int suma = 0;
            for (int i = 0; i < maniobras; i++)
            {
                PresioneEnter();
                int valorDado = TirarDado();
                Console.WriteLine($"Sacaste {valorDado}");
                suma += valorDado;
            }
            Console.WriteLine($"La suma dio {suma}");
            if (suma % maniobras == 0)
            {
                Console.WriteLine($"\nBien hecho! Lograste escapar, vamos a seguir jugando\n");
                return true;
            }
            Console.WriteLine($"Mala suerte! Parece que nuestros esfuerzos no bastaron, intenta de nuevo en tu proximo turno");
            return false;
        }
        static void Jugar(Jugador jugador)
        {
            int cara;
            CambioColor(jugador.color);
            Console.WriteLine($"{jugador.nombre} es tu turno, que la fuerza te acompañe");
            
            if (jugador.campoDeAsteroides)
            {
                bool salioDelCampo = CampoDeAsteroides();
               
                //determino que hacer cuando sale del campo de asteroides o en caso de que no logre salir

                if ((salioDelCampo))
                {
                    jugador.campoDeAsteroides = false;
                }
                else
                {
                    return;
                }
            }

            PresioneEnter();
            cara = TirarDado();
            Console.WriteLine($"Sacaste {cara}");
            jugador.casillero += cara;
            if (jugador.casillero > 64)
            {
                // Hacemos que el jugador se quede en el lugar si se pasa de 64
               
                jugador.casillero -= cara;
                Console.WriteLine($"Lo siento, {jugador.nombre} pero para ganar debes llegar con el numero exacto...No puedes avanzar esta vez\n");
            }
            else if (jugador.casillero == 64)
            {
                Console.WriteLine($"*****{jugador.nombre}, eres el ganador del juego!!! Muchas felicidades!******");
                
                return;
            }
            else
            {
                jugador.casillero=VerificarCasillero(jugador);
            }
        }

        static void Main(string[] args)
        {
            Jugador jugadorUno = new Jugador();
            Jugador jugadorDos = new Jugador();
            bool iniciar = true;
            
            while (iniciar) 
            { 
                //Ingreso de datos jugador 1

                Console.WriteLine("Hora de jugar al juego de La OCA Espacial!\n" +
                    "Por, favor, ingresen sus datos. Muchas gracias!");
                Console.Write("Jugador 1 ingrese su nombre: ");
                jugadorUno.nombre = Console.ReadLine();
                while (jugadorUno.nombre == "")
                {
                    Console.Write("Si no ingresas un nombre no sabremos a quien dirigirnos, por favor, ingresa tu nombre: ");
                    jugadorUno.nombre = Console.ReadLine();
                }
                Console.WriteLine($"Genial, {jugadorUno.nombre}. Ahora elegi un color");
                CambioColor(1);
                Console.WriteLine("1 - Rojo");
                CambioColor(2);
                Console.WriteLine("2 - Magenta");
                CambioColor(3);
                Console.WriteLine("3 - Verde");
                CambioColor(4);
                Console.WriteLine("4 - Amarillo");
                CambioColor(5);
                Console.WriteLine("5 - Cian");
                CambioColor(0);
                Console.Write("Ingresa el número del color elegido: ");
                string colorTemp = Console.ReadLine();

                bool esNumero = false;
                bool dentroDeRango = false;

                while (!(esNumero && dentroDeRango))
                {
                    if (int.TryParse(colorTemp, out jugadorUno.color))
                    {
                        esNumero = true;
                        if (1 <= jugadorUno.color && jugadorUno.color <= 5)
                        {
                            dentroDeRango = true;
                        }
                        else
                        {
                            Console.WriteLine("El numero debe estar dentro de las opciones! Vuelve a intentarlo");
                            Console.Write("Ingresa el número del color elegido: ");
                            colorTemp = Console.ReadLine();

                        }
                    }
                    else
                    {
                        Console.WriteLine("Debe ser un número, no una letra! Por favor, elige uno de la lista y vuelve a intentarlo");
                        Console.Write("Ingresa el número del color elegido: ");
                        colorTemp = Console.ReadLine();
                    }
                }

                CambioColor(jugadorUno.color);
                Console.WriteLine($"Muy bien {jugadorUno.nombre}, de ahora en mas, me dirigiré a ti con este color");

                //Ingreso de datos jugador 2

                CambioColor(0);

                Console.Write("Jugador 2, ingresa tu nombre: ");
                jugadorDos.nombre = Console.ReadLine();
                while (jugadorDos.nombre == "")
                {
                    Console.Write("Si no ingresas un nombre no sabremos a quien dirigirnos, por favor, ingresa tu nombre: ");
                    jugadorDos.nombre = Console.ReadLine();
                }
                Console.WriteLine($"Genial, {jugadorDos.nombre}. Ahora elegi un color");
                CambioColor(1);
                Console.WriteLine("1 - Rojo");
                CambioColor(2);
                Console.WriteLine("2 - Magenta");
                CambioColor(3);
                Console.WriteLine("3 - Verde");
                CambioColor(4);
                Console.WriteLine("4 - Amarillo");
                CambioColor(5);
                Console.WriteLine("5 - Cian");
                CambioColor(0);
                Console.Write("Ingresa el número del color elegido: ");
                colorTemp = Console.ReadLine();
            
                esNumero = false;
                dentroDeRango = false;
                bool esDistinto = false;

                while (!(esNumero && dentroDeRango && esDistinto))
                {
                    if (int.TryParse(colorTemp, out jugadorDos.color))
                    {
                        esNumero = true;
                        if (1 <= jugadorDos.color && jugadorDos.color <= 5)
                        {
                            dentroDeRango = true;
                            if (jugadorUno.color != jugadorDos.color)
                            {
                                esDistinto = true;
                            }
                            else
                            {
                                Console.WriteLine($"Lamentablemente, este color ya lo eligio {jugadorUno.nombre}! Tienes que elegir uno distinto");
                                Console.Write("Ingresa el número del color elegido: ");
                                colorTemp = Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("El numero debe estar dentro de las opciones! Vuelve a intentarlo");
                            Console.Write("Ingresa el número del color elegido: ");
                            colorTemp = Console.ReadLine();

                        }
                    }
                    else
                    {
                        Console.WriteLine("Debe ser un número, no una letra! Por favor, elige uno de la lista y vuelve a intentarlo");
                        Console.Write("Ingresa el número del color elegido: ");
                        colorTemp = Console.ReadLine();
                    }
                }



                CambioColor(jugadorDos.color);
                Console.WriteLine($"Muy bien {jugadorDos.nombre}, de ahora en mas, me dirigiré a ti con este color\n");


                // Definimos quien empieza el juego

                CambioColor(0);

                Console.WriteLine($"Para saber quien comienza el juego, necesitaremos tirar el dado, si sale 3 o menos empieza {jugadorUno.nombre}, si no empieza {jugadorDos.nombre}\n");
                PresioneEnter();

                int dadoInicial = TirarDado();
                Jugador jugadorActual = null; 
                if (dadoInicial<=3) 
                {
                    jugadorActual = jugadorUno;
                }
                else
                {
                    jugadorActual = jugadorDos;
                }
                Console.WriteLine($"Salió {dadoInicial}, {jugadorActual.nombre}, es tu turno!\n");
            
                // Inicia el juego

                while (!(jugadorUno.casillero==64 || jugadorDos.casillero == 64))
                {
                    CambioColor(0);
                    Console.WriteLine("\nTabla de posiciones:\n");
                    CambioColor(jugadorUno.color);
                    Console.WriteLine($"{jugadorUno.nombre} esta en la casilla {jugadorUno.casillero}");
                    CambioColor(jugadorDos.color);
                    Console.WriteLine($"{jugadorDos.nombre} esta en la casilla {jugadorDos.casillero}\n");

                    Jugar(jugadorActual);
                    if (jugadorActual == jugadorUno)
                    {
                        jugadorActual = jugadorDos;
                    }
                    else 
                    {
                        jugadorActual = jugadorUno;
                    }
                }

                // jugar nuevamente o salir

                jugadorUno.casillero = 0;
                jugadorDos.casillero = 0;
                CambioColor(0);
                Console.WriteLine("\nQuerés volver a jugar?\n\n" +
                    "Para jugar nuevamente ingrese 1\n" +
                    "Para salir presione cualquier tecla\n");
                int.TryParse(Console.ReadLine(), out int playAgain);

                if (playAgain != 1)
                {
                    iniciar = false;
                    Console.WriteLine("Gracias por jugar el juego de La Oca Espacial!");
                }
            
            }
        }
    }
}
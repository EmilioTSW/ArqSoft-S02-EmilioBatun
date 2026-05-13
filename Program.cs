using Ahorcado;
using Ahorcado_Emilio;

Console.WriteLine("¿Qué juego quieres jugar?");
Console.WriteLine("  1 — Ahorcado");
Console.WriteLine("  2 — Viborita");
Console.Write("Opción: ");

var opcion = Console.ReadLine();

if (opcion == "1")
{
    var repositorio = new PalabrasEnMemoria();
    var motor = new MotorAhorcado(repositorio);
    var ui = new ConsolaUI(motor);

    Console.WriteLine("=== AHORCADO ===");

    while (!motor.Ganado() && !motor.Perdido())
    {
        ui.MostrarTablero();

        char letra = ui.PedirLetra();

        if (motor.LetraYaUsada(letra))
        {
            ui.MostrarMensaje("Ya usaste esa letra.");
            continue;
        }

        motor.RegistrarLetra(letra);
    }

    ui.MostrarTablero();

    if (motor.Ganado())
    { //hola
        ui.MostrarMensaje($"\n¡Ganaste! La palabra era: {motor.PalabraSecreta}");
    }
    else
    {
        ui.MostrarMensaje($"\nPerdiste. La palabra era: {motor.PalabraSecreta}");
    }

    Console.WriteLine("\nPresiona cualquier tecla para salir...");
    Console.ReadKey();
}
else if (opcion == "2")
{
    var motor = new IMotorViborita();
    var ui = new ConsolaUIViborita(motor);

    Console.CursorVisible = false;

    while (!motor.Ganado() && !motor.Perdido())
    {
        Console.Clear();

        ui.MostrarTablero();

        if (Console.KeyAvailable)
        {
            var tecla = Console.ReadKey(true).Key;

            if (tecla == ConsoleKey.Q)
                break;

            motor.CambiarDireccion(tecla);
        }

        motor.Avanzar();

        Thread.Sleep(150);
    }

    Console.Clear();

    ui.MostrarTablero();

    if (motor.Ganado())
    {
        ui.MostrarMensaje("\n¡Ganaste! Llegaste a 10 puntos.");
    }
    else
    {
        ui.MostrarMensaje("\nGame Over.");
    }

    Console.CursorVisible = true;

    Console.WriteLine("\nPresiona cualquier tecla para salir...");
    Console.ReadKey();
}
else
{
    Console.WriteLine("Opción no válida.");
}
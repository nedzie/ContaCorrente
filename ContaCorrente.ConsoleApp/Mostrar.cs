using System;

namespace ContaCorrente.ConsoleApp
{
    public class Mostrar
    {
        public static void Mensagem(string mensagem, ConsoleColor cor) // Static aqui pra poder usar lá nas outras classes
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
    }
}
using System;

namespace ContaCorrente.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Conta1
            ContaCorrente conta1 = new();
            conta1.saldo = 1000;
            conta1.numero = 12;
            conta1.limite = 0;
            conta1.especial = true;
            conta1.movimentacoes = new Movimentacao[10];
            #endregion

            conta1.Sacar(200);

            conta1.Sacar(100);

            conta1.Depositar(300);

            conta1.Depositar(500);

            conta1.Depositar(200);

            #region Conta2
            ContaCorrente conta2 = new();
            conta2.saldo = 300;
            conta2.numero = 13;
            conta2.limite = 0;
            conta2.especial = true;
            conta2.movimentacoes = new Movimentacao[10];
            #endregion

            conta1.TransferirPara(conta2, 400);
            
            conta1.ExibirExtrato();
        }
    }
}
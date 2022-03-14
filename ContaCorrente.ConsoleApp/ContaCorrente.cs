using System;

namespace ContaCorrente.ConsoleApp
{
    internal class ContaCorrente
    {
        #region Atributos
        public int numero;
        public decimal saldo;
        public decimal limite;
        public bool especial;
        public Movimentacao[] movimentacoes;
        #endregion
        #region Contador
        int contador = 0;
        #endregion
        #region Histórico
        public void SalvarMovimentacao(decimal valor, Movimentacao.TipoMovimentacao tipoEmissor)
        {
            Movimentacao movimentacao = new();
            movimentacao.valor = valor;
            movimentacao.tipo = tipoEmissor;
            movimentacoes[contador] = movimentacao;
            contador++;
        }

        public void SalvarTransferencia(decimal valor, Movimentacao.TipoMovimentacao tipoReceptor, ContaCorrente contaReceptora, Movimentacao.TipoMovimentacao tipoEmissor)
        {
            SalvarMovimentacao(valor, tipoEmissor);
            Movimentacao movimentacao2 = new();
            movimentacao2.valor = valor;
            movimentacao2.tipo = tipoReceptor;
            contaReceptora.movimentacoes[contaReceptora.contador] = movimentacao2;
            contaReceptora.contador++;
        }
        #endregion
        #region Ações nas contas
        public void Sacar(decimal saque)
        {
            if (ValidarSaque(saldo, limite, saque))
            {
                SalvarMovimentacao(saque, Movimentacao.TipoMovimentacao.Debito);
                saldo -= saque;
                //return saldo;
            }
            else
            {
                MensagemInformativa("Desculpe, o valor de saque informado é inválido.", Console.ForegroundColor = ConsoleColor.Red);
                //return saldo;
            }
        }
        public decimal Depositar(decimal deposito)
        {
            if (ValidarDeposito(deposito))
            {
                SalvarMovimentacao(deposito, Movimentacao.TipoMovimentacao.Credito);
                saldo += deposito;
            }
            else
                MensagemInformativa("Desculpe, o valor de depósito informado é inválido.", Console.ForegroundColor = ConsoleColor.Red);
            return saldo;
        }

        public void TransferirPara(ContaCorrente destino, decimal valorTransferido)
        {
            if (ValidarTransferencia(saldo, limite, valorTransferido))
            {
                SalvarTransferencia(valorTransferido, Movimentacao.TipoMovimentacao.Credito, destino, Movimentacao.TipoMovimentacao.Debito);
                saldo -= valorTransferido;
                destino.saldo += valorTransferido;
            }
            else
            {
                MensagemInformativa("Desculpe, o valor de transferência informado é inválido.", Console.ForegroundColor = ConsoleColor.Red);
            }
        }

        #endregion
        #region Validações
        public static bool ValidarSaque(decimal saldo, decimal limite, decimal saque)
        {
            if (saque <= saldo + limite && saque != 0 && saque > 0)
            {
                return true;
            }
            else
                return false;
        }
        public static bool ValidarDeposito(decimal deposito)
        {
            if (deposito > 0)
                return true;
            else
                return false;
        }
        public static bool ValidarTransferencia(decimal saldo, decimal limite, decimal valorTransferido)
        {
            if (valorTransferido <= saldo + limite && valorTransferido > 0 && valorTransferido != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Métodos complementares
        public static void MensagemInformativa(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
        public void ExibirExtrato()
        {
            Console.WriteLine("Número....: " + numero);
            Console.WriteLine("Saldo.....: " + saldo);
            Console.WriteLine("Limite....: " + limite);
            if (especial == true)
            {
                Console.WriteLine("É especial: sim");
            }
            else
                Console.WriteLine("É especial: não");
            for (int i = 0; i < contador; i++)
            {
                Console.Write(movimentacoes[i].tipo + " : ");
                Console.Write(movimentacoes[i].valor + "\n");
            }
        }
        #endregion
    }
}
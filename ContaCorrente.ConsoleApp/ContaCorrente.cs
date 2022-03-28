using System;

namespace ContaCorrente.ConsoleApp
{
    internal class ContaCorrente
    {
        #region Atributos
        private int numero;
        private decimal saldo;
        private decimal limite;
        private bool especial;
        private Movimentacao[] movimentacoes;

        #endregion

        #region Contador
        public int contador = 0; // Não é global, por isso não é static
        #endregion

        #region Encapsulamento
        public decimal Saldo
        {
            set { saldo = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public decimal Limite
        {
            get { return limite; }
            set { limite = value; }
        }

        public bool Especial
        {
            get { return especial; }
            set { especial = value; }
        }

        public Movimentacao[] Movimentacoes
        {
            get { return movimentacoes; }
            set { movimentacoes = value; }
        }
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
            }
            else
            {
                Mostrar.Mensagem("Desculpe, o valor de saque informado é inválido.", Console.ForegroundColor = ConsoleColor.Red); // Igual ao Console
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
                Mostrar.Mensagem("Desculpe, o valor de depósito informado é inválido.", Console.ForegroundColor = ConsoleColor.Red);
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
                Mostrar.Mensagem("Desculpe, o valor de transferência informado é inválido.", Console.ForegroundColor = ConsoleColor.Red);
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

        public void ExibirExtrato()
        {
            Console.WriteLine("Número....: " + Numero);
            Console.WriteLine("Saldo.....: " + saldo);
            Console.WriteLine("Limite....: " + Limite);
            if (Especial)
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
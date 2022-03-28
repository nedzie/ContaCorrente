namespace ContaCorrente.ConsoleApp
{
    internal class Movimentacao
    {
        public decimal valor;
        public TipoMovimentacao tipo;

        public enum TipoMovimentacao
        {
            Credito, 
            Debito
        }
    }
}
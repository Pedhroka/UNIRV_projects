namespace AcefalosBank.Models
{
    public class ContaPoupanca : ContaBancaria
    {
        public decimal TaxaRendimento { get; private set; }

        public ContaPoupanca(int numeroConta, string agencia, Cliente titular, decimal saldoInicial = 0m, decimal taxaRendimento = 0.005m)
            : base(numeroConta, agencia, titular, saldoInicial)
        {
            TaxaRendimento = taxaRendimento;
        }

        public void AplicarRendimento()
        {
            Saldo += Saldo * TaxaRendimento;
        }
    }
}

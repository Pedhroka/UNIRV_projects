namespace UNIRV_projects.Models
{
    public class ContaPoupanca : ContaBancaria
    {
        public decimal TaxaRendimento { get; private set; }

        public ContaPoupanca(string numeroConta, string titular, decimal saldoInicial = 0m, decimal taxaRendimento = 0.005m)
            : base(numeroConta, titular, saldoInicial)
        {
            TaxaRendimento = taxaRendimento;
        }

        public override bool Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            if (Saldo >= valor)
            {
                Saldo -= valor;
                return true;
            }

            Console.WriteLine("Saldo insuficiente na Conta Poupança.");
            return false;
        }

        public void AplicarRendimento()
        {
            decimal rendimento = Saldo * TaxaRendimento;
            Saldo += rendimento;
        }
    }
}
namespace UNIRV_projects.Models
{
    public class ContaCorrente : ContaBancaria
    {
        public decimal TaxaManutencao { get; private set; }

        public ContaCorrente(string numeroConta, string titular, decimal saldoInicial = 0m, decimal taxaManutencao = 5.00m)
            : base(numeroConta, titular, saldoInicial)
        {
            TaxaManutencao = taxaManutencao;
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

            Console.WriteLine("Saldo insuficiente para realizar o saque.");
            return false;
        }

        public void CobrarTaxaManutencao()
        {
            if (Saldo >= TaxaManutencao)
            {
                Saldo -= TaxaManutencao;
            }
            else
            {
                Console.WriteLine("Saldo insuficiente para cobrar a taxa de manutenção.");
            }
        }
    }
}
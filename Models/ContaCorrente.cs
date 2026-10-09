namespace AcefalosBank.Models
{
    public class ContaCorrente : ContaBancaria
    {
        public decimal TaxaManutencao { get; private set; }

        public ContaCorrente(int numeroConta, string agencia, Cliente titular, decimal saldoInicial = 0m, decimal taxaManutencao = 5.00m)
            : base(numeroConta, agencia, titular, saldoInicial)
        {
            TaxaManutencao = taxaManutencao;
        }

        public bool CobrarTaxaManutencao()
        {
            if (Saldo < TaxaManutencao)
            {
                return false;
            }

            Saldo -= TaxaManutencao;
            return true;
        }
    }
}

using System;

namespace AcefalosBank
{
    public class ContaTeste : ContaBancaria
    {
        public ContaTeste(int numeroConta, string agencia, Cliente titular, decimal saldoInicial = 0) 
            : base(numeroConta, agencia, titular, saldoInicial){}
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== INICIANDO TESTES DA CONTA BANCÁRIA ===");

            var cliente = new Cliente("Maria Silva", "123.456.789-00");
            ContaBancaria conta = new ContaTeste(1001, "0001", cliente, 500m);

            Console.WriteLine($"Titular: {conta.Titular.Nome}");
            Console.WriteLine($"Número: {conta.NumeroConta} | Agência: {conta.Agencia}");
            Console.WriteLine($"Saldo Inicial: {conta.Saldo:C}");

            Console.WriteLine("\n--- Testando Depósito ---");
            bool depositou = conta.Depositar(200m);
            Console.WriteLine($"Depositou R$ 200,00? {depositou} | Saldo Atual: {conta.Saldo:C}");

            bool depositouInvalido = conta.Depositar(-50m);
            Console.WriteLine($"Depositou R$ -50,00? {depositouInvalido} | Saldo Atual: {conta.Saldo:C}");

            Console.WriteLine("\n--- Testando Saque ---");
            bool sacou = conta.Sacar(300m);
            Console.WriteLine($"Sacou R$ 300,00? {sacou} | Saldo Atual: {conta.Saldo:C}");

            bool sacouDemais = conta.Sacar(1000m);
            Console.WriteLine($"Sacou R$ 1000,00? {sacouDemais} (esperado: False) | Saldo Atual: {conta.Saldo:C}");

            Console.WriteLine("\n=== TESTES CONCLUÍDOS COM SUCESSO ===");
        }
    }
}
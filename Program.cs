using System;

namespace AcefalosBank
{
    // Classe concreta simples apenas para conseguirmos instanciar e testar a ContaBancaria
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

            // 1. Criar o cliente e a conta
            var cliente = new Cliente("Maria Silva", "123.456.789-00");
            ContaBancaria conta = new ContaTeste(1001, "0001", cliente, 500m);

            Console.WriteLine($"Titular: {conta.Titular.Nome}");
            Console.WriteLine($"Número: {conta.NumeroConta} | Agência: {conta.Agencia}");
            Console.WriteLine($"Saldo Inicial: {conta.Saldo:C}");

            // 2. Teste de Depósito Válido
            Console.WriteLine("\n--- Testando Depósito ---");
            bool depositou = conta.Depositar(200m);
            Console.WriteLine($"Depositou R$ 200,00? {depositou} | Saldo Atual: {conta.Saldo:C}");

            // 3. Teste de Depósito Inválido (Valor negativo)
            bool depositouInvalido = conta.Depositar(-50m);
            Console.WriteLine($"Depositou R$ -50,00? {depositouInvalido} | Saldo Atual: {conta.Saldo:C}");

            // 4. Teste de Saque Válido
            Console.WriteLine("\n--- Testando Saque ---");
            bool sacou = conta.Sacar(300m);
            Console.WriteLine($"Sacou R$ 300,00? {sacou} | Saldo Atual: {conta.Saldo:C}");

            // 5. Teste de Saque Inválido (Saldo insuficiente)
            bool sacouDemais = conta.Sacar(1000m);
            Console.WriteLine($"Sacou R$ 1000,00? {sacouDemais} (esperado: False) | Saldo Atual: {conta.Saldo:C}");

            Console.WriteLine("\n=== TESTES CONCLUÍDOS COM SUCESSO ===");
        }
    }
}
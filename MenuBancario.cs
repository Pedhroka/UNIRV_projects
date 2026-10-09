using System;
using System.Globalization;
using AcefalosBank.Models;

namespace AcefalosBank
{
    public class MenuBancario
    {
        private const string AgenciaPadrao = "0001";

        private readonly Banco _banco;

        public MenuBancario(Banco banco)
        {
            _banco = banco;
        }

        public void Executar()
        {
            while (true)
            {
                ExibirMenu();

                string opcao = LerLinha("Escolha uma opção: ");

                if (opcao == null || opcao == "0")
                {
                    Console.WriteLine("Encerrando o programa...");
                    return;
                }

                switch (opcao)
                {
                    case "1": CriarConta(); break;
                    case "2": Depositar(); break;
                    case "3": Sacar(); break;
                    case "4": Transferir(); break;
                    case "5": ConsultarSaldo(); break;
                    case "6": ListarContas(); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }

                Pausar();
            }
        }

        private void ExibirMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine($"         {_banco.NomeBanco.ToUpper()}");
            Console.WriteLine("=================================");
            Console.WriteLine("1 - Criar Conta");
            Console.WriteLine("2 - Realizar Depósito");
            Console.WriteLine("3 - Realizar Saque");
            Console.WriteLine("4 - Realizar Transferência");
            Console.WriteLine("5 - Consultar Saldo");
            Console.WriteLine("6 - Listar Contas");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("=================================");
        }

        private void CriarConta()
        {
            int? numero = LerInteiro("Número da conta: ");
            if (numero == null)
            {
                Console.WriteLine("Erro: número da conta inválido.");
                return;
            }

            string nome = LerLinha("Nome do titular: ");
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Erro: o nome do titular não pode ser vazio.");
                return;
            }

            string cpf = LerLinha("CPF do titular: ");
            decimal? saldoInicial = LerDecimal("Saldo inicial: ");
            if (saldoInicial == null)
            {
                Console.WriteLine("Erro: saldo inicial inválido.");
                return;
            }

            string tipo = LerLinha("Tipo de conta (1 - Corrente, 2 - Poupança): ");
            Cliente titular = new Cliente(nome, cpf ?? string.Empty);

            ContaBancaria conta;
            try
            {
                conta = tipo == "2"
                    ? new ContaPoupanca(numero.Value, AgenciaPadrao, titular, saldoInicial.Value)
                    : new ContaCorrente(numero.Value, AgenciaPadrao, titular, saldoInicial.Value);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return;
            }

            if (!_banco.AdicionarConta(conta))
            {
                Console.WriteLine("Erro: já existe uma conta com esse número.");
                return;
            }

            Console.WriteLine($"Conta {conta.NumeroConta} criada com sucesso para {titular.Nome}.");
        }

        private void Depositar()
        {
            ContaBancaria conta = LerConta("Número da conta: ");
            if (conta == null)
            {
                return;
            }

            decimal? valor = LerDecimal("Valor do depósito: ");
            if (valor == null || !conta.Depositar(valor.Value))
            {
                Console.WriteLine("Erro: depósito não realizado. Informe um valor maior que zero.");
                return;
            }

            Console.WriteLine($"Depósito realizado. Novo saldo: {conta.Saldo:C}");
        }

        private void Sacar()
        {
            ContaBancaria conta = LerConta("Número da conta: ");
            if (conta == null)
            {
                return;
            }

            decimal? valor = LerDecimal("Valor do saque: ");
            if (valor == null || !conta.Sacar(valor.Value))
            {
                Console.WriteLine("Erro: saque não realizado. Verifique o valor e o saldo disponível.");
                return;
            }

            Console.WriteLine($"Saque realizado. Novo saldo: {conta.Saldo:C}");
        }

        private void Transferir()
        {
            int? origem = LerInteiro("Número da conta de origem: ");
            int? destino = LerInteiro("Número da conta de destino: ");
            decimal? valor = LerDecimal("Valor da transferência: ");

            if (origem == null || destino == null || valor == null)
            {
                Console.WriteLine("Erro: dados da transferência inválidos.");
                return;
            }

            if (_banco.BuscarConta(origem.Value) == null)
            {
                Console.WriteLine("Erro: conta de origem não encontrada.");
                return;
            }

            if (_banco.BuscarConta(destino.Value) == null)
            {
                Console.WriteLine("Erro: conta de destino não encontrada.");
                return;
            }

            if (!_banco.Transferir(origem.Value, destino.Value, valor.Value))
            {
                Console.WriteLine("Erro: transferência não realizada. Verifique o valor, o saldo e se as contas são diferentes.");
                return;
            }

            Console.WriteLine($"Transferência de {valor.Value:C} realizada com sucesso.");
        }

        private void ConsultarSaldo()
        {
            ContaBancaria conta = LerConta("Número da conta: ");
            if (conta == null)
            {
                return;
            }

            Console.WriteLine($"Conta: {conta.NumeroConta} | Agência: {conta.Agencia} | Tipo: {DescreverTipo(conta)}");
            Console.WriteLine($"Titular: {conta.Titular.Nome}");
            Console.WriteLine($"Saldo: {conta.Saldo:C}");
        }

        private void ListarContas()
        {
            if (_banco.Contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            Console.WriteLine($"{"Conta",-10} {"Titular",-30} {"Tipo",-10} {"Saldo",15}");
            foreach (ContaBancaria conta in _banco.Contas)
            {
                Console.WriteLine($"{conta.NumeroConta,-10} {conta.Titular.Nome,-30} {DescreverTipo(conta),-10} {conta.Saldo,15:C}");
            }
        }

        private ContaBancaria LerConta(string mensagem)
        {
            int? numero = LerInteiro(mensagem);
            if (numero == null)
            {
                Console.WriteLine("Erro: número da conta inválido.");
                return null;
            }

            ContaBancaria conta = _banco.BuscarConta(numero.Value);
            if (conta == null)
            {
                Console.WriteLine("Erro: conta não encontrada.");
            }

            return conta;
        }

        private static string DescreverTipo(ContaBancaria conta)
        {
            return conta switch
            {
                ContaCorrente _ => "Corrente",
                ContaPoupanca _ => "Poupança",
                _ => "Outra"
            };
        }

        private static string LerLinha(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine()?.Trim();
        }

        private static int? LerInteiro(string mensagem)
        {
            return int.TryParse(LerLinha(mensagem), out int valor) ? valor : null;
        }

        private static decimal? LerDecimal(string mensagem)
        {
            string texto = LerLinha(mensagem);
            return decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal valor) ? valor : null;
        }

        private static void Pausar()
        {
            Console.Write("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }
}

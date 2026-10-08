using System;
using System.Collections.Generic;
using System.Linq;

namespace AcefalosBank
{
    public class Banco
    {
        public string NomeBanco { get; set; }
        public List<ContaBancaria> Contas { get; private set; }

        public Banco(string nomeBanco)
        {
            NomeBanco = nomeBanco;
            Contas = new List<ContaBancaria>();
        }

        public void AdicionarConta(ContaBancaria conta)
        {
            if (conta != null)
            {
                Contas.Add(conta);
            }
        }

        public ContaBancaria BuscarConta(int numero)
        {
            return Contas.FirstOrDefault(c => c.NumeroConta == numero);
        }

        public bool Transferir(int numeroOrigem, int numeroDestino, decimal valor)
        {
            ContaBancaria contaOrigem = BuscarConta(numeroOrigem);
            ContaBancaria contaDestino = BuscarConta(numeroDestino);

            if (contaOrigem == null)
            {
                Console.WriteLine("Erro: Conta de origem não encontrada no banco.");
                return false;
            }

            if (contaDestino == null)
            {
                Console.WriteLine("Erro: Conta de destino não encontrada no banco.");
                return false;
            }

            if (contaOrigem.Sacar(valor))
            {
                contaDestino.Depositar(valor);
                Console.WriteLine($"Transferência de R${valor:F2} realizada com sucesso!");
                return true;
            }
            else
            {
                Console.WriteLine("Erro: Saldo insuficiente na conta de origem ou valor inválido.");
                return false;
            }
        }
    }
}
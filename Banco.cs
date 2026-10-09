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

        public bool AdicionarConta(ContaBancaria conta)
        {
            if (conta == null || BuscarConta(conta.NumeroConta) != null)
            {
                return false;
            }

            Contas.Add(conta);
            return true;
        }

        public ContaBancaria BuscarConta(int numero)
        {
            return Contas.FirstOrDefault(c => c.NumeroConta == numero);
        }

        public bool Transferir(int numeroOrigem, int numeroDestino, decimal valor)
        {
            ContaBancaria contaOrigem = BuscarConta(numeroOrigem);
            ContaBancaria contaDestino = BuscarConta(numeroDestino);

            if (contaOrigem == null || contaDestino == null || contaOrigem == contaDestino)
            {
                return false;
            }

            if (!contaOrigem.Sacar(valor))
            {
                return false;
            }

            contaDestino.Depositar(valor);
            return true;
        }
    }
}

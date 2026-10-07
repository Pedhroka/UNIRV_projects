using System;

namespace AcefalosBank
{
    public abstract class ContaBancaria
    {
        public int NumeroConta { get; private set; }
        
        public string Agencia { get; set; }
        
        public decimal Saldo { get; protected set; }
        
        public Cliente Titular { get; set; }

        public ContaBancaria(int numeroConta, string agencia, Cliente titular, decimal saldoInicial = 0m)
        {
            if (numeroConta <= 0)
                throw new ArgumentException("O número da conta deve ser maior que zero.", nameof(numeroConta));

            if (string.IsNullOrWhiteSpace(agencia))
                throw new ArgumentException("A agência não pode ser vazia.", nameof(agencia));

            NumeroConta = numeroConta;
            Agencia = agencia;
            Titular = titular ?? throw new ArgumentNullException(nameof(titular), "O titular é obrigatório.");
            Saldo = saldoInicial >= 0 ? saldoInicial : throw new ArgumentException("O saldo inicial não pode ser negativo.");
        }

        public virtual bool Depositar(decimal valor)
        {
            if (valor <= 0)
            {
                return false;
            }

            Saldo += valor;
            return true;
        }

        public virtual bool Sacar(decimal valor)
        {
            if (valor <= 0 || valor > Saldo)
            {
                return false;
            }

            Saldo -= valor;
            return true;
        }
    }
}
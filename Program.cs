using System.Globalization;

namespace AcefalosBank
{
    class Program
    {
        static void Main()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            Banco banco = new Banco("Acefalos Bank");
            new MenuBancario(banco).Executar();
        }
    }
}

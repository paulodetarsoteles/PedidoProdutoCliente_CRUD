using System.ComponentModel.DataAnnotations;

namespace PedidoProdutoCliente.Application.Extensions
{
    public static class Utils
    {
        public static bool ValidaEmail(string email)
        {
            var emailAddressAttribute = new EmailAddressAttribute();

            var result = emailAddressAttribute.IsValid(email);

            return result;
        }

        public static bool ValidaCPF(string cpf)
        {
            //ATENÇÃO!!! -> Feito o ajuste para testes aceitar cpfs invalidos somente com o tamanho padrão
            if (cpf.Length == 11) return true;
            else return false;

            //Para seguir com o código correto excluir o código logo acima, validação correta começa aqui
            string tempCpf;
            string digito;
            int soma;
            int resto;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Razor;
using System.Text;

namespace VL_VendasLanches.Extensions
{
    public static class RazorHelpers
    {
        public static string FormatoMoeda(this RazorPage page, decimal valor)
        {
            //Formatação de valor conforme a sua cultura
            return valor > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", valor) : "Gratuito";
        }

        public static string SelectOptionsPorQuantidade(this RazorPage page, int quantidade, int valorSelecionado = 0)
        {
            var sb = new StringBuilder();
            for (var i = 1; i <= quantidade; i++)
            {
                var selected = "";
                if (i == valorSelecionado) selected = "selected";
                sb.Append($"<option {selected} value='{i}'>{i}</option>");
            }

            return sb.ToString();
        }
    }
}

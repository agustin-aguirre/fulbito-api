using fulbito_api.Sanitizers.Interfaces;
using System.Text.RegularExpressions;
using System.Web;


namespace fulbito_api.Sanitizers
{
	public class StringInputSanitizer : ISanitizer
	{
		public string Sanitize(string input)
		{
			// 1. Validar y restringir el formato
			if (!Regex.IsMatch(input, @"^[a-zA-Z0-9@._\-+!#$%&* ]*$")) throw new ArgumentException("Inputs can only contain characters a-z, A-Z, 0-9 and ._\\ -+!#$%&*");
			// 2. Eliminar caracteres peligrosos (evita xss)
			return HttpUtility.HtmlEncode(input);  // Remueve o escapa caracteres especiales (elimina <script>, ", onclick, etc).
		}
	}
}

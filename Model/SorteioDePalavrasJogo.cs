using JogoDaForca.Interfaces;
using System.Globalization;
using System.Text;

namespace JogoDaForca.Model;

public class SorteioDePalavrasJogo : ISorteioDePalavrasJogo
{
    private static readonly Random _aleatorio = new Random();
    public string Palavra { get; }
    public string Dica { get; }
    public char[] LetrasAdivinhadas { get; set; }
    private static readonly List<string> PalavrasSorteadas = new List<string>();
    public SorteioDePalavrasJogo(string palavra, string dica)
    {
        Palavra = palavra.ToUpper();
        Dica = dica;
        LetrasAdivinhadas = new string('_', Palavra.Length).ToCharArray();
    }

    public static async Task<SorteioDePalavrasJogo> CriarPalavraJogoAsync(GeraDicasJogo gerador)
    {
        int categoria = _aleatorio.Next(3);

        var (nomeCategoria, palavraArray) = categoria switch
        {
            0 => ("Fruta", BancoDePalavras.Fruta),
            1 => ("Animal", BancoDePalavras.Animal),
            2 => ("Profissão", BancoDePalavras.Profissao),
            _ => ("Fruta", BancoDePalavras.Fruta)
        };

        List<string> palavrasDisponiveis = palavraArray.Where(p => !PalavrasSorteadas.Contains(p)).ToList();

        //TODO: VERIFICAR SE É MELHOR COLOCAR EM UM MÉTODO

        if (palavrasDisponiveis.Count == 0)
        {
            //reseta palavras da categoria
            Console.WriteLine($"Resentado.....");
            PalavrasSorteadas.Clear();
            palavrasDisponiveis = palavraArray.ToList();
        }

        string palavraEscolhida = palavrasDisponiveis[_aleatorio.Next(palavrasDisponiveis.Count)];
        PalavrasSorteadas.Add(palavraEscolhida);

        string? dica = await gerador.ObterDicaAsync(palavraEscolhida);
        dica ??= $" Dica : {nomeCategoria}";

        return new SorteioDePalavrasJogo(palavraEscolhida, dica);
    }
    public bool ChutarLetra(char letra)
    {
        bool acertou = false;
        string palavrasNormalizadas = RemoverAcentos(Palavra);

        for (int i = 0; i < palavrasNormalizadas.Length; i++)
        {
            if (palavrasNormalizadas[i] == letra)
            {
                LetrasAdivinhadas[i] = Palavra[i];
                acertou = true;
            }
        }
        return acertou;
    }
    public string RemoverAcentos(string texto)
    {
        var normalizaString = texto.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var ch in normalizaString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(ch);
            }
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public string MostrarPalavra()
    {
        return string.Join("", LetrasAdivinhadas);
    }

    public bool MostrarPalavraCompleta()
    {
        return !MostrarPalavra().Contains("_");
    }


}

using JogoDaForca.Model;

namespace JogoDaForca.Interfaces;

public interface ISorteioDePalavrasJogo
{
    public bool ChutarLetra(char letra);
    public string RemoverAcentos(string texto);
    public string MostrarPalavra();
    public bool MostrarPalavraCompleta();
}

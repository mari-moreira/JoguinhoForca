namespace JogoDaForca.Interfaces;

public interface IGeraDicasJogo
{
    Task<string?> ObterDicaAsync(string palavra);
}

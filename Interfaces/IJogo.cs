namespace JogoDaForca.Interfaces;

public interface IJogo
{
    public Task IniciarJogo();
    public void ExibirCabecalho();
    public void ExibirLogo();
    public void ExibirVitoria();
    public void ExibirDerrota();
    public void ExibirResultado();

}

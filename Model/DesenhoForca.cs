using JogoDaForca.Interfaces;

namespace JogoDaForca.Model;
public class DesenhoForca : IDesenhoForca
{
    private int erros = 0;
    private const int MAX_ERROS = 6;


    private readonly string[][] estadosBoneco = {
        //Collection expression
        ["  |    ", "  |    ", "  |    ", "  |    "],
        ["  |   O", "  |    ", "  |    ", "  |    "],
        ["  |   O", "  |   |", "  |   |", "  |    "],
        ["  |   O", "  |  /|", "  |   |", "  |    "],
        ["  |   O", "  |  /|\\", "  |   |", "  |    "],
        ["  |   O", "  |  /|\\", "  |   |", "  |  / "],
        ["  |   O", "  |  /|\\", "  |   |", "  |  / \\"]
    };

    private readonly string[] partesFixas = {
        "  +---+",
        "  |   |",
        "  |",
        "======="
    };

    public void AdicionarErro()
    {
        erros++;
        if (erros > MAX_ERROS) erros = MAX_ERROS;
        Desenhar();
    }

    public void ResetarDesenho()
    {
        erros = 0;
    }
    public void Desenhar()
    {
        Console.WriteLine(partesFixas[0]);
        Console.WriteLine(partesFixas[1]);

        string[] estadoAtual = estadosBoneco[erros];
        foreach (string linha in estadoAtual)
        {
            Console.WriteLine(linha);
        }

        Console.WriteLine(partesFixas[2]);
        Console.WriteLine(partesFixas[3]);
    }

}
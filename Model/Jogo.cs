using JogoDaForca.Interfaces;

namespace JogoDaForca.Model;

public class Jogo : IJogo
{
    //  TODO: EXIBIR LETRAS JÁ USADAS 
    //TODO: COLOCAR NÚMERO DE VITÓRIAS


    private int Tentativas { get; set; } = 6;
    private SorteioDePalavrasJogo SortearPalavra { get; set; }
    private List<char> LetrasUsadas { get; } = new List<char>();
    private readonly GeraDicasJogo _geradorDicas;
    private readonly DesenhoForca bonecoForca = new();

    public Jogo(SorteioDePalavrasJogo palavraSorteio, GeraDicasJogo geradorDicas)
    {
        SortearPalavra = palavraSorteio;
        _geradorDicas = geradorDicas;
    }
    public async Task IniciarJogo()
    {
        bool jogarNovamente;
        do
        {
            ExibirCabecalho();
            while (Tentativas > 0 && !SortearPalavra.MostrarPalavraCompleta())
            {
                Console.WriteLine($"\nVocê tem {Tentativas} tentativas");
                Console.WriteLine($"Digite uma letra:");
                char letra = char.ToUpper(Console.ReadKey().KeyChar);

                if (LetrasUsadas.Contains(letra))
                {
                    Console.WriteLine($"\nVocê já digitou essa letra: {letra}");
                    continue;
                }
                LetrasUsadas.Add(letra);

                if (!SortearPalavra.ChutarLetra(letra))
                {
                    Tentativas--;
                    Console.WriteLine($"\nNão tem a letra: {letra}");
                    bonecoForca.AdicionarErro();
                }
                Console.WriteLine("\n" + SortearPalavra.MostrarPalavra());
            }

            ExibirResultado();
            jogarNovamente = JogarNovamente();
            if (jogarNovamente)
                await ResetarJogo();

        } while (jogarNovamente);
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        ExibirLogo();

        Console.WriteLine($" Dica: {SortearPalavra.Dica}\n");

        // Console.WriteLine($"Palavra sorteada : {SortearPalavra.Palavra}");

        Console.WriteLine(SortearPalavra.MostrarPalavra());
    }
    public void ExibirLogo()
    {

        Console.WriteLine(@"

░░░░░██╗░█████╗░░██████╗░░█████╗░  ██████╗░░█████╗░  ███████╗░█████╗░██████╗░░█████╗░░█████╗░
░░░░░██║██╔══██╗██╔════╝░██╔══██╗  ██╔══██╗██╔══██╗  ██╔════╝██╔══██╗██╔══██╗██╔══██╗██╔══██╗
░░░░░██║██║░░██║██║░░██╗░██║░░██║  ██║░░██║███████║  █████╗░░██║░░██║██████╔╝██║░░╚═╝███████║
██╗░░██║██║░░██║██║░░╚██╗██║░░██║  ██║░░██║██╔══██║  ██╔══╝░░██║░░██║██╔══██╗██║░░██╗██╔══██║
╚█████╔╝╚█████╔╝╚██████╔╝╚█████╔╝  ██████╔╝██║░░██║  ██║░░░░░╚█████╔╝██║░░██║╚█████╔╝██║░░██║
░╚════╝░░╚════╝░░╚═════╝░░╚════╝░  ╚═════╝░╚═╝░░╚═╝  ╚═╝░░░░░░╚════╝░╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝
");
    }

    public void ExibirVitoria()
    {

        Console.WriteLine(@"



█░█ █▀█ █▀▀ █▀▀   █░█ █▀▀ █▄░█ █▀▀ █▀▀ █░█
▀▄▀ █▄█ █▄▄ ██▄   ▀▄▀ ██▄ █░▀█ █▄▄ ██▄ █▄█
            
    ___________  
   '._==_==_=_.' 
   .-\:      /-. 
  | (|:.     |) |
   '-|:.     |-' 
     \::.    /   
      '::. .'    
        ) (      
      _.' '._    
     `""""""""""""""`              
");
    }

    public void ExibirDerrota()
    {
        Console.WriteLine(@"

█▄░█ ▄▀█ █▀█   █▀▀ █▀█ █   █▀▄ █▀▀ █▀ ▀█▀ ▄▀█   █░█ █▀▀ ▀█
█░▀█ █▀█ █▄█   █▀░ █▄█ █   █▄▀ ██▄ ▄█ ░█░ █▀█   ▀▄▀ ██▄ █▄

      ,-=-.      
     /  +  \     
     | ~~~ |     
     |R.I.P|     
\vV,,|_____|V,VV,,
");
    }

    public void ExibirResultado()
    {
        if (SortearPalavra.MostrarPalavraCompleta())
            ExibirVitoria();
        else
            ExibirDerrota();
    }

    private async Task ResetarJogo()
    {
        Tentativas = 6;
        LetrasUsadas.Clear();
        bonecoForca.ResetarDesenho();
        SortearPalavra = await SorteioDePalavrasJogo.CriarPalavraJogoAsync(_geradorDicas);
    }
    private bool JogarNovamente()
    {
        while (true)
        {
            Console.WriteLine($"Deseja jogar novamente? (S/N) ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char resposta = char.ToUpper(keyInfo.KeyChar);
            Console.WriteLine(resposta);

            if (resposta == 'S')
            {
                return true;

            }
            else if (resposta == 'N')

                return false;
            else
                Console.WriteLine("Resposta Inválida. USE S ou N");
        }
    }
}


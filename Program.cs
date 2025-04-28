using JogoDaForca.Model;

//TODO: Guardar a chave da Api em Local Seguro
var chaveApi = "SUA_API_KEY";
var geradorDicas = new GeraDicasJogo(chaveApi);

var palavraSorteada = await SorteioDePalavrasJogo.CriarPalavraJogoAsync(geradorDicas);

var jogo = new Jogo(palavraSorteada, geradorDicas);
await jogo.IniciarJogo();

using GroqApiLibrary;
using JogoDaForca.Interfaces;
using System.Text.Json.Nodes;

namespace JogoDaForca.Model;

public class GeraDicasJogo : IGeraDicasJogo
{
    private readonly GroqApiClient _client;

    public GeraDicasJogo(string chave)
    {
        _client = new GroqApiClient(chave);
    }

    //TODO: COLOCAR LOGS 
    public async Task<string?> ObterDicaAsync(string palavra)
    {
        var request = new JsonObject
        {
            ["model"] = "meta-llama/llama-4-scout-17b-16e-instruct",
            ["messages"] = new JsonArray
            {
                new JsonObject
                {
                    ["role"] = "user",
                    ["content"] = $"Gere uma dica curta, natural e sem usar a palavra 'dica', 'palavra', 'é' ou 'consiste em'. A dica deve ser para a palavra '{palavra}', como se fosse uma pista para um jogo de adivinhação. Não revele a palavra nem dê explicações."


                }
            }
        };

        var resultado = await _client.CreateChatCompletionAsync(request);
        return resultado?["choices"]?[0]?["message"]?["content"]?.ToString();
    }
}

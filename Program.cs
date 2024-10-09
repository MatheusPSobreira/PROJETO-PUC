using System.Diagnostics;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner, se necessário (ex: serviços de dependência)
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Define o endpoint para SNMP walk
app.MapGet("/snmpwalk/{versao}/{community}/{enderecoIP}", async context =>
{
    // Verifica se os valores estão presentes e não são nulos
    if (context.Request.RouteValues.TryGetValue("versao", out var versao) &&
        context.Request.RouteValues.TryGetValue("community", out var community) &&
        context.Request.RouteValues.TryGetValue("enderecoIP", out var enderecoIP))
    {
        // Cria as informações para o processo
        ProcessStartInfo process = new ProcessStartInfo
        {
            UseShellExecute = false,
            WorkingDirectory = "/bin", // Ajuste conforme necessário
            FileName = "sh",
            Arguments = $"/root/projeto/projetopuc.sh {versao} {community} {enderecoIP}",
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using (var cmd = Process.Start(process))
        {
            if (cmd == null)
            {
                await context.Response.WriteAsync("Falha ao iniciar o processo.");
                return;
            }

            // Aguarda a execução do script
            await cmd.WaitForExitAsync();

            // Lê a saída do comando
            string output = await cmd.StandardOutput.ReadToEndAsync();
            string error = await cmd.StandardError.ReadToEndAsync();

            if (!string.IsNullOrEmpty(error))
            {
                await context.Response.WriteAsync($"Erro ao executar o script: {error}");
                return;
            }

            // Caminho do arquivo de saída
            string caminhoArquivo = "/tmp/saida.txt";
            if (File.Exists(caminhoArquivo))
            {
                // Lê o arquivo e escreve a resposta
                string[] linhas = await File.ReadAllLinesAsync(caminhoArquivo);
                foreach (var linha in linhas)
                {
                    await context.Response.WriteAsync(linha + "\n");
                }
            }
            else
            {
                await context.Response.WriteAsync("O arquivo de saída não foi encontrado.");
            }
        }
    }
    else
    {
        await context.Response.WriteAsync("Parâmetros inválidos.");
    }
});

// Inicializa a aplicação
app.Run();

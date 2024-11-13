using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
 
var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
 
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
 
app.UseHttpsRedirection();
 
// Define o endpoint para SNMP walk
app.MapGet("/snmpwalk/{versao}/{community}/{enderecoIP}", async context =>
{
    context.Response.ContentType = "text/plain";
    context.Response.Headers["Content-Disposition"] = "inline";
    if (context.Request.RouteValues.TryGetValue("versao", out var versao) &&
        context.Request.RouteValues.TryGetValue("community", out var community) &&
        context.Request.RouteValues.TryGetValue("enderecoIP", out var enderecoIP))
    {
        ProcessStartInfo process = new ProcessStartInfo
        {
            UseShellExecute = false,
            WorkingDirectory = "/bin",
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
            await cmd.WaitForExitAsync();
            string output = await cmd.StandardOutput.ReadToEndAsync();
            string error = await cmd.StandardError.ReadToEndAsync();
            if (!string.IsNullOrEmpty(error))
            {
                await context.Response.WriteAsync($"Erro ao executar o script: {error}");
                return;
            }
            string caminhoArquivo = "/tmp/saida.txt";
            if (File.Exists(caminhoArquivo))
            {
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
 
app.Run();

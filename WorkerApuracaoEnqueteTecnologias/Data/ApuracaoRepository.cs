using System.Diagnostics;
using System.Text.Json;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Data.SqlClient;
using Dapper;
using MongoDB.Driver;

namespace WorkerApuracaoEnqueteTecnologias.Data;

public class ApuracaoRepository
{
    private readonly IConfiguration _configuration;
    private readonly TelemetryConfiguration _telemetryConfig;
    
    public ApuracaoRepository(IConfiguration configuration,
        TelemetryConfiguration telemetryConfig)
    {
        _configuration = configuration;
        _telemetryConfig = telemetryConfig;
    }

    public void SaveSummary()
    {
        using var sqlConnection = new SqlConnection(
            _configuration.GetConnectionString("BaseVotacao"));
        var resultados = sqlConnection.Query<Resultado>(
            "SELECT Tecnologia, COUNT(1) AS NumVotos FROM dbo.HistoricoVotacao GROUP BY Tecnologia").ToList();

        var start = DateTime.Now;
        var watch = new Stopwatch();
        watch.Start();
        var client = new MongoClient(
            _configuration.GetConnectionString("BaseApuracao"));
        var db = client.GetDatabase("DBApuracao");
        var votacao = db.GetCollection<VotacaoTecnologiaDocument>("VotacaoTecnologia");

        var document = new VotacaoTecnologiaDocument()
        {
            DataReferencia = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Resultados = resultados
        };
        votacao.InsertOne(document);
        watch.Stop();
        TelemetryClient telemetryClient = new(_telemetryConfig);
        telemetryClient.TrackDependency(
            "MongoDB", nameof(VotacaoTecnologiaDocument),
            JsonSerializer.Serialize(resultados),
            start, watch.Elapsed, true);
    }
}
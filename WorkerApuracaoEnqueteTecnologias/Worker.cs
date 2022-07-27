using WorkerApuracaoEnqueteTecnologias.Data;

namespace WorkerApuracaoEnqueteTecnologias;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly ApuracaoRepository _repository;

    public Worker(ILogger<Worker> logger,
        IConfiguration configuration,
        ApuracaoRepository repository)
    {
        _logger = logger;
        _configuration = configuration;
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Inicio de nova apuracao em: {DateTime.Now:HH:mm:ss}");
            _repository.SaveSummary();         
            _logger.LogInformation($"Nova apuracao realizada em: {DateTime.Now:HH:mm:ss}");
            await Task.Delay(Convert.ToInt32(_configuration["IntervaloExecucaoSegundos"]) * 1000, stoppingToken);
        }
    }
}
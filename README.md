# DotNet6-WorkerService-AppInsights-SqlServer-MongoDB_ApuracaoVotosTecnologias
Exemplo de apuração de votos de uma enquete sobre tecnologias em um Worker Service criado com .NET 6, utilizando ainda SQL Server + Dapper  para consulta e MongoDB para gravação dos dados (registrando inclusive as partições de origem). Inclui um Dockerfile para geração de imagens Docker em Linux, além de monitoramento via Azure Application Insights.

Aplicação que registra os votos na base do SQL Server:

**https://github.com/renatogroffe/DotNet6-WorkerService-AppInsights-Kafka-Partitions-SqlServer_VotacaoTecnologias**
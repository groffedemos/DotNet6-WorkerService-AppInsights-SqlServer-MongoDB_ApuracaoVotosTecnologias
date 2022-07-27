using MongoDB.Bson;

namespace WorkerApuracaoEnqueteTecnologias.Data;

public class VotacaoTecnologiaDocument
{
    public ObjectId _id { get; set; }
    public string? DataReferencia { get; set; }
    public List<Resultado>? Resultados { get; set;}
}
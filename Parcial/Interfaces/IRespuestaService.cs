namespace Parcial.Interfaces
{
    public interface IRespuestaService
    {
        Task CrearRespuesta(int preguntaId, string contenido);
    }
}

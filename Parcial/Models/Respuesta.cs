namespace Parcial.Models
{
    public class Respuesta
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public int PreguntaId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Pregunta Pregunta { get; set; }
    }
}
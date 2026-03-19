namespace Parcial.Models
{
    public class Pregunta
    {

        public int Id { get; set; }
        public string Enunciado { get; set; }
        public string Categoria { get; set; }
        public string Estado { get; set; } = "Sin resolver";

    }

}
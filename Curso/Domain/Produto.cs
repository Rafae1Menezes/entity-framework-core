using Curso.ValueObjects;

namespace Curso.Domain
{
    internal class Produto
    {
        public int Id { get; set; }
        public int CodigoBarras { get; set; }
        public int Descricao { get; set; }
        public int Valor { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public int Ativo { get; set; }
    }
}

using System;
using Curso.Domain;
using Curso.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Curso
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  using var db = new Data.AplicationContext();
             db.Database.Migrate(); // não recomendado

            var existe = db.Database.GetPendingMigrations().Any();
            if(existe)
            {
                Console.WriteLine("Existe migrações pendentes");
            }*/

            // InserirDados();
            // InserirDadosEmMassa();
            // ConsultarDados();
            // CadastrarPedido();
            // ConsultarPedidoCarregamentoAdiantado();
            // AtualizarDados();
            RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.AplicationContext();
            // var cliente = db.Clientes.Find(2);
            var cliente = new Cliente { Id = 3 };
            // db.Clientes.Remove(cliente);
            // db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.AplicationContext();
            // var cliente = db.Clientes.Find(1);
            // cliente.Nome = "Maria chata";

            var cliente = new Cliente
            {
                Id = 3,
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado 2",
                Telefone = "3345676789"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            // db.Entry(cliente).State = EntityState.Modified; // para atualizar tudo

            // db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.AplicationContext();
            var pedidos  = db.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.AplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.AplicationContext();
            // var consultarorSintaxe = (from c in db.Clientes where c.Id > 0 select c);
            var consultaporMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();

            foreach(var cliente in consultaporMetodo) 
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                // db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "13456",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.AplicationContext();
            // db.Produtos.Add(produto);
            // db.Set<Produto>.add(produto);
            db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total de registros: {registros}");
        }

        private static void InserirDadosEmMassa()
        {

            /* var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "13456",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            }; */

            var cliente = new Cliente
            {
                Nome = "Rafael",
                CEP = "35894000",
                Cidade = "Marrocos",
                Estado = "MG",
                Telefone = "Loucura"
            };

            using var db = new Data.AplicationContext();
            db.AddRange(cliente);
            //db.AddRange(produto, cliente);


            var registros = db.SaveChanges();
            Console.WriteLine($"Total de Registro(s) alterados: {registros}");
        }
    }
}
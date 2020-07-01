using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = contexto.Produtos.ToList();

                ExibeEntries(contexto.ChangeTracker.Entries());

                var novoProduto = new Produto()
                {
                    Nome = "Desinfetante2",
                    Categoria = "Limpeza",
                    Preco = 2.99
                };
                contexto.Produtos.Add(novoProduto);

                //var p1 = produtos.First();
                //contexto.Produtos.Remove(p1);

                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
                ExibeEntries(contexto.ChangeTracker.Entries());
                Console.ReadKey();

            }
        }
        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            using(var contexto = new LojaContext())
            {
                Console.WriteLine("-------------");
                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e.Entity.ToString() + " - " + e.State);
                }
            }
            
        }
        private static void AtualizarProduto()
        {
            // inclui um porduto
            

            //atualiza o produto
            using (var repo = new ProdutoDAOEntity())
            {
                Produto primeiro = repo.Produtos().First();
                primeiro.Nome = "Harry Potter - Atualizado";
                repo.Atualizar(primeiro);
            }
            RecuperarProdutos();
        }

        private static void ExcluirProdutos()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos();
                foreach (var item in produtos)
                {
                    repo.Remover(item);
                }
            }
        }

        private static void RecuperarProdutos()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos();
                Console.WriteLine("Foram encontrados {0} produtos", produtos.Count);
                foreach (var item in produtos)
                {
                    Console.WriteLine(item.Nome);

                }
                Console.ReadKey();
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p1 = new Produto();
            p1.Nome = "Harry Potter e a Ordem da Fênix";
            p1.Categoria = "Livros";
            p1.Preco = 19.89;

            Produto p2 = new Produto();
            p2.Nome = "Senhor dos Anéis 1";
            p2.Categoria = "Livros";
            p2.Preco = 19.89;

            Produto p3 = new Produto();
            p3.Nome = "O Monge e o Executivo";
            p3.Categoria = "Livros";
            p3.Preco = 19.89;

            using (var repo = new ProdutoDAOEntity())
            {
                repo.Adicionar(p1);

                repo.Adicionar(p2);

                repo.Adicionar(p3);
            }
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
    }
}

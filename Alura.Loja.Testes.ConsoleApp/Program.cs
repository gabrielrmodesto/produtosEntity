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
                var produtos = contexto.Produtos.ToList();
                foreach(var p in produtos)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine("-------------");
                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e);
                }
                var p1 = produtos.Last();
                p1.Nome = "Harry Potter - Prisioneiro";

                Console.WriteLine("-------------");
                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e);
                }
                Console.ReadKey();
                //Console.WriteLine("-------------");
                //produtos = contexto.Produtos.ToList();
                //foreach(var p in produtos)
                //{
                //    Console.WriteLine(p);
                //}
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

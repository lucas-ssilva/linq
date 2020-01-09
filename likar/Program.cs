using System;
using likar.Entities;
using System.Linq;
using System.Collections.Generic;

namespace likar
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach(T obj in collection)
            {
                Console.WriteLine(obj);
            }
        }

        static void Main(string[] args)
        {
            Category cat1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category cat2 = new Category() { Id = 2, Name = "IT", Tier = 1 };
            Category cat3 = new Category() { Id = 3, Name = "Games", Tier = 3 };

            List<Product> products = new List<Product>() // data source 
            {
                new Product() {Id = 2,Name = "Computer",Price = 1500.00,Category = cat2},
                new Product() {Id = 1,Name = "Toy",Price = 110.00,Category = cat1},
                new Product() {Id = 3,Name = "Persona 5",Price = 40.00,Category = cat3},
                new Product() {Id = 4,Name = "Hammer",Price = 90.00,Category = cat1},
                new Product() {Id = 5,Name = "TV",Price = 1700.00,Category = cat3},
                new Product() {Id = 6,Name = "Notebook",Price = 1300.00,Category = cat2},
                new Product() {Id = 7,Name = "Saw",Price = 80.00,Category = cat1},
                new Product() {Id = 8,Name = "Tablet",Price = 700.00,Category = cat2},
                new Product() {Id = 9,Name = "Camera",Price = 700.00,Category = cat2},
                new Product() {Id = 10,Name = "Printer",Price = 350.00,Category = cat2},
                new Product() {Id = 11,Name = "Macbook",Price = 1800.00,Category = cat2},
                new Product() {Id = 12,Name = "SoundBar",Price = 700.00,Category = cat3},
            };

            var query = products.Where(x => x.Category.Tier == 1 && x.Price < 900.00); // logica da consulta 

            /*foreach (Product p in query) // executar a consulta
            {
                Console.WriteLine(p);
            }*/

            // metodo alternativo usando função auxiliar para imprimir 
            Print("Tier 1 e valor até 900.00", query);

            Console.WriteLine();
            var query2 = products.Where(x => x.Category.Name == "Tools").Select(x => x.Name);

            Print("name of the products from tools", query2);

            Console.WriteLine();
            var query3 = products.Where(x => x.Name.StartsWith('C') || x.Name.StartsWith('c')).Select(x => new { x.Name, CategoryName = x.Category.Name, x.Price });
            Print("Começa com C e objeto anonimo ", query3);

            Console.WriteLine();
            var query4 = products.Where(x => x.Category.Tier == 1).OrderBy(x => x.Price).ThenBy(x => x.Name); // ordena por preço e em caso de empate ordena por nome 
            Print("ordenar tier 1, por preço e por nome para desempate", query4);

            Console.WriteLine();
            var query5 = query4.Skip(2).Take(4);
            Print("ordenar tier 1, por preço e por nome para desempate pula 2 pega 4 ", query5); // skip = pula os 2 primeiros, take = pega os proximos ... numeros 

            Console.WriteLine();
            var query6 = products.First();
            Console.WriteLine("Fist " + query6); // pega o primeiro item da lista   melhor usar o com o defalut pois evita exceção
            Console.WriteLine();
            var query7 = products.Where(x => x.Price > 3000.00).FirstOrDefault();
            Console.WriteLine("tentar usar fist no vazio " + query7);

            var query8 = products.Where(x => x.Id == 3).SingleOrDefault(); // só serve se o filtro já for trazer um resultado  ou nada se vir mais de 1 da exception
            Console.WriteLine("Single or default teste " + query8);

            Console.WriteLine();
            var query9 = products.Where(x => x.Id == 30).SingleOrDefault(); // teste pra não trazer resultado 
            Console.WriteLine("teeste pra não trazer nada " + query9);
            Console.WriteLine();
            var r10 = products.Max(p => p.Price); //maximo da coleção, pode ser chamado sem parametro 
            Console.WriteLine("teste max " + r10);
            Console.WriteLine();
            var r11 = products.Min(p => p.Price); //minimo da coleção, pode ser chamado sem parametro 
            Console.WriteLine("teste mmin " + r11);
            Console.WriteLine();
            var r12 = products.Where(p => p.Category.Tier == 1).Sum(p => p.Price); //somar todos baseado no primeiro filtro 
            Console.WriteLine("teste sum " + r12);
            Console.WriteLine();
            var r13 = products.Where(p => p.Category.Tier == 1).Average(p => p.Price).ToString("F2"); //media todos baseado no primeiro filtro 
            Console.WriteLine("teste media " + r13);
            Console.WriteLine();
            var r14 = products.Where(p => p.Category.Tier == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average(); // caso o filtro de vazio para evitar uma exeption usando od efault if empty, nesse caso está protegendo a função de media que da uma exption caso eu tente dividir por 0
            Console.WriteLine("teste media com defalt if empty" + r14);
            Console.WriteLine();
            var r15 = products.Where(p => p.Category.Tier == 2).Select(p => p.Price).Aggregate(0.0,(x, y) => x + y).ToString("F2"); // operação personalizada, exemplo recebe um x e y e soma ele, feito com a expressão lambda, o 0 antes do x e y serve para tratar caso o select de nulo 
            Console.WriteLine("tier 1 aggregate sum " + r15);
            Console.WriteLine();
            var r16 = products.GroupBy(p => p.Category); //seleciona uma categoria ou grupo, precisa de forech especial, no exemplo ele traz todas as categorias de produtos e depois com o segundo forech ele filtra nos produros dentro das categorias 
            foreach (IGrouping<Category,Product> group in r16)
            {
                Console.WriteLine(group.Key.Name + ": ");
                foreach(Product p in group)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}

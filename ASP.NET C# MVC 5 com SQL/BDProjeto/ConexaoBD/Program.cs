using BDProjeto.Aplicacao;
using BDProjeto.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new Usuario();
            var app = UsuarioAplicacaoConstrutor.UsuarioApADO();

            // Insert e Update
            Console.Write("Digite o nome do usuário: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o cargo do usuário: ");
            string cargo = Console.ReadLine();

            DateTime data = DateTime.Now;

            user.Nome = nome;
            user.Cargo = cargo;
            user.Data = data;
            //usuario.Id = 5;

            app.Salvar(user);

            // Select
            var dados = app.ListarTodos();

            foreach(var usuario in dados)
            {
                Console.WriteLine("Id: {0}, Nome: {1}, Cargo: {2}, Data: {3}", usuario.Id, usuario.Nome, usuario.Cargo, usuario.Data);
            }

            //Console.ReadKey();
        }
    }
}

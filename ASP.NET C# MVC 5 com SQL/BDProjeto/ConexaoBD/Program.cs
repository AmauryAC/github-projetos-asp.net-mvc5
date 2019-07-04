using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoBD
{
    class Program
    {
        static void Main(string[] args)
        {
            //var usuario = new Usuario();
            var usuarioAplicacao = new UsuarioAplicacao();

            // Insert e Update
            /*Console.Write("Digite o nome do usuário: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o cargo do usuário: ");
            string cargo = Console.ReadLine();

            DateTime data = DateTime.Now;

            usuario.Nome = nome;
            usuario.Cargo = cargo;
            usuario.Data = data;
            //usuario.Id = 5;

            usuarioAplicacao.Salvar(usuario);*/

            // Delete
            //usuarioAplicacao.Excluir(7);

            // Select
            var dados = usuarioAplicacao.ListarTodos();

            foreach(var usuario in dados)
            {
                Console.WriteLine("Id: {0}, Nome: {1}, Cargo: {2}, Data: {3}", usuario.Id, usuario.Nome, usuario.Cargo, usuario.Data);
            }

            //Console.ReadKey();
        }
    }
}

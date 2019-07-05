using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDProjeto.Dominio
{
    public class Usuario
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome do usuário")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o cargo do usuário")]
        [DisplayName("Cargo")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Preencha a data de cadastro")]
        [DisplayName("Data de Cadastro")]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
    }
}

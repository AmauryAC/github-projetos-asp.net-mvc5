using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Introducao.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Insira uma informação entre 5 e 50 caracteres.")]
        public string Observacoes { get; set; }

        [Range(18, 70, ErrorMessage = "A idade tem que estar entre 18 e 70 anos.")]
        public int Idade { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Digite um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        [RegularExpression(@"[a-zA-Z]{5,15}", ErrorMessage = "Somente letras e 5 a 15 caracteres.")]
        [Remote("LoginUnico", "Usuario", ErrorMessage = "Este login já existe.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "As senhas não se coincidem!")]
        public string ConfirmarSenha { get; set; }
    }
}
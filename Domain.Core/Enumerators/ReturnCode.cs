using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Core.Enumerators
{
  public enum ReturnCode
  {
    [Display(Name = "Preenchimento dos dados inválido")]
    PreenchimentoInvalido   = 1000,

    [Display(Name = "E-mail já existente para outro usuário!")]
    EmailExiste             = 1004,

    [Display(Name = "Usuário ou senha incorretos")]
    UsuarioOuSenhaErrados   = 1005,

    [Display(Name = "Senha inválida, o tamanho deve ter entre 6  e 100 caracteres")]
    TamanhoSenhaInvalido    = 1006,

    [Display(Name = "O usuário foi bloqueado devido à realização de várias tentativas de acesso")]
    UsuarioBloqueado        = 1007,

    [Display(Name = "Dados de login inválidos")]
    DadosDeLoginInvalidos   = 1200,

    [Display(Name = "Ocorreu um erro não especificado, não foi possível completar a ação")]
    ImpossivelFinalizarAcao = 1650,

    [Display(Name = "Um registro com esta descrição já existe")]
    DescricaoExiste = 2000,

    [Display(Name = "Registro não encontrado")]
    RegistroNaoEncontrado = 2001,

    [Display(Name = "Seu usuário não foi aprovado pela comissão!")]
    UsuarioNaoAprovado = 2002,

    [Display(Name = "Não é possível editar a avaliação deste artigo, verifique a data e o turno do mesmo.")]
    NaoEhPossivelEditarAvaliacaoArtigo = 2003,
  }
}

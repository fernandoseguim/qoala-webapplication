using System.ComponentModel.DataAnnotations;
using System;

namespace Website.Models.ViewModels.Sponsor
{

    public class PlanViewModel
    {
        [Display(Name = "Código do Plano")]
        public int IdPlan { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome do plano.")]
        [Display(Name = "Nome do Plano")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor.")]
        [Display(Name = "Valor do Plano")]
        [DataType(DataType.Currency)]
        public float Price_cents { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade.")]
        [Display(Name = "Quantidade em estoque")]
        public int Left { get; set; }

        [Required(ErrorMessage = "Por favor, informe um texto explicativo do plano")]
        [Display(Name = "Descrição do plano")]
        [DataType(DataType.Html)]
        [System.Web.Mvc.AllowHtml]
        public string Rewards { get; set; }

        [Display(Name = "Criado em")]
        public string CreatedAt { get; set; }

    }
}
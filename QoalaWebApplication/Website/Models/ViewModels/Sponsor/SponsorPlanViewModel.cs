using System.ComponentModel.DataAnnotations;
using System;

namespace Website.Models.ViewModels.Sponsor
{

    public class SponsorPlanViewModel
    {
        public int IdPlan { get; set; }

        [Required(ErrorMessage = "Por favor, informe o id do user")]
        [Display(Name = "Id do usuário")]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade")]
        [Display(Name = "Quantidade")]
        public int Qnt { get; set; }

        public string PlanName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models.ViewModels.Sponsor
{
    public class ReportViewModel
    {

        [Display(Name = "Código do Plano")]
        public int IdPlan { get; set; }
        public int IdPlan2 { get; set; }

        [Display(Name = "Nome do Plano")]
        public string Name { get; set; }

        [Display(Name = "Quantidade em estoque")]
        public int PlanLeft { get; set; }
        public int PlanLeft2 { get; set; }

        [Display(Name = "Quantidade vendido")]
        public int PlanSold { get; set; }
        public int PlanSold2 { get; set; }

        [Display(Name = "Valor do Plano")]
        public int PriceCents { get; set; }
        public int PriceCents2 { get; set; }

        [Display(Name = "Valor arrecadado")]
        [DataType(DataType.Currency)]
        public decimal FundsNow { get { return (PriceCents * PlanSold); } }
    }
}
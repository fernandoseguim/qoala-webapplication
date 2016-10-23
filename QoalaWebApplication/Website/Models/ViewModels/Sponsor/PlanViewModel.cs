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

        /// <summary>
        /// Este é o campo de entrada/saida do WebService
        /// não contem separador de decimal, e deve ter 2 casas decimais.
        /// 
        /// 
        /// Por exemplo: 181,85 será armazenado aqui apenas 18185.
        /// assim como 120,00 será 12000
        /// </summary>
        public int Price_cents { get; set; }

        /// <summary>
        /// Esta propriedade faz a conversão para tirar e colocar dos centavos <br/>
        /// de modo que o valor a ser entregue e recebido pelo WebService deverá ser o <code>Price_cents</code> e não este.
        /// 
        /// <b># Atenção #: este campo somente é utilizado nas Views</b>
        /// </summary>
        [Required(ErrorMessage = "Por favor, informe o valor.")]
        [Display(Name = "Valor do Plano")]
        //[DataType(DataType.Currency)]
        public float Price_curr
        {
            get { return (Price_cents / 100); }
            set { Price_cents = int.Parse((value * 100).ToString("0")); }
        }

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
        public int? QntTotal { get; set; }

    }
}
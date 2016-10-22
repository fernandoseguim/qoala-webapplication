using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class ListViewModel<T> where T : class, new()
    {
        public List<T> ListModel { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public T Filter { get; set; }

        /// <summary>
        /// Cria objeto default de lista com paginacao
        /// </summary>
        /// <param name="ControllerName">Nome do Controller para retorno das Actions de paginacao</param>
        public ListViewModel() 
        {
            ListModel = new List<T>();
            Pagination = new PaginationViewModel
            {
                NextPage = false,
                PreviousPage = false,
                CurrentPage = 1,
                TotalNumberPages = 1,
                ControllerName = ""
            };
            Filter = new T();
        }
    }
}
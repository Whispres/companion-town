using System.Collections.Generic;

namespace Api.Models
{
    public class PagedResult<TModel>
    {
        public PagedResult()
        {
            this.Results = new List<TModel>();
        }

        public int Page { get; set; }

        public int Take { get; set; }

        public int Total { get; set; }

        public List<TModel> Results { get; set; }
    }
}
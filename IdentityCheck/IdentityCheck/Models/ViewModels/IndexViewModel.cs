
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Models.ViewModels
{
    public class IndexViewModel
    {
        public ApplicationUser AppUser { get; set; }
        public List<Post> Posts { get; set; }
        public PagingList<Post> PagingList{ get; set; }


    }
}

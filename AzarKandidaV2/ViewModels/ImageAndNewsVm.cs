using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzarKandidaV2.ViewModels
{
    public class ImageAndNewsVm
    {
        public List<TblImage> Images { get; set; }
        public List<TblBlog> Blogs { get; set; }
    }
}
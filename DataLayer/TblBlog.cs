//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class TblBlog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string MainImage { get; set; }
        public string Description { get; set; }
    }
}

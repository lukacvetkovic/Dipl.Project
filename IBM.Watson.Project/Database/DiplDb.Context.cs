﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IBM.Watson.Project.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DiplProjectDb : DbContext
    {
        public DiplProjectDb()
            : base("name=DiplProjectDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleKeyword> ArticleKeyword { get; set; }
        public virtual DbSet<Concept> Concept { get; set; }
        public virtual DbSet<WatsonKeyword> WatsonKeyword { get; set; }
    }
}

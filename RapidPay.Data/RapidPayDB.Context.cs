﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RapidPay.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RapidPayEntities : DbContext
    {
        public RapidPayEntities()
            : base("name=RapidPayEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RPCard> RPCards { get; set; }
        public virtual DbSet<RPUser> RPUsers { get; set; }
        public virtual DbSet<RPPayment> RPPayments { get; set; }
    }
}
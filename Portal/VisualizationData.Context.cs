﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VisualizationEntities : DbContext
    {
        public VisualizationEntities()
            : base("name=VisualizationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Medium> Media { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<TweetHashtag> TweetHashtags { get; set; }
        public DbSet<TweetWebsite> TweetWebsites { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<WebsiteKeyword> WebsiteKeywords { get; set; }
        public DbSet<SiteTitle> SiteTitles { get; set; }
    }
}

//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Hashtag
    {
        public Hashtag()
        {
            this.TweetHashtags = new HashSet<TweetHashtag>();
        }
    
        public int ID { get; set; }
        public string Tag { get; set; }
    
        public virtual ICollection<TweetHashtag> TweetHashtags { get; set; }
    }
}
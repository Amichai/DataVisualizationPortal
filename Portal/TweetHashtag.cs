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
    
    public partial class TweetHashtag
    {
        public int ID { get; set; }
        public int TweetID { get; set; }
        public int HashtagID { get; set; }
    
        public virtual Hashtag Hashtag { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
}

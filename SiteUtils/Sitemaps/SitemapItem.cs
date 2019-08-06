using System;
using System.Collections.Generic;
using System.Text;

namespace dalhome3.SiteUtils.Sitemap
{
    class SitemapItem
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ReturnType { get; set; }
        public string[] Attributes { get; set; }
    }

    class DistinctSitemapItem : IEqualityComparer<SitemapItem>
    {

        public bool Equals(SitemapItem x, SitemapItem y)
        {
            return x.Action == y.Action &&
                x.Controller == y.Controller &&
                x.ReturnType == y.ReturnType;
        }

        public int GetHashCode(SitemapItem obj)
        {
            return obj.Controller.GetHashCode() ^
                obj.Action.GetHashCode() ^
                obj.ReturnType.GetHashCode();
        }
    }
}

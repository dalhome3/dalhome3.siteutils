using dalhome3.SiteUtils.Sitemap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace dalhome3.SiteUtils
{
    public class Sitemaps
    {
        public static string Generate(string baseURL, string rootPath)
        {
            string output =
"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";
            Assembly asm = Assembly.GetCallingAssembly();

            var controllers = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type)); //filter controllers

            List<SitemapItem> info = new List<SitemapItem>();

            foreach (var controller in controllers)
            {
                var Excluded = controller.IsDefined(typeof(SitemapExcludeAttribute), false);

                info.AddRange(controller.GetMethods()
                .Where(method => method.IsDefined(typeof(SitemapIncludeAttribute)) || (method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)) && typeof(ActionResult).IsAssignableFrom(method.ReturnType) 
                    && !method.IsDefined(typeof(SitemapExcludeAttribute)) && !Excluded))
                .Select(x => new SitemapItem
                {
                    Controller = x.DeclaringType.Name.ToLower().Replace("controller", ""),
                    Action = x.Name,
                    ReturnType = x.ReturnType.Name,
                    Attributes = x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).ToArray()
                }).ToList());
            }

            info = info.OrderBy(x => x.Controller).ThenBy(x => x.Action).Distinct(new DistinctSitemapItem()).ToList();

            foreach (var item in info)
            {
                output += $"\n\t<url>" +
                    $"\n\t\t<loc>{baseURL}/{item.Controller}/{item.Action}</loc>";
                string path = rootPath + $"/Views/{item.Controller}/{item.Action}.cshtml";
                if (File.Exists(path))
                {
                    FileInfo fi = new FileInfo(path);
                    output += $"\n\t\t<lastmod>{fi.LastWriteTime.ToString("yyyy-MM-dd")}</lastmod>";
                }
                output += "\n\t</url>"; ;
            }

            output += "\n</urlset>";

            return output;
        }

    }
}

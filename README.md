# SiteUtils

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

dalhome3.siteutils is designed to help with SEO optimisations starting with a sitemap generator design to be used in asp.netcore based on the simple actions design.

  - Setup route to "Sitemap.xml"
  - Add Sitemap.generate() command and return an XML content result.
  - All controllers and actions are added to the sitemap generator
  - Also you can easily block or add methods using [SitemapInclude] & [SitemapExclude] attributes.
  - Magic

# New Features!

  - Use reflection to autogenerate the sitemap based on class features.

### Usage:

From Asp.NetCore...
```
[Route("/Sitemap.xml")]
public IActionResult SiteMap()
{
    string sitemap = dalhome3.SiteUtils.Sitemaps.Generate($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}", _env.ContentRootPath);
    return Content(sitemap, "application/xml");
}
```

From Asp.Net MVC...
In Route Config
```
routes.MapMvcAttributeRoutes();
```
Then in a controller
```
public ActionResult Sitemap()
{
    string sitemap = Sitemaps.Generate($"{HttpContext.Request.Url.Scheme}://{HttpContext.Request.Url.Authority}", Server.MapPath("/"));
    return Content(sitemap, "application/xml");
}
```
Finally Add this to Web.config
```
<system.webServer>
   <handlers>
     <add name="SitemapXml" path="sitemap.xml" verb="GET" type="System.Web.Handlers.TransferRequestHandler"
        preCondition="integratedMode,runtimeVersionv4.0" />
   </handlers>
</system.webServer>
```

### Tech

SiteUtils uses a number of dependcies to work properly:

* [System.Reflection] - Reflect and view info about executing assemply
* [Microsoft.AspNetCore.Mvc.ViewFeatures] - Access to all of the goodness of asp.netcore

And of course Dillinger itself is open source with a [public repository][dill]
 on GitHub.

### Installation

SiteUtils can be installed from Nuget Package Manager

From Visual Studio...

```sh
$ install-package dalhome3.siteutils
```

License
----

Creative Commons Attribution-ShareAlike 4.0 International

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [System.Reflection]: <https://docs.microsoft.com/en-us/dotnet/api/system.reflection?view=netframework-4.8>
   [Microsoft.AspNetCore.Mvc.ViewFeatures]: <https://asp.net>
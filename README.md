CourtesyFlush
=================

A library to simplify flushing HTTP responses early in ASP.NET MVC.

![CourtesyFlush](https://raw.githubusercontent.com/nikmd23/CourtesyFlush/master/banner.jpg)

## Installation

`CourtesyFlush` can be installed via NuGet:

```
Install-Package CourtesyFlush
```

## Why Flush Early?

Flushing early can provide performance improvements in web applications and has been [a recomended best practice in the web performance community since 2007](http://stevesouders.com/hpws/).

To find out more, check out my blog where I covered the benefits of flushing early in two posts:

- [Flushing in ASP.NET MVC](http://nikcodes.com/2014/03/04/flushing-in-asp-net-mvc/)
- [More HTTP Flushing in ASP.NET MVC](http://nikcodes.com/2014/03/17/more-http-flushing-in-asp-net-mvc/)

## Usage
`CourtesyFlush` is easy to use. It builds on top of common ASP.NET MVC Action Filter functionality.

A full writeup of [how to use `CourtesyFlush` is availble on my blog](http://nikcodes.com/2014/06/23/perfmatters-flush-goes-1-0/).

## Release Notes

### 1.1
- Added support for AntiForgeryTokens. Example usage:
  
  ```C#
  // The "GET" action that includes the token...
  [HttpGet, FlushHead(Title = "Flushed Title", FlushAntiForgeryToken = true)]
  public ActionResult Register()
  {
      return View();
      // Inside this view, call @Html.FlushedAntiForgeryToken() instead of @Html.AntiForgeryToken()
  }

  // The "POST" action with standard ValidateAntiForgeryToke attribute 
  [HttpPost, ValidateAntiForgeryToken]
  public ActionResult Register(string username, string password)
  {
      // handle valid request here
  }
  ```

  *NOTE: `AntiForgeryToken` support only available in .NET 4.5* 

### 1.0
- Re-branded from PerfMatters.Flush to CourtesyFlush 
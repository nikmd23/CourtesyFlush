PerfMatters.Flush
=================

The source for my CourtesyFlush NuGet package.

## Installation

`CourtesyFlush` can be installed via NuGet:

```
Install-Package CourtesyFlush
```

## Setup

 - Create a partial view named `_Head` and place it somewhere in your `Views` folder. (Usually in `Shared`)
 - Cut everything from `_Layout` that you want to flush early and place it in `_Head`.
 - Place a call to `@Html.FlushHead()` at the top of `_Layout`. This will ensure that the layout is still usable on action methods that don't flush, as shown below. 

## Usage
There are two ways to use `CourtesyFlush`:

 1. Leverage the `[FlushHead]` action method attribute:

    ```c#
	[FlushHead(Title = "Welcome to my site")]
	public ActionResult Index()
	{
    	// do some work to generate model
		var model = ...;

		return View(model);
	}
    ```

   The `[FlushHead]` attribute has an optional `Title` property, which will populate `@ViewBag.Title` in your view.

 2. For more advanced scenarios, use the `FlushHead()` extension method:

    
	```c#
	public ActionResult Index()
	{
		ViewBag.Foo = "Bar";
		this.FlushHead(title: "Welcome to my site");

    	// do some work to generate model
		var model = ...;

		return View(model);
	}
    ```

    The `FlushHead()` method automatically copies over any ViewData/ViewBag state, accepts an optional `title` parameter as well an optional `model` parameter, if required.
# Theseus

`IUrlHelper` extensions to point at a controller method to generate links.

Example:

```cs
// Regular IUrlHelper:
string aboutUrl = Url.Action("About", "Home");
string buyUrl = Url.Action("Buy", "Products", new { id = 17, color = "red" });

// Theseus:
string aboutUrl = Url.To<HomeController>(y => y.About());
string buyUrl = Url.To<ProductsController>(y => y.Buy(17, "red"));
```

This library uses [EpsilonEvaluator](https://www.nuget.org/packages/EpsilonEvaluator)
to evaluate the route parameters from the method arguments. Most expressions should be evaluated without compilation and
compiled expressions are not cached.

Inspired by [AspNet.Mvc.TypedRouting][https://github.com/ivaylokenov/AspNet.Mvc.TypedRouting] from Ivaylo Kenov.

## Thanks

- blockmath_2048 for the name suggestion.

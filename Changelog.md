## Changelog

## 0.3.0:

### BREAKING

- Theseus now targets .NET8.0 exclusively.
- Theseus now requires you to call `app.UseTheseus()` in your startup.cs:

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime) {
	/* ... rest of your configuration ... */
	app.UseRouting();
	app.UseTheseus();
	/* ... rest of your configuration ... */
}
```

## 0.2.0:

- Change: Signature of `To<T>(expression, object)` has changed.
	the additional route values used to be an optional parameter. Now the method is overloaded w/ and w/o the parameter.
	This allows for cleaner separation of the differnet codepaths and improves performance for `To<>()` calls w/o additional values.
- Internal: Bumped EpsilonEvaluator for performance benefits in common route expressions.


## 0.1.2
- Fix: Default route values were not applied in hot path.

## 0.1.1

### General:

- Bumped EpsilonEvaluator
- Changed `To<T>(Action<T>)` to `To<T>(Func<T, ActionResult>)`.

### Bugfixes:
- Some URLs did not generate correctly because routing parameters from retrieved action descriptor were not applied.
- Fixed merging of default route values with additional route values from second `To()` parameter.

## 0.1.0

Initital release.

Added public APIs:

* `Url.To<TController>(Func<TController, ActionResult>, routeValues? = null)`
* `Url.To<TController>(Func<TController, Task<ActionResult>>, routeValues? = null)`

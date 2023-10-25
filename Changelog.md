## Changelog

## 0.1.2

Fix:

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

/**
	This Source Code Form is subject to the terms of the Mozilla Public
	License, v. 2.0. If a copy of the MPL was not distributed with this
	file, You can obtain one at http://mozilla.org/MPL/2.0/.
**/

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Theseus;

/// <summary>
/// Theseus controller extensions to abbreviate some common tasks.
/// </summary>
public static class ControllerExtensions {
	/// <summary>
	/// Shortcut for <code>Controller.Redirect(Url.To&lt;TargetController&gt;(actionExpression))</code>
	/// </summary>
	/// <typeparam name="TargetController">
	/// Which controller to direct to.
	/// </typeparam>
	/// <param name="controller">
	/// Controller instance on which to invoke the extension.
	/// </param>
	/// <param name="actionExpression">
	/// Expression that identifies the target action.
	/// </param>
	/// <returns>
	/// The constructed redirect result.
	/// </returns>
	public static RedirectResult RedirectTo<TargetController>(
		this Controller controller,
		Expression<Func<TargetController, Task<ActionResult>>> actionExpression
	) where TargetController : Controller {
		return controller.Redirect(controller.Url.To(actionExpression));
	}

	/// <summary>
	/// Shortcut for <code>Controller.Redirect(Url.To&lt;TargetController&gt;(actionExpression))</code>
	/// </summary>
	/// <typeparam name="TargetController">
	/// Which controller to direct to.
	/// </typeparam>
	/// <param name="controller">
	/// Controller instance on which to invoke the extension.
	/// </param>
	/// <param name="actionExpression">
	/// Expression that identifies the target action.
	/// </param>
	/// <returns>
	/// The constructed redirect result.
	/// </returns>
	public static RedirectResult RedirectTo<TargetController>(
		this Controller controller,
		Expression<Func<TargetController, ActionResult>> actionExpression
	) where TargetController : Controller {
		return controller.Redirect(controller.Url.To(actionExpression));
	}
}

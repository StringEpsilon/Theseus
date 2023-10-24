/**
	This Source Code Form is subject to the terms of the Mozilla Public
	License, v. 2.0. If a copy of the MPL was not distributed with this
	file, You can obtain one at http://mozilla.org/MPL/2.0/.
**/

using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using EpsilonEvaluator;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace Theseus;

public static class IUrlHelperExtensions {
	private static ConcurrentDictionary<MethodInfo, ControllerActionDescriptor?> _cache = new();
	private static IActionDescriptorCollectionProvider? _provider;

	/// <summary>
	/// Generates a URL with an absolute path for the action method identified by the function expression.
	/// </summary>
	/// <typeparam name="TController">
	/// Controller the action belongs to.
	/// </typeparam>
	/// <param name="urlHelper">
	/// Extended IUrlHelper instance.
	/// </param>
	/// <param name="action">
	/// Expression that identifies the target action.
	/// </param>
	/// <param name="routeValues">
	/// Additional route values. Optional.
	/// </param>
	/// <returns>
	/// The generated URL.
	/// </returns>
	/// <exception cref="ArgumentException"></exception>
	public static string To<TController>(
		this IUrlHelper urlHelper,
		Expression<Action<TController>> action,
		object? routeValues = null
	) where TController : class {
		return ConstructUrl(urlHelper, action.Body as MethodCallExpression, routeValues) ??
			throw new ArgumentException($"Can not create URL to {typeof(TController).FullName}");
	}

	/// <summary>
	/// Generates a URL with an absolute path for the action method identified by the function expression.
	/// </summary>
	/// <typeparam name="TController">
	/// Controller the action belongs to.
	/// </typeparam>
	/// <param name="urlHelper">
	/// Extended IUrlHelper instance.
	/// </param>
	/// <param name="action">
	/// Expression that identifies the target action.
	/// </param>
	/// <param name="routeValues">
	/// Additional route values. Optional.
	/// </param>
	/// <returns>
	/// The generated URL.
	/// </returns>
	/// <exception cref="ArgumentException"></exception>
	public static string To<TController>(
		this IUrlHelper urlHelper,
		Expression<Func<TController, Task<ActionResult>>> action,
		object? routeValues = null
	) where TController : class {
		return ConstructUrl(urlHelper, action.Body as MethodCallExpression, routeValues) ??
			throw new ArgumentException($"Can not create URL to {typeof(TController).FullName}");
	}

	private static string? ConstructUrl(
		IUrlHelper urlHelper,
		MethodCallExpression? callExpression,
		object? routeValues
	) {
		if (callExpression == null) {
			return null;
		}
		ControllerActionDescriptor? actionDescriptor = GetDescriptor(urlHelper, callExpression.Method);
		if (actionDescriptor == null) {
			return null;
		}
		return urlHelper.Action(GetActionContext(callExpression, actionDescriptor, routeValues));
	}

	private static UrlActionContext GetActionContext(
		MethodCallExpression callExpression,
		ControllerActionDescriptor actionDescriptor,
		object? additionalRouteValues = null
	) {
		if (callExpression.Object == null) {
			throw new InvalidOperationException("Can not generate links to static methods.");
		}
		UrlActionContext context;

		if (!actionDescriptor.Parameters.Any()) {
			context = new UrlActionContext() {
				Action = actionDescriptor.ActionName,
				Controller = actionDescriptor.ControllerName,
				Values = new RouteValueDictionary(actionDescriptor.RouteValues),
			};
			if (additionalRouteValues!=null) {
				context.Values = MergeRouteValues(
					(IDictionary<string, object?>)context.Values,
					new RouteValueDictionary(additionalRouteValues)
				);
			}
			return context;
		}

		var routeValues = new RouteValueDictionary(actionDescriptor.RouteValues);

		var parameters = callExpression.Method.GetParameters();
		for (int i = 0; i < callExpression.Arguments.Count; i++) {
			routeValues.Add(
				parameters[i].Name ?? "",
				ExpressionEvaluator.Evaluate(callExpression.Arguments[i])
			);
		}

		context = new UrlActionContext() {
			Action = actionDescriptor.ActionName,
			Controller = actionDescriptor.ControllerName,
		};

		if (additionalRouteValues != null) {
			context.Values = MergeRouteValues(
				routeValues,
				new RouteValueDictionary(additionalRouteValues)
			);
		}
		return context;
	}

	private static IDictionary<string, object?> MergeRouteValues(
		IDictionary<string, object?> routeValues,
		IDictionary<string, object?> additionalRouteValues
	) {
		if (additionalRouteValues != null && additionalRouteValues.Any()) {
			foreach (var keyValue in routeValues) {
				routeValues.TryAdd(keyValue.Key, keyValue.Value);
			}
		}
		return routeValues;
	}

	private static ControllerActionDescriptor? GetDescriptor(IUrlHelper urlHelper, MethodInfo methodInfo) {
		return _cache.GetOrAdd(
			methodInfo,
			methodInfo => {
				var provider = GetProvider(urlHelper);
				return (ControllerActionDescriptor?)provider.ActionDescriptors.Items.FirstOrDefault(
					y =>
						y is ControllerActionDescriptor controllerDescriptor
						&& controllerDescriptor.MethodInfo == methodInfo
				);
			}
		);
	}

	private static IActionDescriptorCollectionProvider GetProvider(IUrlHelper urlHelper) {
		if (_provider != null) {
			return _provider;
		}

		var provider = urlHelper
			.ActionContext
			.HttpContext
			.RequestServices
			.GetService(typeof(IActionDescriptorCollectionProvider)) as IActionDescriptorCollectionProvider;

		if (provider == null) {
			throw new InvalidOperationException(
				"Can not resolve URL. Missing IActionDescriptorCollectionProvider service."
			);
		}
		_provider = provider;
		return provider;
	}
}

/**
	This Source Code Form is subject to the terms of the Mozilla Public
	License, v. 2.0. If a copy of the MPL was not distributed with this
	file, You can obtain one at http://mozilla.org/MPL/2.0/.
**/

using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Linq;

namespace Theseus;

internal static class ActionDescriptorCache {
	private static IActionDescriptorCollectionProvider? _provider = null;

	internal static void Set(IActionDescriptorCollectionProvider provider) {
		_provider = provider;
	}
	private static ConcurrentDictionary<MethodInfo, ControllerActionDescriptor?> _descriptorCache = new();


	private static IActionDescriptorCollectionProvider Get() {
		if (_provider == null) {
			throw new InvalidOperationException(
				"No action descriptor provider found. Please add .UseTheseus() to your application builder."
			);
		}
		return _provider;
	}

	internal static ControllerActionDescriptor? GetDescriptor(IUrlHelper urlHelper, MethodInfo methodInfo) {
		var descriptor = _descriptorCache.GetOrAdd(
			methodInfo,
			methodInfo => {
				var provider = Get();
				return (ControllerActionDescriptor?)provider.ActionDescriptors.Items.FirstOrDefault(
					y =>
						y is ControllerActionDescriptor controllerDescriptor
						&& controllerDescriptor.MethodInfo == methodInfo
				);
			}
		);
		return descriptor;
	}
}

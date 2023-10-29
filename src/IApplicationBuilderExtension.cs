/**
	This Source Code Form is subject to the terms of the Mozilla Public
	License, v. 2.0. If a copy of the MPL was not distributed with this
	file, You can obtain one at http://mozilla.org/MPL/2.0/.
**/

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Theseus;

public static class IApplicationBuilderExtension {

	/// <summary>
	/// Initializes theseus.
	/// </summary>
	/// <param name="app">
	/// The Microsoft.AspNetCore.Builder.IApplicationBuilder to add theseus to.
	/// </param>
	/// <returns>
	/// A reference to the app.
	/// </returns>
	public static IApplicationBuilder UseTheseus(this IApplicationBuilder app) {
		var provider = app.ApplicationServices.GetService<IActionDescriptorCollectionProvider>();
		if (provider != null) {
			ActionDescriptorCache.Set(provider);
		}
		return app;
	}
}

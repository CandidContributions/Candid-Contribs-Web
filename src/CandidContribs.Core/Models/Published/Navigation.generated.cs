//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v8.5.3
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace CandidContribs.Core.Models.Published
{
	// Mixin Content Type with alias "navigation"
	/// <summary>_Navigation</summary>
	public partial interface INavigation : IPublishedContent
	{
		/// <summary>Menu Icon</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		global::Umbraco.Core.Models.PublishedContent.IPublishedContent MenuIcon { get; }

		/// <summary>umbracoNaviHide</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		bool UmbracoNaviHide { get; }
	}

	/// <summary>_Navigation</summary>
	[PublishedModel("navigation")]
	public partial class Navigation : PublishedContentModel, INavigation
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new const string ModelTypeAlias = "navigation";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Navigation, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public Navigation(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Menu Icon: icon to use if page is set to appear in header menu
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("menuIcon")]
		public global::Umbraco.Core.Models.PublishedContent.IPublishedContent MenuIcon => GetMenuIcon(this);

		/// <summary>Static getter for Menu Icon</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public static global::Umbraco.Core.Models.PublishedContent.IPublishedContent GetMenuIcon(INavigation that) => that.Value<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>("menuIcon");

		///<summary>
		/// umbracoNaviHide: surely we all know that this means?!
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("umbracoNaviHide")]
		public bool UmbracoNaviHide => GetUmbracoNaviHide(this);

		/// <summary>Static getter for umbracoNaviHide</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public static bool GetUmbracoNaviHide(INavigation that) => that.Value<bool>("umbracoNaviHide");
	}
}

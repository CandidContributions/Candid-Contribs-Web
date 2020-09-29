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
	/// <summary>Events page</summary>
	[PublishedModel("eventsPage")]
	public partial class EventsPage : PublishedContentModel, IMetaTags, INavigation
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new const string ModelTypeAlias = "eventsPage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<EventsPage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public EventsPage(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// About right content: appears alongside the image in 'about' section
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("aboutContent")]
		public global::System.Web.IHtmlString AboutContent => this.Value<global::System.Web.IHtmlString>("aboutContent");

		///<summary>
		/// About left content: only shows if no left image specified
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("aboutLeftContent")]
		public global::System.Web.IHtmlString AboutLeftContent => this.Value<global::System.Web.IHtmlString>("aboutLeftContent");

		///<summary>
		/// About left image: Select an image or leave blank (and specify About left content)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("aboutLeftImage")]
		public global::Umbraco.Core.Models.PublishedContent.IPublishedContent AboutLeftImage => this.Value<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>("aboutLeftImage");

		///<summary>
		/// Css class: eg 'codepatch' or 'umbrackathon'
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("cssClass")]
		public string CssClass => this.Value<string>("cssClass");

		///<summary>
		/// Header logo
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("headerLogo")]
		public global::Umbraco.Core.Models.PublishedContent.IPublishedContent HeaderLogo => this.Value<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>("headerLogo");

		///<summary>
		/// Header words: If a logo is specified, this will be used as the alt text of the logo. If no logo then this text will displayed in the header banner
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("headerWords")]
		public string HeaderWords => this.Value<string>("headerWords");

		///<summary>
		/// Mailchimp group id: Leave this blank if you don't want to the sign up form to show. 2 = codepatch, 4 = umbrackathon...
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("mailchimpGroupId")]
		public int MailchimpGroupId => this.Value<int>("mailchimpGroupId");

		///<summary>
		/// Entries
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("part1Entries")]
		public global::System.Collections.Generic.IEnumerable<global::CandidContribs.Core.Models.Published.DayScheduleEntry> Part1Entries => this.Value<global::System.Collections.Generic.IEnumerable<global::CandidContribs.Core.Models.Published.DayScheduleEntry>>("part1Entries");

		///<summary>
		/// Start date
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("part1StartDate")]
		public global::System.DateTime Part1StartDate => this.Value<global::System.DateTime>("part1StartDate");

		///<summary>
		/// Entries
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("part2Entries")]
		public global::System.Collections.Generic.IEnumerable<global::CandidContribs.Core.Models.Published.DayScheduleEntry> Part2Entries => this.Value<global::System.Collections.Generic.IEnumerable<global::CandidContribs.Core.Models.Published.DayScheduleEntry>>("part2Entries");

		///<summary>
		/// Start date
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("part2StartDate")]
		public global::System.DateTime Part2StartDate => this.Value<global::System.DateTime>("part2StartDate");

		///<summary>
		/// Sign up content: appears above the email input in 'Sign Up' section
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("signUpContent")]
		public global::System.Web.IHtmlString SignUpContent => this.Value<global::System.Web.IHtmlString>("signUpContent");

		///<summary>
		/// Speakers
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("speakers")]
		public global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent> Speakers => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>>("speakers");

		///<summary>
		/// Description: meta and OG description,  has default value
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("metaDescription")]
		public string MetaDescription => global::CandidContribs.Core.Models.Published.MetaTags.GetMetaDescription(this);

		///<summary>
		/// Keywords: meta keywords, has default value
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("metaKeywords")]
		public string MetaKeywords => global::CandidContribs.Core.Models.Published.MetaTags.GetMetaKeywords(this);

		///<summary>
		/// Title: browser and OG title, defaults to node name
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("metaTitle")]
		public string MetaTitle => global::CandidContribs.Core.Models.Published.MetaTags.GetMetaTitle(this);

		///<summary>
		/// Open Graph image: Leave blank to use the default image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("openGraphImage")]
		public global::Umbraco.Core.Models.PublishedContent.IPublishedContent OpenGraphImage => global::CandidContribs.Core.Models.Published.MetaTags.GetOpenGraphImage(this);

		///<summary>
		/// Menu Icon: icon to use if page is set to appear in header menu
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("menuIcon")]
		public global::Umbraco.Core.Models.PublishedContent.IPublishedContent MenuIcon => global::CandidContribs.Core.Models.Published.Navigation.GetMenuIcon(this);

		///<summary>
		/// umbracoNaviHide: surely we all know that this means?!
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("umbracoNaviHide")]
		public bool UmbracoNaviHide => global::CandidContribs.Core.Models.Published.Navigation.GetUmbracoNaviHide(this);
	}
}

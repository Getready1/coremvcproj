using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Infrastructure
{
	[HtmlTargetElement("div", Attributes = "paging-model")]
    public class PageLinkTagHelper : TagHelper
    {
		private IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
		{
			this.urlHelperFactory = urlHelperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public PagingInfo PagingModel { get; set; }

		public string ActionName { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			TagBuilder result = new TagBuilder("div");

			for (int i = 1; i <= PagingModel.TotalPages; i++)
			{
				TagBuilder tag = new TagBuilder("a");
				tag.Attributes["href"] = urlHelper.Action(ActionName, new { page = i });
				tag.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(tag);
			}

			output.Content.AppendHtml(result.InnerHtml);
		}
	}
}

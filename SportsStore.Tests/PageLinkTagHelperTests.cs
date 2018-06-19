using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Web.Infrastructure;
using SportsStore.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Tests
{

    public class PageLinkTagHelperTests
    {
		[Fact]
		public void Can_Generate_Links()
		{
			var urlHelper = new Mock<IUrlHelper>();
			urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
				.Returns("Test/Page1")
				.Returns("Test/Page2")
				.Returns("Test/Page3");

			var urlHelperFactory = new Mock<IUrlHelperFactory>();
			urlHelperFactory.Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>()))
				.Returns(urlHelper.Object);

			PageLinkTagHelper tagHelper = new PageLinkTagHelper(urlHelperFactory.Object)
			{
				ActionName = "Test",
				PagingModel = new PagingInfo
				{
					CurrentPage = 2,
					ItemsPerPage = 10,
					TotalItems = 28
				}
			};

			TagHelperContext tagContext = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

			var content = new Mock<TagHelperContent>();
			TagHelperOutput output = new TagHelperOutput
				(
				"div",
				new TagHelperAttributeList(),
				(cache, encoder) => Task.FromResult(content.Object)
				);

			tagHelper.Process(tagContext, output);

			var linKTemplate = @"<a href=""Test/Page{0}"">{0}</a>";

			Assert.Equal(string.Concat(
				string.Format(linKTemplate, 1),
				string.Format(linKTemplate, 2),
				string.Format(linKTemplate, 3)
				), output.Content.GetContent());
		}
    }
}

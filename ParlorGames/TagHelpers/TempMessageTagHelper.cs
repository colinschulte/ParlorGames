using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;      // ViewContext attribute
using Microsoft.AspNetCore.Mvc.Rendering;         // ViewContext class
using ParlorGames;

namespace ParlorGames.TagHelpers
{
    [HtmlTargetElement("my-temp-message")]
    public class TempMessageTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var td = ViewCtx.TempData;
            if (td.ContainsKey("message"))
            {
                string bg = td["background"]?.ToString() ?? "info";  // default background if none in TempData
                output.BuildTag("h4", $"bg-{bg} text-center text-white p-2");
                output.Content.SetContent(td["message"].ToString());
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }

}

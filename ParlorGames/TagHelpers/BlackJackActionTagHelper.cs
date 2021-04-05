using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;       // for TagBuilder and ViewContext data type
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // for ViewContext attribute
using Microsoft.AspNetCore.Routing;             // for LinkGenerator

namespace ParlorGames.TagHelpers
{
    public class MyBlackJackActionTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public MyBlackJackActionTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public string Action { get; set; }
        public bool IsDisabled { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // make it a form element with start and end tag
            output.TagName = "form";
            output.TagMode = TagMode.StartTagAndEndTag;

            // get URL for form action
            string ctlr = ViewCtx.RouteData.Values["controller"].ToString();
            string url = linkBuilder.GetPathByAction(Action, ctlr);

            // add action and post attributes to form
            output.Attributes.SetAttribute("action", url);
            output.Attributes.SetAttribute("method", "post");

            // add bootstrap column class to form
            output.Attributes.SetAttribute("class", "col");

            // create and style a submit button 
            TagBuilder button = new TagBuilder("button");
            button.Attributes.Add("type", "submit");
            button.Attributes.Add("class", "btn btn-primary");
            button.InnerHtml.Append(Action);

            // disable the button as needed
            if (IsDisabled)
                button.Attributes.Add("disabled", "disabled");

            // add button to form
            output.Content.AppendHtml(button);
        }
    }
}

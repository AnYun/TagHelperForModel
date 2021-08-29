using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagHelperForModel.Helper
{
    public class DisplayTitleTagHelper : TagHelper
    {
        public ModelExpression aspFor { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator _generator { get; set; }

        public DisplayTitleTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";
            var propMetadata = aspFor.Metadata;
            var @class = context.AllAttributes["class"].Value;

            var label = _generator.GenerateLabel(ViewContext, aspFor.ModelExplorer,
                propMetadata.Name, propMetadata.Name, new { @class });

            var strong = new TagBuilder("strong");
            strong.InnerHtml.Append(propMetadata.DisplayName);
            label.InnerHtml.Clear();
            label.InnerHtml.AppendHtml(strong);

            if (propMetadata.IsRequired)
            {
                var span = new TagBuilder("span");
                span.AddCssClass("text-danger");
                span.InnerHtml.Append("*");

                label.InnerHtml.AppendHtml(span);
            }

            output.Content.AppendHtml(label);


            if (string.IsNullOrEmpty(propMetadata.Description) == false)
            {
                var span = new TagBuilder("span");
                span.AddCssClass("text-success");
                span.InnerHtml.Append(propMetadata.Description);

                output.Content.AppendHtml(span);
            }

            var validation = _generator.GenerateValidationMessage(ViewContext, aspFor.ModelExplorer,
                propMetadata.Name, string.Empty, string.Empty, new { @class = "text-danger" });

            output.Content.AppendHtml(validation);

            base.Process(context, output);
        }
    }
}

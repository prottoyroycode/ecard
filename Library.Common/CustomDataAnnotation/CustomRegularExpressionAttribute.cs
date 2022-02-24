using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.CustomDataAnnotation
{
    /// <summary>
    /// <see cref="Use example:"/>
    /// <para>[CustomRegularExpression(@"^[a-z]{2,4}$", "DependentField", "Value")]</para>
    /// <para>[CustomRegularExpression(@"^[a-z]{2,4}$", "DependentField", new[] { "", "Value1", "Value1" })]</para>
    /// <para>public string Code { get; set; }</para>
    /// <para>public CategoryType CategoryType { get; set; }</para>
    /// </summary>
    public class CustomRegularExpressionAttribute : ConditionalValidationAttribute
    {
        private readonly string pattern;
        protected override string ValidationName
        {
            get { return "customregularexpression"; }
        }
        public CustomRegularExpressionAttribute(string pattern, string dependentProperty, object targetValue)
            : base(new RegularExpressionAttribute(pattern), dependentProperty, targetValue)
        {
            this.pattern = pattern;
        }
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            // Set the rule RegEx and the rule param pattern
            return new Dictionary<string, object>
            {
                {"rule", "regex"},
                { "ruleparam", pattern }
            };
        }
    }
}

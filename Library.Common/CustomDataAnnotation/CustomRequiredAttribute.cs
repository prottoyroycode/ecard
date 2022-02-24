using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.CustomDataAnnotation
{
    /// <summary>
    /// Check its validation by this or any other properties value.
    /// <para>[CustomRequired("DependentField", "Value")]</para>
    /// <para>public bool IsActive { get; set; }</para>
    /// </summary>
    /// <returns>
    /// validated or not.
    /// </returns>
    /// <example>
    /// <code>
    /// [CustomRequired("DependentField", "Value")]
    /// public bool IsActive { get; set; }
    /// </code>
    /// </example>
    public class CustomRequiredAttribute : ConditionalValidationAttribute
    {
        protected override string ValidationName
        {
            get { return "customrequired"; }
        }
        public CustomRequiredAttribute(string dependentProperty, object targetValue)
            : base(new RequiredAttribute(), dependentProperty, targetValue)
        {
        }
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>
            {
                { "rule", "required" }
            };
        }
    }
}

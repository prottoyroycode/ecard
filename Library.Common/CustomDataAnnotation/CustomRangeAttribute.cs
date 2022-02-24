using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.CustomDataAnnotation
{
    /// <summary>
    /// <see cref="Use example:"/>
    /// public class Category : IEntityModel
    /// {
    ///     [CustomRange(1, int.MaxValue, "IsSelectable", true)]
    ///     public int SLA { get; set; }
    ///     public bool IsSelectable { get; set; }
    /// }
    /// </summary>
    public class CustomRangeAttribute : ConditionalValidationAttribute
    {
        private readonly int minimum;
        private readonly int maximum;
        protected override string ValidationName
        {
            get { return "customrange"; }
        }
        public CustomRangeAttribute(int minimum, int maximum, string dependentProperty, object targetValue)
            : base(new RangeAttribute(minimum, maximum), dependentProperty, targetValue)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            // Set the rule Range and the rule param [minumum,maximum]
            return new Dictionary<string, object>
            {
                {"rule", "range"},
                { "ruleparam", string.Format("[{0},{1}]", this.minimum, this.maximum) }
            };
        }
    }
}

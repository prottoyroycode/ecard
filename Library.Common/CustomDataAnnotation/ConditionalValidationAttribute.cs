﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.CustomDataAnnotation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class ConditionalValidationAttribute : ValidationAttribute
    {
        protected readonly ValidationAttribute InnerAttribute;
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }
        protected abstract string ValidationName { get; }

        //Required if you want to use this attribute multiple times
        private readonly object _typeId = new object();  
        public override object TypeId
        {
            get { return _typeId; }
        }   
        protected virtual IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>();
        }

        protected ConditionalValidationAttribute(ValidationAttribute innerAttribute, string dependentProperty, object targetValue)
        {
            this.InnerAttribute = innerAttribute;
            this.DependentProperty = dependentProperty;
            this.TargetValue = targetValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get a reference to the property this validation depends upon
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);
            if (field != null)
            {
                // get the value of the dependent property
                var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);

                // compare the value against the target value
                if ((dependentvalue == null && this.TargetValue == null) || (dependentvalue != null && dependentvalue.Equals(this.TargetValue)))
                {
                    // match => means we should try validating this field
                    if (!InnerAttribute.IsValid(value))
                    {
                        // validation failed - return an error
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }
            return ValidationResult.Success;
        }

        
    }
}

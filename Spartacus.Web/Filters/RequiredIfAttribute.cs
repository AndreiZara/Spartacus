using Spartacus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Filters
{
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly RequiredAttribute _innerAttribute = new RequiredAttribute();

        internal string _dependentProperty;
        internal object _targetValue;

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            _dependentProperty = dependentProperty;
            _targetValue = targetValue;
        }

        /// <summary>
        /// Returns if the given validation result is valid. It checks if the RequiredIfAttribute needs to be validated
        /// </summary>
        /// <param name="value">Value of the control</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if ((dependentValue == null && _targetValue == null) || (dependentValue.ToString() == _targetValue.ToString()))
                {
                    if (!_innerAttribute.IsValid(value))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                return ValidationResult.Success;
            }
            else
            {
                throw new ValidationException("RequiredIf Dependant Property " + _dependentProperty + " does not exist");
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "requiredif",
            };
            rule.ValidationParameters["dependentproperty"] = (context as ViewContext).ViewData.TemplateInfo.GetFullHtmlFieldId(_dependentProperty);
            rule.ValidationParameters["desiredvalue"] = _targetValue is bool ? _targetValue.ToString().ToLower() : _targetValue;

            yield return rule;
        }
    }
}
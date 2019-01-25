using System.ComponentModel.DataAnnotations;

namespace EventPlanning.Annotations
{
    public class FieldsAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string[] fieldAttribute = (string[])value;
            if (fieldAttribute != null) 
            for (int i = 0; i < fieldAttribute.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(fieldAttribute[i]))
                    return false;
            }

            return true;
        }
    }
}
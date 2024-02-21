using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace NewBlog.Extensions
{
    public  static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var item in modelState.Values)
                result.AddRange(item.Errors.Select(error => error.ErrorMessage)); //same as doing another foreach

            return result;
        }
    }
}

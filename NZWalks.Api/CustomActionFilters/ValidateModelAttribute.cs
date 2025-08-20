using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.CustomActionFilters
{
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        //Only runs if u didnt use [ApiController] attribute otherwise modelstates are checked by default
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid is false)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
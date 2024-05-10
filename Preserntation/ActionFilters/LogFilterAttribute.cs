﻿using Entities.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class LogFilterAttribute :ActionFilterAttribute
    {
        private readonly ILogerService _logger;

        public LogFilterAttribute(ILogerService logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInfo(Log("OnActionExecuted", context.RouteData));
        }

        private string Log(string modelName, RouteData routeData) 
        {
            var logDetails = new LogDetails()
            {
                ModelName = modelName,
                Controller = routeData.Values["controller"],
                Action = routeData.Values["action"]
            };

            if (routeData.Values.Count >= 3)
                logDetails.Id = routeData.Values["Id"];

            return logDetails.ToString();

        }
    }
}

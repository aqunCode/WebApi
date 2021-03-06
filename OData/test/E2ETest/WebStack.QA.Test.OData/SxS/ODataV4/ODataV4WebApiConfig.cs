﻿using System.Web.Http;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using WebStack.QA.Test.OData.SxS.ODataV4.Extensions;

namespace WebStack.QA.Test.OData.SxS.ODataV4
{
    public static class ODataV4WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var model = WebStack.QA.Test.OData.SxS.ODataV4.Models.ModelBuilder.GetEdmModel();
            var conventions = ODataRoutingConventions.CreateDefaultWithAttributeRouting(config, model);
            conventions.Add(new EntitySetVersioningRoutingConvention("V2"));

            var odataRoute = config.MapODataServiceRoute(
                routeName: "SxSODataV4",
                routePrefix: "SxSOData",
                model: model,
                pathHandler: new DefaultODataPathHandler(),
                routingConventions: conventions);

            var constraint = new ODataVersionRouteConstraint(new { v = "2" });
            odataRoute.Constraints.Add("VersionConstraintV2", constraint);
        }
    }
}

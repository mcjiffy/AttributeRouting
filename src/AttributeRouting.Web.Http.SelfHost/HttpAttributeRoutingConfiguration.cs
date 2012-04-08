﻿using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using AttributeRouting.Framework.Factories;
using AttributeRouting.Web.Http.SelfHost.Framework;
using AttributeRouting.Web.Http.SelfHost.Framework.Factories;

namespace AttributeRouting.Web.Http.SelfHost {
    public class HttpAttributeRoutingConfiguration : AttributeRoutingConfiguration<AttributeRoute, RouteParameter, HttpRequestMessage, IHttpRouteData>
    {
        private readonly IAttributeRouteFactory _attributeFactory;
        private readonly IConstraintFactory _constraintFactory;
        private readonly IParameterFactory<RouteParameter> _parameterFactory;

        public HttpAttributeRoutingConfiguration() {
            _attributeFactory = new AttributeRouteFactory(this);
            _constraintFactory = new HttpRouteConstraintFactory();
            _parameterFactory = new RouteParameterFactory();
        }

        public override Type FrameworkControllerType {
            get { return typeof (IHttpController); }
        }

        /// <summary>
        /// Attribute factory
        /// </summary>
        public override IAttributeRouteFactory AttributeFactory {
            get { return _attributeFactory; }
        }

        /// <summary>
        /// Constraint factory
        /// </summary>
        public override IConstraintFactory ConstraintFactory {
            get { return _constraintFactory; }
        }

        /// <summary>
        /// Parameter factory
        /// </summary>
        public override IParameterFactory<RouteParameter> ParameterFactory {
            get { return _parameterFactory; }
        }

        /// <summary>
        /// Scans the assembly of the specified controller for routes to register.
        /// </summary>
        /// <typeparam name="T">The type of the controller used to specify the assembly</typeparam>
        public void ScanAssemblyOf<T>() where T : IHttpController {
            ScanAssembly(typeof(T).Assembly);
        }

        /// <summary>
        /// Adds all the routes for the specified controller type to the end of the route collection.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        public void AddRoutesFromController<T>() where T : IHttpController {
            AddRoutesFromController(typeof(T));
        }

        /// <summary>
        /// Adds all the routes for all the controllers that derive from the specified controller
        /// to the end of the route collection.
        /// </summary>
        /// <typeparam name="T">The base controller type</typeparam>
        public void AddRoutesFromControllersOfType<T>() where T : IHttpController {
            AddRoutesFromControllersOfType(typeof(T));
        }

        /// <summary>
        /// Automatically applies the specified constaint against url parameters
        /// with names that match the given regular expression.
        /// </summary>
        /// <param name="keyRegex">The regex used to match url parameter names</param>
        /// <param name="constraint">The constraint to apply to matched parameters</param>
        public void AddDefaultRouteConstraint(string keyRegex, IHttpRouteConstraint constraint) {
            base.AddDefaultRouteConstraint(keyRegex, constraint);
        }
    }
}
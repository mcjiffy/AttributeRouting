using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AttributeRouting
{
    /// <summary>
    /// The route information for an action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public abstract class RouteAttributeBase : IRouteAttribute
    {
        /// <summary>
        /// Specify the route information for an action.
        /// </summary>
        /// <param name="routeUrl">The url that is associated with this action</param>
        protected RouteAttributeBase(string routeUrl)
        {
            if (routeUrl == null) throw new ArgumentNullException("routeUrl");

            RouteUrl = routeUrl;
            Order = int.MaxValue;
            Precedence = int.MaxValue;
            HttpMethods = new string[0];
        }

        /// <summary>
        /// Specify the route information for an action.
        /// </summary>
        /// <param name="routeUrl">The url that is associated with this action</param>
        /// <param name="allowedMethods">The httpMethods against which to constrain the route</param>
        protected RouteAttributeBase(string routeUrl, params string[] allowedMethods)
            : this(routeUrl)
        {
            HttpMethods = allowedMethods;

            if (HttpMethods.Any(m => !Regex.IsMatch(m, "HEAD|GET|POST|PUT|DELETE|PATCH|OPTIONS|TRACE")))
                throw new InvalidOperationException(
                    "The allowedMethods are restricted to either HEAD, GET, POST, PUT, DELETE, PATCH, OPTIONS, or TRACE.");
        }

        public string RouteUrl { get; private set; }

        public string[] HttpMethods { get; protected set; }

        public int Order { get; set; }

        public int Precedence { get; set; }

        public string RouteName { get; set; }

        public bool IsAbsoluteUrl { get; set; }

        public string TranslationKey { get; set; }

        public bool UseLowercaseRoute
        {
            set { UseLowercaseRouteFlag = value; }
        }

        public bool? UseLowercaseRouteFlag { get; private set; }

        public bool PreserveCaseForUrlParameters 
        {
            set { PreserveCaseForUrlParametersFlag = value; }
        }
        
        public bool? PreserveCaseForUrlParametersFlag { get; private set; }

        public bool AppendTrailingSlash
        {
            set { AppendTrailingSlashFlag = value; }
        }

        public bool? AppendTrailingSlashFlag { get; private set; }
    }
}
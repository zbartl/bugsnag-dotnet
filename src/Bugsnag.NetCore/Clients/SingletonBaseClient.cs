﻿using System;
using System.Threading.Tasks;

namespace Bugsnag.NetCore.Clients
{
    public static class SingletonBaseClient
    {
        /// <summary>
        /// Gets the static Client if configured.
        /// </summary>
        public static BaseClient Client { get; set; }
        public static Configuration Config
        {
            get
            {
                if (Client != null)
                {
                    return Client.Config;
                }
                return null;
            }
        }

        /// <summary>
        /// Notifies Bugsnag of an exception
        /// </summary>
        /// <param name="exception">The exception to send to Bugsnag</param>
        public static async Task Notify(Exception exception)
        {
            await Client.Notify(exception);
        }

        /// <summary>
        /// Notifies Bugsnag of an exception, with an associated severity level
        /// </summary>
        /// <param name="exception">The exception to send to Bugsnag</param>
        /// <param name="severity">The associated severity of the exception</param>
        public static async Task Notify(Exception exception, Severity severity)
        {
            await Client.Notify(exception, severity);
        }

        /// <summary>
        /// Notifies Bugsnag of an exception with associated meta data
        /// </summary>
        /// <param name="exception">The exception to send to Bugsnag</param>
        /// <param name="data">The metadata to send with the exception</param>
        public static async Task Notify(Exception exception, Metadata data)
        {
            await Client.Notify(exception, data);
        }

        /// <summary>
        /// Notifies Bugsnag of an exception, with an associated severity level and meta data
        /// </summary>
        /// <param name="exception">The exception to send to Bugsnag</param>
        /// <param name="severity">The associated severity of the exception</param>
        /// <param name="data">The metadata to send with the exception</param>
        public static async Task Notify(Exception exception, Severity severity, Metadata data)
        {
            await Client.Notify(exception, severity, data);
        }

        /// <summary>
        /// Notifies Bugsnag of an error event
        /// </summary>
        /// <param name="errorEvent">The event to report on</param>
        public static async Task Notify(Event errorEvent)
        {
            await Client.Notify(errorEvent);
        }
    }
}

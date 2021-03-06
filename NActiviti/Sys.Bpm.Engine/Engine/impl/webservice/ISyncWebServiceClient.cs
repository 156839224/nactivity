﻿/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Concurrent;

namespace Sys.Workflow.Engine.Impl.Webservice
{

    /// <summary>
    /// A dynamic web service client that allows to perform synchronous calls to a specific web service.
    /// 
    /// 
    /// </summary>
    public interface ISyncWebServiceClient
    {

        /// <summary>
        /// Synchronously invoke a web service method with some arguments.
        /// </summary>
        /// <param name="methodName">
        ///          a not null method name </param>
        /// <param name="arguments">
        ///          a not null list of arguments </param>
        /// <param name="overridenEndpointAddresses"> 
        ///          a not null map of overriden enpoint addresses. The key is the endpoint qualified name. </param>
        /// <returns> the result of invoking the method of the web service </returns>
        object[] send(string methodName, object[] arguments, ConcurrentDictionary<string, Uri> overridenEndpointAddresses);
    }

}
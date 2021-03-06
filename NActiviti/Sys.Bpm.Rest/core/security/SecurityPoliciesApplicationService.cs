﻿/*
 * Licensed under the Apache License, Version 2.0 (the "License");
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
 *
 */

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Sys.Workflow;

namespace Sys.Workflow.Cloud.Services.Core
{
    /// <summary>
    /// authentication midlleware
    /// </summary>
    public class SecurityPoliciesApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        public SecurityPoliciesApplicationService()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public IUserInfo User
        {
            get; set;
        }
    }
}
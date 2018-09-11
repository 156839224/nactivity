﻿#region License

/*
 * Copyright © 2010-2011 the original author or authors.
 *
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
 */

#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Spring.Logging;
using Spring.Objects.Factory.Support;
namespace Spring.Objects.Factory.Attributes
{
    /// <summary>
    /// Internal class for managing injection metadata.
    /// Not intended for direct use in applications.
    /// </summary>
    public class InjectionMetadata
    {
        private static readonly ILogger Logger = NoneLoggerFactory.Instance. GetLogger<InjectionMetadata>();

        private readonly IList<InjectedElement> _injectedElements;


        /// <summary>
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="elements"></param>
        public InjectionMetadata(Type targetType, IList<InjectedElement> elements)
        {
            _injectedElements = new List<InjectedElement>();
            if (elements.Count > 0)
            {
                foreach (var element in elements)
                {
                    Logger.LogDebug("Found injected element on class [" + targetType.Name + "]: " + element);
                    _injectedElements.Add(element);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="objectDefinition"></param>
        public void CheckConfigMembers(RootObjectDefinition objectDefinition)
        {
        }


	    /// <summary>
	    /// Inject values for members into object instance
	    /// </summary>
	    /// <param name="instance"></param>
	    /// <param name="objectName"></param>
	    /// <param name="pvs"></param>
	    public void Inject(Object instance, string objectName, IPropertyValues pvs)
	    {
	        if (_injectedElements.Count == 0)
                return;

	        foreach(var element in _injectedElements)
	        {
	            Logger.LogDebug(string.Format("Processing injected method of bean '{0}': {1}", objectName, element));
	            element.Inject(instance, objectName, pvs);
	        }
	    }

        /// <summary>
        /// Represents an element that needs to be injected
        /// </summary>
        public abstract class InjectedElement
        {
            /// <summary>
            /// The Property, field, method or constructor info
            /// </summary>
            private readonly MemberInfo _member;

            /// <summary>
            /// Instantiates a new inject element
            /// </summary>
            /// <param name="member"></param>
            protected InjectedElement(MemberInfo member)
            {
                _member = member;
            }

            public MemberInfo Member
            {
                get { return _member; }
            }

            /// <summary>
            /// Ececuted to inject value to associated memeber info
            /// </summary>
            /// <param name="target"></param>
            /// <param name="requestingObjectName"></param>
            /// <param name="pvs"></param>
            public abstract void Inject(object target, string requestingObjectName, IPropertyValues pvs);
        }
    }
}
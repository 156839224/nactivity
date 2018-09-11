﻿using Microsoft.Extensions.Logging;
using Sys;
using System;
using System.Collections.Generic;

/* Licensed under the Apache License, Version 2.0 (the "License");
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
namespace org.activiti.engine.@delegate.@event.impl
{

    /// <summary>
    /// Class that allows adding and removing event listeners and dispatching events to the appropriate listeners.
    /// 
    /// 
    /// </summary>
    public class ActivitiEventSupport
    {
        protected internal IList<IActivitiEventListener> eventListeners;
        protected internal IDictionary<ActivitiEventType, IList<IActivitiEventListener>> typedListeners;

        private ILogger<ActivitiEventSupport> log = ProcessEngineServiceProvider.LoggerService<ActivitiEventSupport>();

        public ActivitiEventSupport()
        {
            eventListeners = new SynchronizedCollection<IActivitiEventListener>();
            typedListeners = new Dictionary<ActivitiEventType, IList<IActivitiEventListener>>();
        }

        public virtual void addEventListener(IActivitiEventListener listenerToAdd)
        {
            lock (this)
            {
                if (listenerToAdd == null)
                {
                    throw new ActivitiIllegalArgumentException("Listener cannot be null.");
                }
                if (!eventListeners.Contains(listenerToAdd))
                {
                    eventListeners.Add(listenerToAdd);
                }
            }
        }

        public virtual void addEventListener(IActivitiEventListener listenerToAdd, params ActivitiEventType[] types)
        {
            lock (this)
            {
                if (listenerToAdd == null)
                {
                    throw new ActivitiIllegalArgumentException("Listener cannot be null.");
                }

                if (types == null || types.Length == 0)
                {
                    addEventListener(listenerToAdd);

                }
                else
                {
                    foreach (ActivitiEventType type in types)
                    {
                        addTypedEventListener(listenerToAdd, type);
                    }
                }
            }
        }

        public virtual void removeEventListener(IActivitiEventListener listenerToRemove)
        {
            eventListeners.Remove(listenerToRemove);

            foreach (IList<IActivitiEventListener> listeners in typedListeners.Values)
            {
                listeners.Remove(listenerToRemove);
            }
        }

        public virtual void dispatchEvent(IActivitiEvent @event)
        {
            if (@event == null)
            {
                throw new ActivitiIllegalArgumentException("Event cannot be null.");
            }

            if (@event.Type == null)
            {
                throw new ActivitiIllegalArgumentException("Event type cannot be null.");
            }

            // Call global listeners
            if (eventListeners.Count > 0)
            {
                foreach (IActivitiEventListener listener in eventListeners)
                {
                    dispatchEvent(@event, listener);
                }
            }

            // Call typed listeners, if any
            typedListeners.TryGetValue(@event.Type, out IList<IActivitiEventListener> typed);
            if (typed != null && typed.Count > 0)
            {
                foreach (IActivitiEventListener listener in typed)
                {
                    dispatchEvent(@event, listener);
                }
            }
        }

        protected internal virtual void dispatchEvent(IActivitiEvent @event, IActivitiEventListener listener)
        {
            try
            {
                listener.onEvent(@event);
            }
            catch (Exception t)
            {
                if (listener.FailOnException)
                {
                    throw new ActivitiException("Exception while executing event-listener", t);
                }
                else
                {
                    // Ignore the exception and continue notifying remaining listeners. The listener
                    // explicitly states that the exception should not bubble up
                    log.LogError(t, $"Exception while executing event-listener, which was ignored");
                }
            }
        }

        protected internal virtual void addTypedEventListener(IActivitiEventListener listener, ActivitiEventType type)
        {
            lock (this)
            {
                typedListeners.TryGetValue(type, out IList<IActivitiEventListener> listeners);
                if (listeners == null)
                {
                    // Add an empty list of listeners for this type
                    listeners = new SynchronizedCollection<IActivitiEventListener>();
                    typedListeners[type] = listeners;
                }

                if (!listeners.Contains(listener))
                {
                    listeners.Add(listener);
                }
            }
        }
    }

}
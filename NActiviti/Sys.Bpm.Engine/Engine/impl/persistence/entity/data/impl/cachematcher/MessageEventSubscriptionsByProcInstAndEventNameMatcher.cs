﻿using System.Collections.Generic;

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
namespace org.activiti.engine.impl.persistence.entity.data.impl.cachematcher
{


    /// 
    public class MessageEventSubscriptionsByProcInstAndEventNameMatcher : CachedEntityMatcherAdapter<IEventSubscriptionEntity>
    {

        public override bool isRetained(IEventSubscriptionEntity eventSubscriptionEntity, object param)
        {
            IDictionary<string, object> paramMap = (IDictionary<string, object>)param ?? new Dictionary<string, object>();
            paramMap.TryGetValue("processInstanceId", out object processInstanceId);
            paramMap.TryGetValue("eventName", out object eventName);

            return eventSubscriptionEntity.EventType != null &&
                string.Compare(eventSubscriptionEntity.EventType, MessageEventSubscriptionEntity_Fields.EVENT_TYPE, true) ==0 &&
                eventSubscriptionEntity.EventName != null &&
                string.Compare(eventSubscriptionEntity.EventName, eventName?.ToString(), true) == 0 &&
                eventSubscriptionEntity.ProcessInstanceId != null && 
                string.Compare(eventSubscriptionEntity.ProcessInstanceId, processInstanceId?.ToString(), true) == 0;
        }

    }
}
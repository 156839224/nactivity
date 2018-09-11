﻿using System;
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
namespace org.activiti.engine.impl.bpmn.behavior
{

    using org.activiti.bpmn.model;
    using org.activiti.engine.@delegate;
    using org.activiti.engine.@delegate.@event;
    using org.activiti.engine.@delegate.@event.impl;
    using org.activiti.engine.history;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.impl.persistence.entity;

    [Serializable]
    public class IntermediateCatchMessageEventActivityBehavior : IntermediateCatchEventActivityBehavior
    {

        private const long serialVersionUID = 1L;

        protected internal MessageEventDefinition messageEventDefinition;

        public IntermediateCatchMessageEventActivityBehavior(MessageEventDefinition messageEventDefinition)
        {
            this.messageEventDefinition = messageEventDefinition;
        }

        public override void execute(IExecutionEntity execution)
        {
            ICommandContext commandContext = Context.CommandContext;

            string messageName = null;
            if (!string.IsNullOrWhiteSpace(messageEventDefinition.MessageRef))
            {
                messageName = messageEventDefinition.MessageRef;
            }
            else
            {
                IExpression messageExpression = commandContext.ProcessEngineConfiguration.ExpressionManager.createExpression(messageEventDefinition.MessageExpression);
                messageName = messageExpression.getValue(execution).ToString();
            }

            commandContext.EventSubscriptionEntityManager.insertMessageEvent(messageName, execution);

            if (commandContext.ProcessEngineConfiguration.EventDispatcher.Enabled)
            {
                commandContext.ProcessEngineConfiguration.EventDispatcher.dispatchEvent(ActivitiEventBuilder.createMessageEvent(ActivitiEventType.ACTIVITY_MESSAGE_WAITING, execution.ActivityId, messageName, null, execution.Id, execution.ProcessInstanceId, execution.ProcessDefinitionId));
            }
        }

        public override void trigger(IExecutionEntity execution, string triggerName, object triggerData)
        {
            IExecutionEntity executionEntity = deleteMessageEventSubScription(execution);
            leaveIntermediateCatchEvent(executionEntity);
        }

        public override void eventCancelledByEventGateway(IExecutionEntity execution)
        {
            deleteMessageEventSubScription(execution);
            Context.CommandContext.ExecutionEntityManager.deleteExecutionAndRelatedData(execution, DeleteReason_Fields.EVENT_BASED_GATEWAY_CANCEL, false);
        }

        protected internal virtual IExecutionEntity deleteMessageEventSubScription(IExecutionEntity execution)
        {
            IEventSubscriptionEntityManager eventSubscriptionEntityManager = Context.CommandContext.EventSubscriptionEntityManager;
            IList<IEventSubscriptionEntity> eventSubscriptions = execution.EventSubscriptions;
            foreach (IEventSubscriptionEntity eventSubscription in eventSubscriptions)
            {
                if (eventSubscription is IMessageEventSubscriptionEntity && eventSubscription.EventName.Equals(messageEventDefinition.MessageRef))
                {

                    eventSubscriptionEntityManager.delete(eventSubscription);
                }
            }
            return execution;
        }
    }

}
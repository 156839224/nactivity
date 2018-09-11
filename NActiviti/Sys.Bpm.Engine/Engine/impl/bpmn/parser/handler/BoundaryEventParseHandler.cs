﻿using System;

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
namespace org.activiti.engine.impl.bpmn.parser.handler
{
    using org.activiti.bpmn.model;

    /// 
    /// 
    public class BoundaryEventParseHandler : AbstractFlowNodeBpmnParseHandler<BoundaryEvent>
    {

        protected internal override Type HandledType
        {
            get
            {
                return typeof(BoundaryEvent);
            }
        }

        protected internal override void executeParse(BpmnParse bpmnParse, BoundaryEvent boundaryEvent)
        {

            if (boundaryEvent.AttachedToRef == null)
            {
                //logger.warn("Invalid reference in boundary event. Make sure that the referenced activity " + "is defined in the same scope as the boundary event " + boundaryEvent.Id);
                return;
            }

            EventDefinition eventDefinition = null;
            if (boundaryEvent.EventDefinitions.Count > 0)
            {
                eventDefinition = boundaryEvent.EventDefinitions[0];
            }

            if (eventDefinition is TimerEventDefinition || eventDefinition is ErrorEventDefinition || eventDefinition is SignalEventDefinition || eventDefinition is CancelEventDefinition || eventDefinition is MessageEventDefinition || eventDefinition is CompensateEventDefinition)
            {

                bpmnParse.BpmnParserHandlers.parseElement(bpmnParse, eventDefinition);

            }
            else
            {
                // Should already be picked up by process validator on deploy, so this is just to be sure
                //logger.warn("Unsupported boundary event type for boundary event " + boundaryEvent.Id);
            }

        }

    }

}
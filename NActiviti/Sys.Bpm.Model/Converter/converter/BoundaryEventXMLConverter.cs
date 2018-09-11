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
namespace org.activiti.bpmn.converter
{

    using org.activiti.bpmn.converter.util;
    using org.activiti.bpmn.model;

    /// 
    public class BoundaryEventXMLConverter : BaseBpmnXMLConverter
    {

        public override Type BpmnElementType
        {
            get
            {
                return typeof(BoundaryEvent);
            }
        }

        public override string XMLElementName
        {
            get
            {
                return org.activiti.bpmn.constants.BpmnXMLConstants.ELEMENT_EVENT_BOUNDARY;
            }
        }
        protected internal override BaseElement convertXMLToElement(XMLStreamReader xtr, BpmnModel model)
        {
            BoundaryEvent boundaryEvent = new BoundaryEvent();
            BpmnXMLUtil.addXMLLocation(boundaryEvent, xtr);
            if (!string.IsNullOrWhiteSpace(xtr.getAttributeValue(org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_BOUNDARY_CANCELACTIVITY)))
            {
                string cancelActivity = xtr.getAttributeValue(org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_BOUNDARY_CANCELACTIVITY);
                if (org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_VALUE_FALSE.Equals(cancelActivity, StringComparison.CurrentCultureIgnoreCase))
                {
                    boundaryEvent.CancelActivity = false;
                }
            }
            boundaryEvent.AttachedToRefId = xtr.getAttributeValue(org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_BOUNDARY_ATTACHEDTOREF);
            parseChildElements(XMLElementName, boundaryEvent, model, xtr);

            // Explicitly set cancel activity to false for error boundary events
            if (boundaryEvent.EventDefinitions.Count == 1)
            {
                EventDefinition eventDef = boundaryEvent.EventDefinitions[0];

                if (eventDef is ErrorEventDefinition)
                {
                    boundaryEvent.CancelActivity = false;
                }
            }

            return boundaryEvent;
        }
        protected internal override void writeAdditionalAttributes(BaseElement element, BpmnModel model, XMLStreamWriter xtw)
        {
            BoundaryEvent boundaryEvent = (BoundaryEvent)element;
            if (boundaryEvent.AttachedToRef != null)
            {
                writeDefaultAttribute(org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_BOUNDARY_ATTACHEDTOREF, boundaryEvent.AttachedToRef.Id, xtw);
            }

            if (boundaryEvent.EventDefinitions.Count == 1)
            {
                EventDefinition eventDef = boundaryEvent.EventDefinitions[0];

                if (eventDef is ErrorEventDefinition == false)
                {
                    writeDefaultAttribute(org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_BOUNDARY_CANCELACTIVITY, boundaryEvent.CancelActivity.ToString().ToLower(), xtw);
                }
            }
        }
        protected internal override void writeAdditionalChildElements(BaseElement element, BpmnModel model, XMLStreamWriter xtw)
        {
            BoundaryEvent boundaryEvent = (BoundaryEvent)element;
            writeEventDefinitions(boundaryEvent, boundaryEvent.EventDefinitions, model, xtw);
        }
    }

}
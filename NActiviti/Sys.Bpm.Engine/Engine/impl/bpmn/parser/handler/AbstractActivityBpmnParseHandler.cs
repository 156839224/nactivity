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
namespace org.activiti.engine.impl.bpmn.parser.handler
{
    using org.activiti.bpmn.model;
    using org.activiti.engine.impl.bpmn.behavior;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.el;

    /// 
    public abstract class AbstractActivityBpmnParseHandler<T> : AbstractFlowNodeBpmnParseHandler<T> where T : org.activiti.bpmn.model.FlowNode
    {

        public override void parse(BpmnParse bpmnParse, BaseElement element)
        {
            base.parse(bpmnParse, element);

            if (element is Activity && ((Activity)element).LoopCharacteristics != null)
            {
                createMultiInstanceLoopCharacteristics(bpmnParse, (Activity)element);
            }
        }

        protected internal virtual void createMultiInstanceLoopCharacteristics(BpmnParse bpmnParse, Activity modelActivity)
        {

            MultiInstanceLoopCharacteristics loopCharacteristics = modelActivity.LoopCharacteristics;

            // Activity Behavior
            MultiInstanceActivityBehavior miActivityBehavior = null;

            if (loopCharacteristics.Sequential)
            {
                miActivityBehavior = bpmnParse.ActivityBehaviorFactory.createSequentialMultiInstanceBehavior(modelActivity, (AbstractBpmnActivityBehavior)modelActivity.Behavior);
            }
            else
            {
                miActivityBehavior = bpmnParse.ActivityBehaviorFactory.createParallelMultiInstanceBehavior(modelActivity, (AbstractBpmnActivityBehavior)modelActivity.Behavior);
            }

            modelActivity.Behavior = miActivityBehavior;

            ExpressionManager expressionManager = Context.ProcessEngineConfiguration.ExpressionManager;

            // loop cardinality
            if (!string.IsNullOrWhiteSpace(loopCharacteristics.LoopCardinality))
            {
                miActivityBehavior.LoopCardinalityExpression = expressionManager.createExpression(loopCharacteristics.LoopCardinality);
            }

            // completion condition
            if (!string.IsNullOrWhiteSpace(loopCharacteristics.CompletionCondition))
            {
                miActivityBehavior.CompletionConditionExpression = expressionManager.createExpression(loopCharacteristics.CompletionCondition);
            }

            // activiti:collection
            if (!string.IsNullOrWhiteSpace(loopCharacteristics.InputDataItem))
            {
                if (loopCharacteristics.InputDataItem.Contains("{"))
                {
                    miActivityBehavior.CollectionExpression = expressionManager.createExpression(loopCharacteristics.InputDataItem);
                }
                else
                {
                    miActivityBehavior.CollectionVariable = loopCharacteristics.InputDataItem;
                }
            }

            // activiti:elementVariable
            if (!string.IsNullOrWhiteSpace(loopCharacteristics.ElementVariable))
            {
                miActivityBehavior.CollectionElementVariable = loopCharacteristics.ElementVariable;
            }

            // activiti:elementIndexVariable
            if (!string.IsNullOrWhiteSpace(loopCharacteristics.ElementIndexVariable))
            {
                miActivityBehavior.CollectionElementIndexVariable = loopCharacteristics.ElementIndexVariable;
            }

        }
    }

}
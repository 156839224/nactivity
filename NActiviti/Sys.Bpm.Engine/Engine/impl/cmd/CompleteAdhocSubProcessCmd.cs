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

namespace org.activiti.engine.impl.cmd
{
    using org.activiti.bpmn.model;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.impl.persistence.entity;


    /// 
    [Serializable]
    public class CompleteAdhocSubProcessCmd : ICommand<object>
    {

        private const long serialVersionUID = 1L;

        protected internal string executionId;

        public CompleteAdhocSubProcessCmd(string executionId)
        {
            this.executionId = executionId;
        }

        public virtual object execute(ICommandContext commandContext)
        {
            IExecutionEntityManager executionEntityManager = commandContext.ExecutionEntityManager;
            IExecutionEntity execution = executionEntityManager.findById<IExecutionEntity>(new KeyValuePair<string, object>("id", executionId));
            if (execution == null)
            {
                throw new ActivitiObjectNotFoundException("No execution found for id '" + executionId + "'", typeof(IExecutionEntity));
            }

            if (!(execution.CurrentFlowElement is AdhocSubProcess))
            {
                throw new ActivitiException("The current flow element of the requested execution is not an ad-hoc sub process");
            }

            IList<IExecutionEntity> childExecutions = execution.Executions;
            if (childExecutions.Count > 0)
            {
                throw new ActivitiException("Ad-hoc sub process has running child executions that need to be completed first");
            }

            IExecutionEntity outgoingFlowExecution = executionEntityManager.createChildExecution(execution.Parent);
            outgoingFlowExecution.CurrentFlowElement = execution.CurrentFlowElement;

            executionEntityManager.deleteExecutionAndRelatedData(execution, null, false);

            Context.Agenda.planTakeOutgoingSequenceFlowsOperation(outgoingFlowExecution, true);

            return null;
        }

    }

}
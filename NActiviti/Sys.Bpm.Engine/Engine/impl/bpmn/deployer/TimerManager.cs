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
namespace org.activiti.engine.impl.bpmn.deployer
{

    using org.activiti.bpmn.model;
    using org.activiti.engine.impl.asyncexecutor;
    using org.activiti.engine.impl.cmd;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.jobexecutor;
    using org.activiti.engine.impl.persistence.entity;
    using org.activiti.engine.impl.util;

    /// <summary>
    /// Manages timers for newly-deployed process definitions and their previous versions.
    /// </summary>
    public class TimerManager
    {

        protected internal virtual void removeObsoleteTimers(IProcessDefinitionEntity processDefinition)
        {
            IList<ITimerJobEntity> jobsToDelete = null;

            if (!string.ReferenceEquals(processDefinition.TenantId, null) && !ProcessEngineConfiguration.NO_TENANT_ID.Equals(processDefinition.TenantId))
            {
                jobsToDelete = Context.CommandContext.TimerJobEntityManager.findJobsByTypeAndProcessDefinitionKeyAndTenantId(TimerStartEventJobHandler.TYPE, processDefinition.Key, processDefinition.TenantId);
            }
            else
            {
                jobsToDelete = Context.CommandContext.TimerJobEntityManager.findJobsByTypeAndProcessDefinitionKeyNoTenantId(TimerStartEventJobHandler.TYPE, processDefinition.Key);
            }

            if (jobsToDelete != null)
            {
                foreach (ITimerJobEntity job in jobsToDelete)
                {
                    (new CancelJobsCmd(job.Id)).execute(Context.CommandContext);
                }
            }
        }

        protected internal virtual void scheduleTimers(IProcessDefinitionEntity processDefinition, Process process)
        {
            IJobManager jobManager = Context.CommandContext.JobManager;
            IList<ITimerJobEntity> timers = getTimerDeclarations(processDefinition, process);
            foreach (ITimerJobEntity timer in timers)
            {
                jobManager.scheduleTimerJob(timer);
            }
        }

        protected internal virtual IList<ITimerJobEntity> getTimerDeclarations(IProcessDefinitionEntity processDefinition, Process process)
        {
            IJobManager jobManager = Context.CommandContext.JobManager;
            IList<ITimerJobEntity> timers = new List<ITimerJobEntity>();
            if (CollectionUtil.IsNotEmpty(process.FlowElements))
            {
                foreach (FlowElement element in process.FlowElements)
                {
                    if (element is StartEvent)
                    {
                        StartEvent startEvent = (StartEvent)element;
                        if (CollectionUtil.IsNotEmpty(startEvent.EventDefinitions))
                        {
                            EventDefinition eventDefinition = startEvent.EventDefinitions[0];
                            if (eventDefinition is TimerEventDefinition)
                            {
                                TimerEventDefinition timerEventDefinition = (TimerEventDefinition)eventDefinition;
                                ITimerJobEntity timerJob = jobManager.createTimerJob(timerEventDefinition, false, null, TimerStartEventJobHandler.TYPE, TimerEventHandler.createConfiguration(startEvent.Id, timerEventDefinition.EndDate, timerEventDefinition.CalendarName));

                                if (timerJob != null)
                                {
                                    timerJob.ProcessDefinitionId = processDefinition.Id;

                                    if (!string.ReferenceEquals(processDefinition.TenantId, null))
                                    {
                                        timerJob.TenantId = processDefinition.TenantId;
                                    }
                                    timers.Add(timerJob);
                                }

                            }
                        }
                    }
                }
            }

            return timers;
        }
    }


}
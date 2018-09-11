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
namespace org.activiti.engine.impl.cmd
{
    using Microsoft.Extensions.Logging;
    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.impl.persistence.entity;
    using Sys;
    using System.Collections.Generic;

    /// 
    [Serializable]
    public class MoveTimerToExecutableJobCmd : ICommand<IJobEntity>
    {
        private static readonly ILogger<MoveTimerToExecutableJobCmd> log = ProcessEngineServiceProvider.LoggerService<MoveTimerToExecutableJobCmd>();

        private const long serialVersionUID = 1L;

        protected internal string jobId;

        public MoveTimerToExecutableJobCmd(string jobId)
        {
            this.jobId = jobId;
        }

        public  virtual IJobEntity  execute(ICommandContext  commandContext)
        {

            if (string.ReferenceEquals(jobId, null))
            {
                throw new ActivitiIllegalArgumentException("jobId and job is null");
            }

            ITimerJobEntity timerJob = commandContext.TimerJobEntityManager.findById<ITimerJobEntity>(new KeyValuePair<string, object>("id", jobId));

            if (timerJob == null)
            {
                throw new JobNotFoundException(jobId);
            }

            if (log.IsEnabled(LogLevel.Debug))
            {
                log.LogDebug($"Executing timer job {timerJob.Id}");
            }

            return commandContext.JobManager.moveTimerJobToExecutableJob(timerJob);
        }

        public virtual string JobId
        {
            get
            {
                return jobId;
            }
        }

    }

}
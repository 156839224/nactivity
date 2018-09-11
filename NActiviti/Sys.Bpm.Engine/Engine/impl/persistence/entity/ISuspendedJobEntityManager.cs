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
namespace org.activiti.engine.impl.persistence.entity
{

    using org.activiti.engine.runtime;

    /// 
    public interface ISuspendedJobEntityManager : IEntityManager<ISuspendedJobEntity>
    {

        /// <summary>
        /// Returns all <seealso cref="ISuspendedJobEntity"/> instances related to on <seealso cref="IExecutionEntity"/>.
        /// </summary>
        IList<ISuspendedJobEntity> findJobsByExecutionId(string id);

        /// <summary>
        /// Returns all <seealso cref="ISuspendedJobEntity"/> instances related to on <seealso cref="IExecutionEntity"/>. 
        /// </summary>
        IList<ISuspendedJobEntity> findJobsByProcessInstanceId(string id);

        /// <summary>
        /// Executes a <seealso cref="JobQueryImpl"/> and returns the matching <seealso cref="ISuspendedJobEntity"/> instances.
        /// </summary>
        IList<IJob> findJobsByQueryCriteria(SuspendedJobQueryImpl jobQuery, Page page);

        /// <summary>
        /// Same as <seealso cref="#findJobsByQueryCriteria(SuspendedJobQueryImpl, Page)"/>, but only returns a count 
        /// and not the instances itself.
        /// </summary>
        long findJobCountByQueryCriteria(SuspendedJobQueryImpl jobQuery);

        /// <summary>
        /// Changes the tenantId for all jobs related to a given <seealso cref="IDeploymentEntity"/>.
        /// </summary>
        void updateJobTenantIdForDeployment(string deploymentId, string newTenantId);

    }

}
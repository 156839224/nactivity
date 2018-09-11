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

	using org.activiti.engine.repository;

	/// 
	public interface IProcessDefinitionEntityManager : IEntityManager<IProcessDefinitionEntity>
	{

	  IProcessDefinitionEntity findLatestProcessDefinitionByKey(string processDefinitionKey);

	  IProcessDefinitionEntity findLatestProcessDefinitionByKeyAndTenantId(string processDefinitionKey, string tenantId);

	  IList<IProcessDefinition> findProcessDefinitionsByQueryCriteria(ProcessDefinitionQueryImpl processDefinitionQuery, Page page);

	  long findProcessDefinitionCountByQueryCriteria(ProcessDefinitionQueryImpl processDefinitionQuery);

	  IProcessDefinitionEntity findProcessDefinitionByDeploymentAndKey(string deploymentId, string processDefinitionKey);

	  IProcessDefinitionEntity findProcessDefinitionByDeploymentAndKeyAndTenantId(string deploymentId, string processDefinitionKey, string tenantId);

	  IProcessDefinition findProcessDefinitionByKeyAndVersionAndTenantId(string processDefinitionKey, int? processDefinitionVersion, string tenantId);

	  IList<IProcessDefinition> findProcessDefinitionsByNativeQuery(IDictionary<string, object> parameterMap, int firstResult, int maxResults);

	  long findProcessDefinitionCountByNativeQuery(IDictionary<string, object> parameterMap);

	  void updateProcessDefinitionTenantIdForDeployment(string deploymentId, string newTenantId);

	  void deleteProcessDefinitionsByDeploymentId(string deploymentId);

	}
}
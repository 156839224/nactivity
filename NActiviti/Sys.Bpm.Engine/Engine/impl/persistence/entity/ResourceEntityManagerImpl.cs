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

    using org.activiti.engine.impl.cfg;
    using org.activiti.engine.impl.persistence.entity.data;

    /// 
    /// 
    public class ResourceEntityManagerImpl : AbstractEntityManager<IResourceEntity>, IResourceEntityManager
    {

        protected internal IResourceDataManager resourceDataManager;

        public ResourceEntityManagerImpl(ProcessEngineConfigurationImpl processEngineConfiguration, IResourceDataManager resourceDataManager) : base(processEngineConfiguration)
        {
            this.resourceDataManager = resourceDataManager;
        }

        protected internal override IDataManager<IResourceEntity> DataManager
        {
            get
            {
                return resourceDataManager;
            }
        }

        public virtual void deleteResourcesByDeploymentId(string deploymentId)
        {
            resourceDataManager.deleteResourcesByDeploymentId(deploymentId);
        }

        public virtual IResourceEntity findResourceByDeploymentIdAndResourceName(string deploymentId, string resourceName)
        {
            return resourceDataManager.findResourceByDeploymentIdAndResourceName(deploymentId, resourceName);
        }

        public virtual IList<IResourceEntity> findResourcesByDeploymentId(string deploymentId)
        {
            return resourceDataManager.findResourcesByDeploymentId(deploymentId);
        }

        public virtual IResourceDataManager ResourceDataManager
        {
            get
            {
                return resourceDataManager;
            }
            set
            {
                this.resourceDataManager = value;
            }
        }


    }

}
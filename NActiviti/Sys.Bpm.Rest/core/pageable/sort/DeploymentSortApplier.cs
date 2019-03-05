﻿using org.activiti.api.runtime.shared.query;
using org.activiti.engine.impl;
using org.activiti.engine.query;
using org.activiti.engine.repository;
using org.activiti.engine.runtime;
using System.Collections.Generic;

/*
 * Licensed under the Apache License, Version 2.0 (the "License");
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
 *
 */

namespace org.activiti.cloud.services.core.pageable.sort
{
    public class DeploymentSortApplier : BaseSortApplier<IDeploymentQuery, IDeployment>
    {

        private IDictionary<string, IQueryProperty> orderByProperties = new Dictionary<string, IQueryProperty>();

        public DeploymentSortApplier()
        {
            orderByProperties["id"] = DeploymentQueryProperty.DEPLOYMENT_ID;
            orderByProperties["name"] = DeploymentQueryProperty.DEPLOYMENT_NAME;
        }

        protected internal override void applyDefaultSort(IDeploymentQuery query)
        {
            query.orderByDeploymentName().asc();
        }

        protected internal override IQueryProperty getOrderByProperty(Sort.Order order)
        {
            orderByProperties.TryGetValue(order.Property, out IQueryProperty qp);

            return qp;
        }
    }

}
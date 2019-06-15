﻿using org.activiti.api.runtime.shared.query;
using org.activiti.engine.history;
using org.activiti.engine.impl;
using org.activiti.engine.query;
using org.activiti.engine.task;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.activiti.cloud.services.core.pageable.sort
{
    /// <summary>
    /// 
    /// </summary>
    public class HistoryTaskSortApplier : BaseSortApplier<IHistoricTaskInstanceQuery, IHistoricTaskInstance>
    {
        private IDictionary<string, IQueryProperty> orderByProperties = new Dictionary<string, IQueryProperty>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        public HistoryTaskSortApplier()
        {
            orderByProperties["id"] = HistoricTaskInstanceQueryProperty.HISTORIC_TASK_INSTANCE_ID;
            orderByProperties["name"] = HistoricTaskInstanceQueryProperty.TASK_NAME;
            orderByProperties["assignee"] = HistoricTaskInstanceQueryProperty.TASK_ASSIGNEE;
            orderByProperties["createTime"] = HistoricTaskInstanceQueryProperty.START;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        protected internal override void ApplyDefaultSort(IHistoricTaskInstanceQuery query)
        {
            query.OrderByTaskId().Asc();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected internal override IQueryProperty GetOrderByProperty(Sort.Order order)
        {
            orderByProperties.TryGetValue(order.Property, out IQueryProperty qp);

            return qp;
        }
    }
}
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

namespace Sys.Workflow.Engine.Impl.Persistence.Entity
{

    using Sys.Workflow.Engine.History;
    using Sys.Workflow.Engine.Impl.DB;
    using Sys.Workflow.Engine.Impl.Variable;

    /// 
    /// 
    public interface IHistoricVariableInstanceEntity : IValueFields, IHistoricVariableInstance, IEntity, IHasRevision
    {
        new string Id { get; set; }

        IVariableType VariableType { get; set; }

        new string Name { get; set; }


        new string ProcessInstanceId { get; set; }

        new string TaskId { get; set; }

        new DateTime? CreateTime { get; set; }

        new DateTime? LastUpdatedTime { get; set; }

        new string ExecutionId { get; set; }


        IByteArrayRef ByteArrayRef { get; }
    }

}
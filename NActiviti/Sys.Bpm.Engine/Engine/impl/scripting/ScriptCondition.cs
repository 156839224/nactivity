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
namespace org.activiti.engine.impl.scripting
{
    using org.activiti.engine.@delegate;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.persistence.entity;

    /// 
    [Serializable]
    public class ScriptCondition : ICondition
    {

        private readonly string expression;
        private readonly string language;

        public ScriptCondition(string expression, string language)
        {
            this.expression = expression;
            this.language = language;
        }

        public virtual bool evaluate(string sequenceFlowId, IExecutionEntity execution)
        {
            //ScriptingEngines scriptingEngines = Context.ProcessEngineConfiguration.ScriptingEngines;

            //object result = scriptingEngines.evaluate(expression, language, execution);
            //if (result == null)
            //{
            //    throw new ActivitiException("condition script returns null: " + expression);
            //}
            //if (!(result is bool?))
            //{
            //    throw new ActivitiException("condition script returns non-Boolean: " + result + " (" + result.GetType().FullName + ")");
            //}
            //return bool.Parse(result?.ToString());
            return true;
        }

    }

}
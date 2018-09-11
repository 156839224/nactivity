﻿/* Licensed under the Apache License, Version 2.0 (the "License");
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

namespace org.activiti.engine.impl.variable
{
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.interceptor;

    /// <summary>
    /// Variable type capable of storing reference to JPA-entities. Only JPA-Entities which are configured by annotations are supported. Use of compound primary keys is not supported.
    /// 
    /// 
    /// </summary>
    public class JPAEntityVariableType : AbstractVariableType, ICacheableVariable
    {

        public const string TYPE_NAME = "jpa-entity";

        private JPAEntityMappings mappings;

        private bool forceCacheable;

        public JPAEntityVariableType()
        {
            mappings = new JPAEntityMappings();
        }

        public override string TypeName
        {
            get
            {
                return TYPE_NAME;
            }
        }

        public override bool Cachable
        {
            get
            {
                return forceCacheable;
            }
        }

        public override bool isAbleToStore(object value)
        {
            if (value == null)
            {
                return true;
            }
            return mappings.isJPAEntity(value);
        }

        public override void setValue(object value, IValueFields valueFields)
        {
            IEntityManagerSession entityManagerSession = Context.CommandContext.getSession<IEntityManagerSession>();
            if (entityManagerSession == null)
            {
                throw new ActivitiException("Cannot set JPA variable: " + typeof(IEntityManagerSession) + " not configured");
            }
            else
            {
                // Before we set the value we must flush all pending changes from
                // the entitymanager
                // If we don't do this, in some cases the primary key will not yet
                // be set in the object
                // which will cause exceptions down the road.
                entityManagerSession.flush();
            }

            if (value != null)
            {
                string className = mappings.getJPAClassString(value);
                string idString = mappings.getJPAIdString(value);
                valueFields.TextValue = className;
                valueFields.TextValue2 = idString;
            }
            else
            {
                valueFields.TextValue = null;
                valueFields.TextValue2 = null;
            }
        }

        public override object getValue(IValueFields valueFields)
        {
            if (!string.ReferenceEquals(valueFields.TextValue, null) && !string.ReferenceEquals(valueFields.TextValue2, null))
            {
                return mappings.getJPAEntity(valueFields.TextValue, valueFields.TextValue2);
            }
            return null;
        }

        /// <summary>
        /// Force the value to be cacheable.
        /// </summary>
        public virtual bool ForceCacheable
        {
            set
            {
                this.forceCacheable = value;
            }
        }

    }

}
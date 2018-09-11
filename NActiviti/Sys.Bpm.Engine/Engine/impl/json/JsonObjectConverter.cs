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
namespace org.activiti.engine.impl.json
{
    using Newtonsoft.Json.Linq;
    using System.IO;

    /// 
    public abstract class JsonObjectConverter<T>
    {

        public virtual void toJson(T @object, StreamWriter writer)
        {
            writer.Write(toJson(@object));
        }

        public virtual string toJson(T @object)
        {
            if (@object == null)
            {
                return "";
            }

            return toJsonObject(@object).ToString();
        }

        public virtual string toJson(T @object, int indentFactor)
        {
            if (@object == null)
            {
                return "";
            }

            return JToken.FromObject(@object).ToString(indentFactor > 0 ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
        }

        public abstract JToken toJsonObject(T @object);


        public abstract T toObject(StreamReader reader);
    }

}
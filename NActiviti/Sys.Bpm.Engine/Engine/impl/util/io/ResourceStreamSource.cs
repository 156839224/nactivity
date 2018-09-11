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
using Sys;
using System.IO;
using org.activiti.engine;

namespace org.activiti.engine.impl.util.io
{


    /// 
    /// 
    public class ResourceStreamSource : IStreamSource
    {

        internal string resource;
        internal ClassLoader classLoader;

        public ResourceStreamSource(string resource)
        {
            this.resource = resource;
        }

        public ResourceStreamSource(string resource, ClassLoader classLoader)
        {
            this.resource = resource;
            this.classLoader = classLoader;
        }

        public virtual System.IO.Stream InputStream
        {
            get
            {
                System.IO.Stream inputStream = null;
                if (classLoader == null)
                {
                    inputStream = ReflectUtil.getResourceAsStream(resource);
                }
                else
                {
                    inputStream = classLoader.getResourceAsStream(resource);
                }
                if (inputStream == null)
                {
                    throw new ActivitiIllegalArgumentException("resource '" + resource + "' doesn't exist");
                }
                return new BufferedStream(inputStream);
            }
        }

        public override string ToString()
        {
            return "Resource[" + resource + "]";
        }
    }

}
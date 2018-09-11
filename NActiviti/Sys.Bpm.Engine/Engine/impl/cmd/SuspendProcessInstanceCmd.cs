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
namespace org.activiti.engine.impl.cmd
{
	using org.activiti.engine.impl.persistence.entity;

	/// 
	/// 
	public class SuspendProcessInstanceCmd : AbstractSetProcessInstanceStateCmd
	{

	  public SuspendProcessInstanceCmd(string executionId) : base(executionId)
	  {
	  }

	  protected internal override ISuspensionState NewState
	  {
		  get
		  {
			return org.activiti.engine.impl.persistence.entity.SuspensionState_Fields.SUSPENDED;
		  }
	  }

	}

}
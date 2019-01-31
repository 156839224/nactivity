///////////////////////////////////////////////////////////
//  GetExecutorBookmarkRuleCmd.cs
//  Implementation of the Class GetExecutorBookmarkRuleCmd
//  Generated by Enterprise Architect
//  Created on:      30-1月-2019 8:32:00
//  Original author: 张楠
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using org.activiti.engine.impl.interceptor;

namespace Sys.Workflow.Engine.Bpmn.Rules
{
    /// <summary>
    /// 获取当前执行人的用户信息
    /// </summary>
	public class GetExecutorBookmarkRuleCmd : IGetBookmarkRule
    {

        public GetExecutorBookmarkRuleCmd()
        {

        }

        public QueryBookmark Condition { get; set; }

        /// 
        /// <param name="executorId"></param>
        private IUserInfo 获取用户信息(string executorId)
        {

            return null;
        }

        public IList<IUserInfo> execute(ICommandContext commandContext)
        {
            throw new NotImplementedException();
        }
    }
}
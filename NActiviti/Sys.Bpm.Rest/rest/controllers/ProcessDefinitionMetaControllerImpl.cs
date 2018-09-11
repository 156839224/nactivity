﻿using Microsoft.AspNetCore.Mvc;
using org.activiti.bpmn.model;
using org.activiti.cloud.services.api.model;
using org.activiti.cloud.services.rest.api;
using org.activiti.cloud.services.rest.api.resources;
using org.activiti.cloud.services.rest.assemblers;
using org.activiti.engine;
using org.activiti.engine.repository;
using System.Collections.Generic;
using System.Linq;

namespace org.activiti.cloud.services.rest.controllers
{
    [Route("v1/process-definitions/{id}/meta")]
    [ApiController]
    public class ProcessDefinitionMetaControllerImpl : ControllerBase, IProcessDefinitionMetaController
    {
        private readonly IRepositoryService repositoryService;
        private readonly ProcessDefinitionMetaResourceAssembler resourceAssembler;

        public virtual string handleAppException(ActivitiObjectNotFoundException ex)
        {
            return ex.Message;
        }

        public ProcessDefinitionMetaControllerImpl(IRepositoryService repositoryService, ProcessDefinitionMetaResourceAssembler resourceAssembler)
        {
            this.repositoryService = repositoryService;
            this.resourceAssembler = resourceAssembler;
        }

        [HttpGet]
        public virtual ProcessDefinitionMetaResource GetProcessDefinitionMetadata(string id)
        {
            IProcessDefinition processDefinition = repositoryService.createProcessDefinitionQuery().processDefinitionId(id).singleResult();
            if (processDefinition == null)
            {
                throw new ActivitiObjectNotFoundException("Unable to find process definition for the given id:'" + id + "'");
            }

            IList<Process> processes = repositoryService.getBpmnModel(id).Processes;
            ISet<ProcessDefinitionVariable> variables = new HashSet<ProcessDefinitionVariable>();
            ISet<string> users = new HashSet<string>();
            ISet<string> groups = new HashSet<string>();
            ISet<ProcessDefinitionUserTask> userTasks = new HashSet<ProcessDefinitionUserTask>();
            ISet<ProcessDefinitionServiceTask> serviceTasks = new HashSet<ProcessDefinitionServiceTask>();

            foreach (Process process in processes)
            {
                var vs = getVariables(process).ToList();
                vs.ForEach(v => variables.Add(v));
                IList<FlowElement> flowElementList = (IList<FlowElement>)process.FlowElements;
                foreach (FlowElement flowElement in flowElementList)
                {
                    if (flowElement.GetType().Equals(typeof(UserTask)))
                    {
                        UserTask userTask = (UserTask)flowElement;
                        ProcessDefinitionUserTask task = new ProcessDefinitionUserTask(userTask.Name, userTask.Documentation);
                        userTasks.Add(task);
                        userTask.CandidateUsers.ToList().ForEach(u => users.Add(u));
                        userTask.CandidateGroups.ToList().ForEach(ug => groups.Add(ug));
                    }
                    if (flowElement.GetType().Equals(typeof(ServiceTask)))
                    {
                        ServiceTask serviceTask = (ServiceTask)flowElement;
                        ProcessDefinitionServiceTask task = new ProcessDefinitionServiceTask(serviceTask.Name, serviceTask.Implementation);
                        serviceTasks.Add(task);
                    }
                }
            }

            return resourceAssembler.toResource(new ProcessDefinitionMeta(processDefinition.Id, processDefinition.Name, processDefinition.Description, processDefinition.Version, users, groups, variables, userTasks, serviceTasks));
        }

        private IList<ProcessDefinitionVariable> getVariables(Process process)
        {
            IList<ProcessDefinitionVariable> variables = new List<ProcessDefinitionVariable>();
            if (process.ExtensionElements.Count > 0)
            {
                IEnumerator<IList<ExtensionElement>> it = process.ExtensionElements.Values.GetEnumerator();
                while (it.MoveNext())
                {
                    IList<ExtensionElement> extensionElementList = it.Current;
                    IEnumerator<ExtensionElement> it2 = extensionElementList.GetEnumerator();
                    while (it2.MoveNext())
                    {
                        ExtensionElement ee = it2.Current;
                        string name = ee.getAttributeValue(ee.Namespace, "variableName");
                        string type = ee.getAttributeValue(ee.Namespace, "variableType");
                        ProcessDefinitionVariable variable = new ProcessDefinitionVariable(name, type);
                        variables.Add(variable);
                    }
                }
            }
            return variables;
        }
    }

}
﻿using Microsoft.AspNetCore.Mvc;
using org.activiti.cloud.services.api.commands;
using org.activiti.cloud.services.api.model;
using org.activiti.cloud.services.api.model.converter;
using org.activiti.cloud.services.core;
using org.activiti.cloud.services.rest.api;
using org.activiti.cloud.services.rest.api.resources;
using org.activiti.cloud.services.rest.assemblers;
using org.activiti.engine;
using org.springframework.data.domain;
using org.springframework.hateoas;
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

namespace org.activiti.cloud.services.rest.controllers
{
    [Route("/v1/tasks")]
    [ApiController]
    public class TaskControllerImpl : ControllerBase, ITaskController
    {

        private ProcessEngineWrapper processEngine;

        private readonly TaskResourceAssembler taskResourceAssembler;

        private AuthenticationWrapper authenticationWrapper;

        //private readonly AlfrescoPagedResourcesAssembler<Task> pagedResourcesAssembler;

        private readonly TaskConverter taskConverter;

        public TaskControllerImpl(ProcessEngineWrapper processEngine, TaskResourceAssembler taskResourceAssembler, AuthenticationWrapper authenticationWrapper, TaskConverter taskConverter)
        {
            this.authenticationWrapper = authenticationWrapper;
            this.processEngine = processEngine;
            this.taskResourceAssembler = taskResourceAssembler;
            this.taskConverter = taskConverter;
        }

        public virtual string handleAppException(ActivitiObjectNotFoundException ex)
        {
            return ex.Message;
        }

        [HttpGet]
        public virtual PagedResources<TaskResource> getTasks(Pageable pageable)
        {
            Page<Task> page = processEngine.getTasks(pageable);
            //return pagedResourcesAssembler.toResource(pageable, page, taskResourceAssembler);
            return null;
        }

        [HttpGet("{taskId}")]
        public virtual Resource<Task> getTaskById(string taskId)
        {
            Task task = processEngine.getTaskById(taskId);
            if (task == null)
            {
                throw new ActivitiObjectNotFoundException("Unable to find task for the given id: " + taskId);
            }
            return taskResourceAssembler.toResource(task);
        }

        [HttpPost("/{taskId}/claim")]
        public virtual Resource<Task> claimTask(string taskId)
        {
            string assignee = authenticationWrapper.AuthenticatedUserId;
            if (string.ReferenceEquals(assignee, null))
            {
                throw new System.InvalidOperationException("Assignee must be resolved from the Identity/Security Layer");
            }

            return taskResourceAssembler.toResource(processEngine.claimTask(new ClaimTaskCmd(taskId, assignee)));
        }


        [HttpPost("/{taskId}/release")]
        public virtual Resource<Task> releaseTask(string taskId)
        {

            return taskResourceAssembler.toResource(processEngine.releaseTask(new ReleaseTaskCmd(taskId)));
        }

        [HttpPost("/{taskId}/complete")]
        public virtual IActionResult completeTask(string taskId, [FromBody]CompleteTaskCmd completeTaskCmd)
        {

            IDictionary<string, object> outputVariables = null;
            if (completeTaskCmd != null)
            {
                outputVariables = completeTaskCmd.OutputVariables;
            }
            processEngine.completeTask(new CompleteTaskCmd(taskId, outputVariables));

            return Ok();
        }

        [HttpDelete("{taskId}")]
        public virtual void deleteTask(string taskId)
        {
            processEngine.deleteTask(taskId);
        }

        [HttpPost]
        public virtual Resource<Task> createNewTask([FromBody]CreateTaskCmd createTaskCmd)
        {
            return taskResourceAssembler.toResource(processEngine.createNewTask(createTaskCmd));
        }

        [HttpPut("{taskId}")]
        public virtual IActionResult updateTask(string taskId, UpdateTaskCmd updateTaskCmd)
        {
            processEngine.updateTask(taskId, updateTaskCmd);

            return Ok();
        }

        [HttpPost("{taskId}/subtask")]
        public virtual Resource<Task> createSubtask([FromQuery]string taskId, [FromBody]CreateTaskCmd createSubtaskCmd)
        {
            return taskResourceAssembler.toResource(processEngine.createNewSubtask(taskId, createSubtaskCmd));
        }

        [HttpGet("{taskId}/subtasks")]
        public virtual Resources<TaskResource> getSubtasks(string taskId)
        {
            return null;
            //return new Resources<TaskResource>(taskResourceAssembler.toResources(taskConverter.from(processEngine.getSubtasks(taskId))), linkTo(typeof(TaskControllerImpl)).withSelfRel());
        }

        public virtual AuthenticationWrapper AuthenticationWrapper
        {
            get
            {
                return authenticationWrapper;
            }
            set
            {
                this.authenticationWrapper = value;
            }
        }

    }

}
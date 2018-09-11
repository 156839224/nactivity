﻿using org.activiti.cloud.services.api.model;
using org.activiti.cloud.services.rest.api.resources;
using org.activiti.cloud.services.rest.controllers;
using org.springframework.hateoas.mvc;
using System;

namespace org.activiti.cloud.services.rest.assemblers
{
    public class TaskVariableResourceAssembler : ResourceAssemblerSupport<TaskVariable, TaskVariableResource>
    {

        public TaskVariableResourceAssembler() : base(typeof(TaskVariableControllerImpl), typeof(TaskVariableResource))
        {
        }

        public override TaskVariableResource toResource(TaskVariable taskVariable)
        {
            throw new NotImplementedException();
            //Link globalVariables = linkTo(methodOn(typeof(TaskVariableControllerImpl)).getVariables(taskVariable.TaskId)).withRel("globalVariables");
            //Link localVariables = linkTo(methodOn(typeof(TaskVariableControllerImpl)).getVariablesLocal(taskVariable.TaskId)).withRel("localVariables");
            //Link taskRel = linkTo(methodOn(typeof(TaskControllerImpl)).getTaskById(taskVariable.TaskId)).withRel("task");
            //Link homeLink = linkTo(typeof(HomeControllerImpl)).withRel("home");
            //return new TaskVariableResource(taskVariable,globalVariables,localVariables,taskRel,homeLink);
        }
    }

}
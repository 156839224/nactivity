﻿using Newtonsoft.Json;

namespace org.activiti.cloud.services.api.commands.results
{
    public class SignalProcessInstancesResults : CommandResults
    {

        private string id;
        private string commandId;

        public SignalProcessInstancesResults()
        {
            this.id = System.Guid.NewGuid().ToString();
        }

        [JsonConstructor]
        public SignalProcessInstancesResults([JsonProperty("CommandId")]string commandId) : this()
        {
            this.commandId = commandId;
        }

        public virtual string Id
        {
            get
            {
                return id;
            }
        }

        public virtual string CommandId
        {
            get
            {
                return commandId;
            }
        }
    }

}
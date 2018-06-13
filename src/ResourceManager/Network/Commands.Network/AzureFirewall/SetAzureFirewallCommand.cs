﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Management.Automation;
using Microsoft.Azure.Commands.Network.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Management.Network;
using MNM = Microsoft.Azure.Management.Network.Models;

namespace Microsoft.Azure.Commands.Network
{
    [Cmdlet(VerbsCommon.Set, "AzureRmFirewall", SupportsShouldProcess = true), OutputType(typeof(PSAzureFirewall))]
    public class SetAzureFirewallCommand : AzureFirewallBaseCmdlet
    {
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            HelpMessage = "The AzureFirewall")]
        public PSAzureFirewall AzureFirewall { get; set; }

        public override void Execute()
        {
            base.Execute();

            if (!this.IsAzureFirewallPresent(this.AzureFirewall.ResourceGroupName, this.AzureFirewall.Name))
            {
                throw new ArgumentException(Microsoft.Azure.Commands.Network.Properties.Resources.ResourceNotFound);
            }

            if (this.AzureFirewall.IpConfigurations.Count != 1)
            {
                throw new ArgumentException(string.Format("There must be exactly one IP configuration associated with the Azure Firewall, found {0}.", this.AzureFirewall.IpConfigurations.Count));
            }
            if (this.AzureFirewall.IpConfigurations[0].PublicIpAddress == null)
            {
                throw new ArgumentException("The Azure Firewall IP configuration Public IP Address must be populated.");
            }

            this.AzureFirewall.IpConfigurations[0].InternalPublicIpAddress = this.AzureFirewall.IpConfigurations[0].PublicIpAddress;
            this.AzureFirewall.IpConfigurations[0].PublicIpAddress = null;

            // Map to the sdk object
            var secureGwModel = NetworkResourceManagerProfile.Mapper.Map<MNM.AzureFirewall>(this.AzureFirewall);
            secureGwModel.Tags = TagsConversionHelper.CreateTagDictionary(this.AzureFirewall.Tag, validate: true);

            // Execute the PUT AzureFirewall call
            this.AzureFirewallClient.CreateOrUpdate(this.AzureFirewall.ResourceGroupName, this.AzureFirewall.Name, secureGwModel);

            var getAzureFirewall = this.GetAzureFirewall(this.AzureFirewall.ResourceGroupName, this.AzureFirewall.Name);
            WriteObject(getAzureFirewall);
        }
    }
}

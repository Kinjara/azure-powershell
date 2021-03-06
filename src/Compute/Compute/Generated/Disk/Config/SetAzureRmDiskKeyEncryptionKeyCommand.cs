//
// Copyright (c) Microsoft and contributors.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and
// limitations under the License.
//

// Warning: This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    [Cmdlet(VerbsCommon.Set, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "DiskKeyEncryptionKey", SupportsShouldProcess = true)]
    [OutputType(typeof(PSDisk))]
    public partial class SetAzureRmDiskKeyEncryptionKeyCommand : Microsoft.Azure.Commands.ResourceManager.Common.AzureRMCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public PSDisk Disk { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public string KeyUrl { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public string SourceVaultId { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldProcess("Disk", "Set"))
            {
                Run();
            }
        }

        private void Run()
        {
            // EncryptionSettingsCollection
            if (this.Disk.EncryptionSettingsCollection == null)
            {
                this.Disk.EncryptionSettingsCollection = new EncryptionSettingsCollection();
            }

            // EncryptionSettings
            if (this.Disk.EncryptionSettingsCollection.EncryptionSettings == null)
            {
                this.Disk.EncryptionSettingsCollection.EncryptionSettings = new List<EncryptionSettingsElement>();
            }

            if (this.Disk.EncryptionSettingsCollection.EncryptionSettings.Count == 0)
            {
                this.Disk.EncryptionSettingsCollection.EncryptionSettings.Add(new EncryptionSettingsElement());
            }

            if (this.IsParameterBound(c => c.KeyUrl))
            {
                // KeyEncryptionKey
                if (this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey == null)
                {
                    this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey = new KeyVaultAndKeyReference();
                }
                this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey.KeyUrl = this.KeyUrl;
            }

            if (this.IsParameterBound(c => c.SourceVaultId))
            {
                // KeyEncryptionKey
                if (this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey == null)
                {
                    this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey = new KeyVaultAndKeyReference();
                }
                // SourceVault
                if (this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey.SourceVault == null)
                {
                    this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey.SourceVault = new SourceVault();
                }
                this.Disk.EncryptionSettingsCollection.EncryptionSettings[0].KeyEncryptionKey.SourceVault.Id = this.SourceVaultId;
            }

            WriteObject(this.Disk);
        }
    }
}

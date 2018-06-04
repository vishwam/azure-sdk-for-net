// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

namespace IotCentral.Tests.ScenarioTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IotCentral.Tests.Helpers;
    using Microsoft.Azure.Management.IotCentral;
    using Microsoft.Azure.Management.IotCentral.Models;
    using Microsoft.Azure.Management.ResourceManager;
    using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
    using Xunit;

    public class IotCentralLifeCycleTests : IotCentralTestBase
    {
        [Fact]
        public void TestIotCentralCreateLifeCycle()
        {
            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                this.Initialize(context);

                // Create Resource Group
                var resourceGroup = this.CreateResourceGroup(IotCentralTestUtilities.DefaultResourceGroupName);

                // Check if Hub Exists and Delete
                var iotHubNameAvailabilityInfo = this.iotHubClient.Apps.CheckNameAvailability(IotCentralTestUtilities.DefaultIotCentralName);
                if (!(bool)iotHubNameAvailabilityInfo.NameAvailable)
                {
                    this.iotHubClient.Apps.Delete(
                        IotCentralTestUtilities.DefaultResourceGroupName,
                        IotCentralTestUtilities.DefaultIotCentralName);

                    iotHubNameAvailabilityInfo = this.iotHubClient.Apps.CheckNameAvailability(IotCentralTestUtilities.DefaultIotCentralName);
                    Assert.True(iotHubNameAvailabilityInfo.NameAvailable);
                }

                // Create App
                var app = this.CreateIotCentral(resourceGroup, IotCentralTestUtilities.DefaultLocation, IotCentralTestUtilities.DefaultIotCentralName);

                Assert.NotNull(app);
                Assert.Equal(AppSku.S1, app.Sku.Name);
                Assert.Equal(IotCentralTestUtilities.DefaultIotCentralName, app.Name);

                // Add and Get Tags
                IDictionary<string, string> tags = new Dictionary<string, string>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                };

                var appPatch = new AppPatch()
                {
                    Tags = tags
                };

                app = this.iotHubClient.Apps.Update(IotCentralTestUtilities.DefaultResourceGroupName, IotCentralTestUtilities.DefaultIotCentralName, appPatch);

                Assert.NotNull(app);
                Assert.True(app.Tags.Count().Equals(2));
                Assert.Equal("value2", app.Tags["key2"]);

                // Get all Iot Hubs in a resource group
                var iotHubs = this.iotHubClient.Apps.ListByResourceGroup(IotCentralTestUtilities.DefaultResourceGroupName);
                Assert.True(iotHubs.Count() > 0);

                // Get all Iot Hubs in a subscription
                var iotHubBySubscription = this.iotHubClient.Apps.ListBySubscription();
                Assert.True(iotHubBySubscription.Count() > 0);

                // Get all of the available IoT Hub REST API operations
                var operationList = this.iotHubClient.Operations.List();
                Assert.True(operationList.Count() > 0);
                Assert.Contains(operationList, e => e.Name.Equals("Microsoft.IoTCentral/apps/Read", StringComparison.OrdinalIgnoreCase));

                // Get IoT Hub REST API read operation
                var hubReadOperation = operationList.Where(e => e.Name.Equals("Microsoft.IoTCentral/apps/Read", StringComparison.OrdinalIgnoreCase));
                Assert.True(hubReadOperation.Count().Equals(1));
            }
        }

        [Fact]
        public void TestIotCentralUpdateLifeCycle()
        {
            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                this.Initialize(context);

                // Create Resource Group
                var resourceGroup = this.CreateResourceGroup(IotCentralTestUtilities.DefaultUpdateResourceGroupName);

                // Check if Hub Exists and Delete
                var iotHubNameAvailabilityInfo = this.iotHubClient.Apps.CheckNameAvailability(IotCentralTestUtilities.DefaultUpdateIotCentralName);
                if (!(bool)iotHubNameAvailabilityInfo.NameAvailable)
                {
                    this.iotHubClient.Apps.Delete(
                        IotCentralTestUtilities.DefaultResourceGroupName,
                        IotCentralTestUtilities.DefaultUpdateIotCentralName);

                    iotHubNameAvailabilityInfo = this.iotHubClient.Apps.CheckNameAvailability(IotCentralTestUtilities.DefaultUpdateIotCentralName);
                    Assert.True(iotHubNameAvailabilityInfo.NameAvailable);
                }

                // Update App
                var app = this.CreateIotCentral(resourceGroup, IotCentralTestUtilities.DefaultLocation, IotCentralTestUtilities.DefaultIotCentralName);

                Assert.NotNull(app);
                Assert.Equal(AppSku.S1, app.Sku.Name);
                Assert.Equal(IotCentralTestUtilities.DefaultUpdateIotCentralName, app.Name);
            }
        }
    }
}

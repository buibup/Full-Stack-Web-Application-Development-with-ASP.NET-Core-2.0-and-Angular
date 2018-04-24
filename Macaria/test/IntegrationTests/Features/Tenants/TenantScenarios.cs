﻿using Macaria.API.Features.Tenants;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestUtilities.Extensions;
using Xunit;

namespace IntegrationTests.Features.Tenants
{
    [CollectionDefinition("NoteScenarios", DisableParallelization = true)]
    public class TenantScenarios: TenantScenarioBase
    {

        [Fact]
        public async Task ShouldSaveTenant()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .PostAsAsync<SaveTenantCommand.Request, SaveTenantCommand.Response>(Post.Tenants, new SaveTenantCommand.Request() {
                        Tenant = new TenantApiModel()
                        {
                            Name = "New Tenant"
                        }
                    });

                Assert.True(response.TenantId != default(Guid));
            }
        }

        [Fact]
        public async Task ShouldVerifyTenant()
        {
            using (var server = CreateServer())
            {                
                var response = await server.CreateClient()
                    .PostAsync(Post.Verify(new Guid("60DE04D9-E441-E811-9D3A-D481D7227E7A")),null);

                response.EnsureSuccessStatusCode();                
            }
        }
        [Fact]
        public async Task ShouldGetAllTenants()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetTenantsQuery.Response>(Get.Tenants);

                Assert.True(response.Tenants.Count() == 1);
            }
        }


        [Fact]
        public async Task ShouldGetTenantById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetTenantByIdQuery.Response>(Get.TenantById(new Guid("60DE04D9-E441-E811-9D3A-D481D7227E7A")));

                Assert.True(response.Tenant.TenantId == new Guid("60DE04D9-E441-E811-9D3A-D481D7227E7A"));
            }
        }
        
        [Fact]
        public async Task ShouldUpdateTenant()
        {
            using (var server = CreateServer())
            {                
                var saveResponse = await server.CreateClient()
                    .PostAsAsync<SaveTenantCommand.Request, SaveTenantCommand.Response>(Post.Tenants, new SaveTenantCommand.Request()
                    {
                        Tenant = new TenantApiModel() {
                            TenantId = new Guid("60DE04D9-E441-E811-9D3A-D481D7227E7A"),
                            Name = "Default 1"
                        }
                    });

                Assert.True(saveResponse.TenantId == new Guid("60DE04D9-E441-E811-9D3A-D481D7227E7A"));
            }
        }
        
        [Fact]
        public async Task ShouldDeleteTenant()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Tenant(new Guid("60DE04D9-E441-E811-9D3A-D481D7227E7A")));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
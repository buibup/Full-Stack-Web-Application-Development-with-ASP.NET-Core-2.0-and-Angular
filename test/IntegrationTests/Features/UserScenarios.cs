using Macaria.API.Features.Users;
using Macaria.Core.Extensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class UserScenarios: UserScenarioBase
    {
        [Fact]
        public async Task ShouldAuthenticateUser()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .PostAsAsync<AuthenticateCommand.Request, AuthenticateCommand.Response>(Post.Token, new AuthenticateCommand.Request()
                    {
                        Username = "quinntynebrown@gmail.com",
                        Password = "P@ssw0rd"
                    });

                Assert.NotEqual(default(Guid), response.UserId);
                Assert.True(response.AccessToken != default(string));
            }
        }
    }
}

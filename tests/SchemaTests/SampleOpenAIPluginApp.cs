using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace SchemaTests;
class SampleOpenAIPluginApp : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {

        // without this line below, certain remote test scenarios will fail
        builder.UseContentRoot(Directory.GetCurrentDirectory());

        return base.CreateHost(builder);
    }
}

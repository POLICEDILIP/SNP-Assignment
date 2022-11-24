using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SNP_Assignment;

public class Program
{

    static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<UploadAndDownloadFiles>();

        var serviceProvider = services.BuildServiceProvider();
        var uploadAndDownloadFiles = serviceProvider.GetService<UploadAndDownloadFiles>();

        uploadAndDownloadFiles.UploadFiles();

    }

}

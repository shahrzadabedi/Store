using JsonNet.ContractResolvers;
using Newtonsoft.Json;
namespace Store.Infrastructure.Persistence;

public static class SeedHelper
{
    public static List<TEntity>? SeedData<TEntity>(string fileName)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        const string path = "wwwroot\\seed-data";
        var fullPath = Path.Combine(currentDirectory, path, fileName);

        using var reader = new StreamReader(fullPath);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<TEntity>>(json, new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        });
    }
}

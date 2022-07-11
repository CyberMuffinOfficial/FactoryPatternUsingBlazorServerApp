using FactoryPatternUsingBlazorServerApp.Samples;

namespace FactoryPatternUsingBlazorServerApp.Factories;

public static class DifferentImplementationsFactory
{
    public static void AddVehicleFactory(this IServiceCollection services)
    {
        services.AddTransient<IVehicle, Car>();
        services.AddTransient<IVehicle, Van>();
        services.AddTransient<IVehicle, Truck>();
        services.AddSingleton<Func<IEnumerable<IVehicle>>>(x => () => x.GetService<IEnumerable<IVehicle>>()!);
        services.AddSingleton<IVehicleFactory, VehicleFactory>();
    }
}

public interface IVehicleFactory
{
    IVehicle Create(string name);
}

public class VehicleFactory : IVehicleFactory
{
    private readonly Func<IEnumerable<IVehicle>> _factory;

    public VehicleFactory(Func<IEnumerable<IVehicle>> factory)
    {
        _factory = factory;
    }

    public IVehicle Create(string name)
    {
        var allVehicles = _factory();
        IVehicle output = allVehicles.Where(x => x.VehicleType == name).First();
        return output;
    }

}

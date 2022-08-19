namespace ProxyCollectorNET.Infrastructure;

public static class TryCatch
{
    public static async Task<(T? Response, Exception? Error)> HandleAsync<T>(Func<Task<T>> method)
    {
        try
        {
            var response = await method();

            return (response, default);
        }
        catch (Exception ex)
        {
            return (default, ex);
        }
    }

    public static (T? Response, Exception? Error) Handle<T>(Func<T> method)
    {
        try
        {
            var response = method();

            return (response, null);
        }
        catch (Exception ex)
        {
            return (default, ex);
        }
    }

    public static Exception? Handle(Action method)
    {
        try
        {
            method();

            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public static async Task<Exception?> HandleAsync(Action method)
    {
        try
        {
            await Task.Run(method);

            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
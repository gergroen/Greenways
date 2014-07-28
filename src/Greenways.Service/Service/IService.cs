namespace Greenways.Service
{
    public interface IService
    {
        int Order { get; }
        bool Start();
        bool Stop();
    }
}

namespace Candal.Configuration
{
    public interface IAppConfig
    {
        void Load();
        void Save();
        AppConfigData Data { get; }
    }
}
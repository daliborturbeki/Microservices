using CommandsService.Models;

namespace CommandsService.Data 
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformID);

        IEnumerable<Command> GetCommandsForPlatform(int platformID);
        Command GetCommand(int platformID, int commandID);
        void CreateCommand(int platformID, Command command);

    }
}
using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int platformID, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformID = platformID;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformID, int commandID)
        {
            return _context.Commands
                .Where(c => c.PlatformID == platformID && c.ID == commandID)
                .FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformID)
        {
            return _context.Commands
                .Where(c => c.PlatformID == platformID);
        }

        public bool PlatformExists(int platformID)
        {
            return _context.Platforms.Any(p => p.ID == platformID);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
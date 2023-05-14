using AutoMapper;
using CommandsService.Data;
using CommandsService.DTOs;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository repository, IMapper mapper)    
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsForPlatform(int platformID)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformID}");

            if (!_repository.PlatformExists(platformID))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForPlatform(platformID);

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }
        
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDTO> GetCommandForPlatform(int platformID, int commandID)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformID} / {commandID}");

            if (!_repository.PlatformExists(platformID))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(platformID, commandID);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommandForPlatform(int platformID, CommandCreateDTO commandDTO)
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformID}");

            if (!_repository.PlatformExists(platformID))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDTO);
            _repository.CreateCommand(platformID, command);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformID = platformID, commandID = commandReadDTO.ID }, commandReadDTO);
        }
    }
}
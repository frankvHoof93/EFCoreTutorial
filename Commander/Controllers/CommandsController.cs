using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    // api/commands (both)
    // [Route("api/[controller]")]
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            IEnumerable<Command> commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            Command commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            return NotFound();
        }

        // POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmd)
        {
            Command command = _mapper.Map<Command>(cmd);
            _repository.CreateCommand(command);
            _repository.SaveChanges();

            var readCmd = _mapper.Map<CommandReadDto>(command);
            
            return CreatedAtRoute(nameof(GetCommandById), new {Id = readCmd.Id}, readCmd);
            
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto cmd)
        {
            Command cmdModelFromRepo = _repository.GetCommandById(id);
            if (cmdModelFromRepo == null)
                return NotFound();
            // Updates original object, which syncs with DB
            _mapper.Map(cmd, cmdModelFromRepo);
            // This should do nothing in most implementations, but might be used by users
            _repository.UpdateCommand(cmdModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            Command cmdModelFromRepo = _repository.GetCommandById(id);
            if (cmdModelFromRepo == null)
                return NotFound();
            
            CommandUpdateDto cmdToPatch = _mapper.Map<CommandUpdateDto>(cmdModelFromRepo);
            patchDoc.ApplyTo(cmdToPatch, ModelState);
            if (!TryValidateModel(cmdToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(cmdToPatch, cmdModelFromRepo);

            _repository.UpdateCommand(cmdModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        // DELETE api/commands/{id]
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            Command cmdModelFromRepo = _repository.GetCommandById(id);
            if (cmdModelFromRepo == null)
                return NotFound();
            _repository.DeleteCommand(cmdModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
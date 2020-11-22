using Microsoft.AspNetCore.Mvc;
using RestApi_Dicom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Dicom.Controllers
{
    [Route("Image/com")]
    [ApiController]
    public class CommandsController2 : ControllerBase
    {
        private ICOmmanderRepoForHac _repository;

        public CommandsController2(ICOmmanderRepoForHac repository)
        {
            _repository = repository;
        }

        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        [HttpGet]
        public ActionResult<IEnumerable<ImageData>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(commandItems);
        }

        [HttpGet("{id}")]
        public ActionResult<ImageData> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
                return Ok(commandItem);
            else
                return NotFound(id);
        }

        //POST api/commands
        [HttpPost]
        public ActionResult CreateCommand(ImageData imageCreate)
        {
            _repository.AddToList(imageCreate);
            return Ok(imageCreate);

        }
    }
}

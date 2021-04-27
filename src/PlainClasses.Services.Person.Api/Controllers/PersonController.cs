using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlainClasses.Services.Person.Api.Controllers.Requests;
using PlainClasses.Services.Person.Application.Commands.AddAuth;
using PlainClasses.Services.Person.Application.Commands.CreatePerson;
using PlainClasses.Services.Person.Application.Commands.DeleteAuth;
using PlainClasses.Services.Person.Application.Commands.DeletePerson;
using PlainClasses.Services.Person.Application.Commands.UpdatePerson;
using PlainClasses.Services.Person.Application.Queries.GetPerson;
using PlainClasses.Services.Person.Application.Queries.GetPersons;

namespace PlainClasses.Services.Person.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersons()
        {
            var persons = await _mediator.Send(new GetPersonsQuery());

            return Ok(persons);
        }
        
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PersonViewModelDetail), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            var person = await _mediator.Send(new GetPersonQuery { Id = id});

            return Ok(person);
        }
        
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(ReturnPersonViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePerson([FromBody]CreatePersonRequest request) 
        {
            var person = await _mediator.Send(new CreatePersonCommand(request.PersonalNumber, request.MilitaryRankId, 
                request.PlatoonId, request.Password, request.FirstName, request.LastName, request.FatherName, request.BirthDate, 
                request.WorkPhoneNumber, request.PersonalPhoneNumber, request.Position));

            return Created(string.Empty, person);
        }     
        
        [Route("{id}/auth")]
        [HttpPost]
        [ProducesResponseType(typeof(ReturnPersonViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddAuthToPerson(Guid id, [FromBody]AddAuthRequest request)
        {
            var person = await _mediator.Send(new AddAuthCommand(id, request.AuthName));

            return Created(string.Empty, person);
        } 
        
        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdatePerson(Guid id, [FromBody]UpdatePersonRequest request)
        {
            await _mediator.Send(new UpdatePersonCommand(id, request.MilitaryRankId, request.PlatoonId, request.Password, 
                request.LastName, request.WorkPhoneNumber, request.PersonalPhoneNumber, request.Position));

            return NoContent();
        } 
        
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            await _mediator.Send(new DeletePersonCommand(id));

            return NoContent();
        }
        
        [Route("{id}/auth/{authId}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAuthFromPerson(Guid id, Guid authId)
        {
            await _mediator.Send(new DeleteAuthCommand(id, authId));

            return NoContent();
        }
    }
}
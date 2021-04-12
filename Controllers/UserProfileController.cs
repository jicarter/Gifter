using System;
using Microsoft.AspNetCore.Mvc;
using Gifter.Repositories;
using Gifter.Models;
using System.Collections.Generic;

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userprofileRepository;
        public UserProfileController(IUserProfileRepository userprofileRepository)
        {
            _userprofileRepository = userprofileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userprofileRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userprofile = _userprofileRepository.GetById(id);
            if (userprofile == null)
            {
                return NotFound();
            }
            return Ok(userprofile);
        }


        [HttpPost]
        public IActionResult UserProfile(UserProfile userprofile)
        {
            _userprofileRepository.Add(userprofile);
            return CreatedAtAction("Get", new { id = userprofile.Id }, userprofile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile userprofile)
        {
            if (id != userprofile.Id)
            {
                return BadRequest();
            }

            _userprofileRepository.Update(userprofile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userprofileRepository.Delete(id);
            return NoContent();
        }



        [HttpGet("GetUserProfileByIdWithPosts{id}")]
        public IActionResult GetUserProfileByIdWithPosts(int id)
        {
            var userprofile = _userprofileRepository.GetUserProfileByIdWithPosts(id);
            if (userprofile == null)
            {
                return NotFound();
            }
            return Ok(userprofile);
        }


    }
}
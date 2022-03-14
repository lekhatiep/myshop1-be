using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Api.Share.Delegate;

namespace Api.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Permission.Home")]
    public class HomesController : ControllerBase
    {
        //private readonly IStudentRepository _repository = null;
        private readonly IStudentRepository _repository;
        private readonly ServiceResolver _serviceResolver;
        //Initialize the variable through constructor
        public HomesController( ServiceResolver serviceResolver)
        {
            //_repository = repository;
            _serviceResolver = serviceResolver;
        }

        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            TestStudentRepository1 repository = new TestStudentRepository1();
            //List<Student> allStudentDetails = repository.GetAllStudent();

            List<Student> allStudentDetails = _repository.GetAllStudent();
            return Ok(allStudentDetails);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentDetails(int Id)
        {
            TestStudentRepository1 repository = new TestStudentRepository1();
           // Student studentDetails = repository.GetStudentById(Id);

            var change = repository.OftenChange();
            ///
            Student studentDetails = _repository.GetStudentById(Id);
            return Ok(studentDetails);
        }

        [HttpGet]
        [Route("change")]
        public string GetChange()
        {
            var _repository = _serviceResolver(ServiceType.StudentRepo1);
            return _repository.OftenChange();
        }
        
        [HttpGet]
        [Route("change2")]
        public string GetChange2()
        {
            //From Service implement2
            var _repository = _serviceResolver(ServiceType.StudentRepo2);
            return _repository.OftenChange();
        }

    }
}

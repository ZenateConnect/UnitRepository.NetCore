using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnitRepository.Core.Abstarct;
using UnitRepository.Domain.Concrete;
using UnitRepository.Model.Core;

namespace UnitRepository.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleClassController : ControllerBase
    {
        protected IUnitOfWork<AppDbContext> Work;

        public SampleClassController(IUnitOfWork<AppDbContext> work)
        {
            Work = work;
        }

        [HttpGet]
        public IEnumerable<SampleClass> GetAll()
        {
            List<SampleClass> listAll = new List<SampleClass>();
            try
            {
                listAll = Work.GetRepository<SampleClass>().GetAll.ToList();
            }
            catch (Exception)
            {
                // Write log here.
            }
            return listAll;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<SampleClass>> Get(int id)
        {
            List<SampleClass> obj = new List<SampleClass>();
            try
            {
                obj = Work.GetRepository<SampleClass>().Get(a => a.Id == id).ToList();
            }
            catch (Exception)
            {
                // Write log here.
            }
            return obj;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<SampleClass> Insert([FromBody]SampleClass model)
        {
            try
            {
                SampleClass obj = new SampleClass {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "System",
                    ModifiedDate = DateTime.Now
                };
                Work.GetRepository<SampleClass>().Insert(obj);
                Work.SaveChanges();

                model.Id = obj.Id;
            }
            catch (Exception)
            {
                // Write log here.
            }
            return model;
        }
    }
}
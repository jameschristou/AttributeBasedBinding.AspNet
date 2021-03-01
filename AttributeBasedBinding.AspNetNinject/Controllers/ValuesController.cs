using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AttributeBasedBinding.AspNetNinject.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IMessageProvider _messageProvider;

        public ValuesController(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return _messageProvider.GetMsg();
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public interface IMessageProvider
    {
        string GetMsg();
    }

    [Bind]
    public class MessageProvider : IMessageProvider
    {
        public string GetMsg()
        {
            return "This worked!";
        }
    }
}

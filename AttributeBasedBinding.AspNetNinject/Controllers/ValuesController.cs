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
        private readonly IAlternativeMessageProvider _alternativeMessageProvider;

        public ValuesController(IMessageProvider messageProvider, IAlternativeMessageProvider alternativeMessageProvider)
        {
            _messageProvider = messageProvider;
            _alternativeMessageProvider = alternativeMessageProvider;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            _alternativeMessageProvider.SetMsg(DateTime.Now.ToString());

            return new string[] { _messageProvider.GetMsg(), _alternativeMessageProvider.GetMsg() };
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

    public interface IAlternativeMessageProvider
    {
        string GetMsg();
        void SetMsg(string msg);
    }

    [Bind(BindingType.Singleton)]
    public class AlternativeMessageProvider : IAlternativeMessageProvider
    {
        private string _message = "Initial";

        public string GetMsg()
        {
            return _message;
        }

        public void SetMsg(string msg)
        {
            _message = _message + msg;
        }
    }
}

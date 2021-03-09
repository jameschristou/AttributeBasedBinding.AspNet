using System.Web.Http;

namespace AttributeBasedBinding.Examples.AspNetNinject.Controllers
{
    [RoutePrefix("examples")]
    public class ExamplesController : ApiController
    {
        private readonly ITransientMessageProvider _transientMessageProvider;
        private readonly ISingletonMessageProvider _singletonMessageProvider;
        private readonly ToSelfMessageProvider _toSelfMessageProvider;
        private readonly ToSelfAsSingletonMessageProvider _toSelfAsSingletonMessageProvider;
        private readonly IPerRequestMessageProvider _perRequestMessageProvider;
        private readonly ToSelfPerRequestMessageProvider _toSelfPerRequestMessageProvider;

        public ExamplesController(ITransientMessageProvider transientMessageProvider, 
                                ISingletonMessageProvider singletonMessageProvider,
                                ToSelfMessageProvider toSelfMessageProvider,
                                ToSelfAsSingletonMessageProvider toSelfAsSingletonMessageProvider,
                                IPerRequestMessageProvider perRequestMessageProvider,
                                ToSelfPerRequestMessageProvider toSelfPerRequestMessageProvider)
        {
            _transientMessageProvider = transientMessageProvider;
            _singletonMessageProvider = singletonMessageProvider;
            _toSelfMessageProvider = toSelfMessageProvider;
            _toSelfAsSingletonMessageProvider = toSelfAsSingletonMessageProvider;
            _perRequestMessageProvider = perRequestMessageProvider;
            _toSelfPerRequestMessageProvider = toSelfPerRequestMessageProvider;
        }

        [Route("transient")]
        [HttpGet]
        public IHttpActionResult GetTransient()
        {
            return Ok(new { msg = _transientMessageProvider.GetMsg() });
        }

        [Route("singleton")]
        [HttpGet]
        public IHttpActionResult GetSingleton()
        {
            return Ok(new { msg = _singletonMessageProvider.GetMsg() });
        }

        [Route("toselfastransient")]
        [HttpGet]
        public IHttpActionResult GetToSelfAsTransient()
        {
            return Ok(new { msg = _toSelfMessageProvider.GetMsg() });
        }

        [Route("toselfassingleton")]
        [HttpGet]
        public IHttpActionResult GetToSelfAsSingleton()
        {
            return Ok(new { msg = _toSelfAsSingletonMessageProvider.GetMsg() });
        }

        [Route("perrequest")]
        [HttpGet]
        public IHttpActionResult GetPerRequest()
        {
            return Ok(new { msg = _perRequestMessageProvider.GetMsg() });
        }

        [Route("toselfperrequest")]
        [HttpGet]
        public IHttpActionResult GetToSelfPerRequest()
        {
            return Ok(new { msg = _toSelfPerRequestMessageProvider.GetMsg() });
        }
    }

    public interface ITransientMessageProvider
    {
        string GetMsg();
    }

    [Bind]
    public class TransientMessageProvider : MessageProvider, ITransientMessageProvider
    {

    }

    public interface ISingletonMessageProvider
    {
        string GetMsg();
    }

    [BindAsSingleton]
    public class SingletonMessageProvider : MessageProvider, ISingletonMessageProvider
    {

    }

    [BindToSelf]
    public class ToSelfMessageProvider : MessageProvider
    {

    }

    [BindToSelfAsSingleton]
    public class ToSelfAsSingletonMessageProvider : MessageProvider
    {

    }

    public interface IPerRequestMessageProvider
    {
        string GetMsg();
    }

    [BindPerRequest]
    public class PerRequestMessageProvider : MessageProvider, IPerRequestMessageProvider
    {

    }

    [BindToSelfPerRequest]
    public class ToSelfPerRequestMessageProvider : MessageProvider
    {

    }

    public class MessageProvider
    {
        private string _message = "Message";

        public string GetMsg()
        {
            _message = $"{_message}_{_message}";

            return _message;
        }
    }

    public interface IAlternativeMessageProvider
    {
        string GetMsg();
    }

    [BindAsSingleton]
    public class AlternativeMessageProvider : IAlternativeMessageProvider
    {
        private string _message = "AlternativeMessage";

        public string GetMsg()
        {
            _message = $"{_message}_{_message}";

            return _message;
        }
    }
}

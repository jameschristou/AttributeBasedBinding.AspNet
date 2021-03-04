using System.Web.Http;

namespace AttributeBasedBinding.AspNetNinject.Controllers
{
    [RoutePrefix("examples")]
    public class ExamplesController : ApiController
    {
        private readonly ITransientMessageProvider _transientMessageProvider;
        private readonly ISingletonMessageProvider _singletonMessageProvider;
        private readonly ToSelfMessageProvider _toSelfMessageProvider;
        private readonly ToSelfAsSingletonMessageProvider _toSelfAsSingletonMessageProvider;

        public ExamplesController(ITransientMessageProvider transientMessageProvider, 
                                ISingletonMessageProvider singletonMessageProvider,
                                ToSelfMessageProvider toSelfMessageProvider,
                                ToSelfAsSingletonMessageProvider toSelfAsSingletonMessageProvider)
        {
            _transientMessageProvider = transientMessageProvider;
            _singletonMessageProvider = singletonMessageProvider;
            _toSelfMessageProvider = toSelfMessageProvider;
            _toSelfAsSingletonMessageProvider = toSelfAsSingletonMessageProvider;
        }

        // GET api/values
        [Route("transient")]
        [HttpGet]
        public string GetTransient()
        {
            return _transientMessageProvider.GetMsg();
        }

        // GET api/values/5
        [Route("singleton")]
        [HttpGet]
        public string GetSingleton()
        {
            return _singletonMessageProvider.GetMsg();
        }

        [Route("selfastransient")]
        [HttpGet]
        public string GetSelfAsTransient()
        {
            return _toSelfMessageProvider.GetMsg();
        }

        [Route("selfassingleton")]
        [HttpGet]
        public string GetSelfAsSingleton()
        {
            return _toSelfAsSingletonMessageProvider.GetMsg();
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

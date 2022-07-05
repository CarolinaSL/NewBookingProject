namespace MessageBus
{
    using System;
    using System.Threading.Tasks;
    using EasyNetQ;
    using EasyNetQ.Internals;

    
        public interface IMessageBus : IDisposable
        {
            bool IsConnected { get; }
            IAdvancedBus AdvancedBus { get; }

            void Publish<T>(T message) where T : class;

            Task PublishAsync<T>(T message) where T : class;

            void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

            AwaitableDisposable<ISubscriptionResult> SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;

            TResponse Request<TRequest, TResponse>(TRequest request)
                where TRequest : class
                where TResponse : class;

            Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
               where TRequest : class
                where TResponse : class;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
                 where TRequest : class
                where TResponse : class;

        AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
                where TRequest : class
                where TResponse : class;
    }
    }

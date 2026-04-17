using System;
using System.Collections.Generic;

namespace Develop.Runtime.Infrastructure.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly List<Type> _requests = new();

        public void RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            Registration registration = new Registration(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);
        }

        public T Resolve<T>()
        {
            if(_requests.Contains(typeof(T)))
                throw  new InvalidOperationException($"Cyclic dependency detected: {typeof(T)}");
            
            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return (T)registration.CreateInstanceFrom(this);
            }
            finally
            {
                _requests.Remove(typeof(T));
            }
            
            throw new InvalidOperationException($"Registration for {typeof(T)} not exists");
        }
    }
}
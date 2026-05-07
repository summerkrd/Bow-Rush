using System;

namespace Develop.Runtime.Infrastructure.DI
{
    public class Registration : IRegistrationOptions
    {
        private Func<DIContainer, object> _creator;
        private object _cashedInstance;
        
        public bool IsNonLazy { get; private set; }

        public Registration(Func<DIContainer, object> creator) => _creator = creator;

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cashedInstance != null)
                return _cashedInstance;
            
            if (_creator == null)
                throw new InvalidOperationException("Not has instances or creator");
            
            _cashedInstance = _creator.Invoke(container);
            return _cashedInstance;
        }
        
        public void NonLazy() => IsNonLazy = true;
    }
}
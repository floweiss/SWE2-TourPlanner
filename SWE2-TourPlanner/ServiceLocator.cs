using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE2_TourPlanner
{
    public class ServiceLocator : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            lock (_services)
            {
                if (_services.ContainsKey(serviceType))
                    return _services[serviceType];
            }
            return null;
        }

        public bool RegisterService<T>(T service, bool overwriteIfExists = false)
        {
            lock (_services)
            {
                if (!_services.ContainsKey(typeof(T)))
                {
                    _services.Add(typeof(T), service);
                    return true;
                }

                if (!overwriteIfExists) return false;
                _services[typeof(T)] = service;
                return true;
            }
        }
    }
}

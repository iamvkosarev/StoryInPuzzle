using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryInPuzzle.Infrastructure
{
    public class ServicesContainer
    {
        private readonly Dictionary<Type, IService> _services = new();

        public void RegisterServiceInterfaces<T>() where T : class, IService
        {
            //Debug.Log("<color=green>Register</color>: "+typeof(T));
            var assembly = typeof(T).Assembly;
            var types = assembly.GetTypes().Where(t =>
                typeof(IService).IsAssignableFrom(t) && t.IsAssignableFrom(typeof(T)) && t.IsInterface &&
                t != typeof(IService));


            foreach (var type in types)
            {
                if (_services.ContainsKey(type))
                    throw new InvalidOperationException($"Service type of '{type}' was already registered");
                _services[type] = GetInstance<T>();
                //Debug.Log($"<color=yellow>Service</color> '{typeof(T)} registered into {type}'");
            }
        }

        public void RegisterServiceInterfacesFromInstance<T>(T service)
            where T : class, IService
        {
            var assembly = typeof(T).Assembly;
            var types = assembly.GetTypes().Where(t =>
                typeof(IService).IsAssignableFrom(t) && t.IsAssignableFrom(typeof(T)) && t.IsInterface &&
                t != typeof(IService));


            foreach (var type in types)
            {
                if (_services.ContainsKey(type))
                    throw new InvalidOperationException($"Service type of '{type}' was already registered");
                _services[type] = service;
            }
        }

        public T GetInstance<T>() where T : class
        {
            var constructor = typeof(T).GetConstructors().SingleOrDefault();
            if (constructor == null) return Activator.CreateInstance<T>();
            var parameters = constructor.GetParameters();
            object[] arguments = parameters.Select(param =>
            {
                if (_services.ContainsKey(param.ParameterType))
                {
                    return _services[param.ParameterType];
                }

                throw new InvalidOperationException($"Service for type '{param.ParameterType.Name}' not found");
            }).ToArray();

            return (T)constructor.Invoke(arguments);
        }
    }
}
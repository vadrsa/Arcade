﻿using System;
using System.Collections.Generic;

namespace Kernel.Configuration
{
    public class OptionConfiguration : IOptionConfiguration
    {
        Dictionary<Type, Object> Options = new Dictionary<Type, Object>();

        public OptionConfiguration Configure<T>(T option)
        {
            Options[typeof(T)] = option;
            return this;
        }

        public OptionConfiguration Configure<T>(Action<T> init)
        {
            T option = Activator.CreateInstance<T>();
            init(option);
            Configure(option);
            return this;
        }

        public bool IsConfigured<T>()
        {
            return Options.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Get the option of type T
        /// </summary>
        /// <typeparam name="T">Type of the option</typeparam>
        /// <returns>The option if configured</returns>
        public T GetOption<T>() where T : class
        {
            if (!Options.ContainsKey(typeof(T)))
                return null;
            return (T)Options[typeof(T)];
        }
    }
}

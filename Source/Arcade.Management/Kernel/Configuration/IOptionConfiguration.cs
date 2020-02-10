﻿using System;

namespace Kernel.Configuration
{
    /// <summary>
    /// Configuration interface powered by options pattern
    /// </summary>
    public interface IOptionConfiguration
    {
        void Configure<T>(T option);

        void Configure<T>(Action<T> init);

        /// <summary>
        /// Get the option of type T
        /// </summary>
        /// <typeparam name="T">Type of the option</typeparam>
        /// <returns>The option if configured, otherwise null</returns>
        T GetOption<T>() where T : class;
    }
}

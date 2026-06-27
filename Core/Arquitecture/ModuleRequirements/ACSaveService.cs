using UnityEngine;
using System.Collections.Generic;
using System;

namespace AshenCore.Core
{

    public class ACSaveService
    {

        public Dictionary<string, IGlobalPersistenceProvider> GlobalProviders = new Dictionary<string, IGlobalPersistenceProvider>();
        public Dictionary<Guid, IRuntimePersistenceProvider> RuntimeProviders = new Dictionary<Guid, IRuntimePersistenceProvider>();

        private ILog Log;
        
        public void SetLog(ILog _log) { Log = _log; }

        public Dictionary<string, object> CaptureGlobalData()
        {
            Dictionary<string, object> capturedData = new Dictionary<string, object>();

            foreach (var provider in GlobalProviders)
            {
                Log?.Log($"Capturing data from global provider with key {provider.Key}", ConsoleMessageType.Verbose);
                capturedData[provider.Key] = provider.Value.Capture();
            }

            return capturedData;
        }

        public void RestoreGlobalData(Dictionary<string, object> data)
        {
            foreach (var provider in GlobalProviders)
            {
                if (data.TryGetValue(provider.Key, out var providerData))
                {
                    Log?.Log($"Restoring data for global provider with key {provider.Key}");
                    provider.Value.Restore(providerData);
                }
                else
                {
                    Log?.Warn($"No data found for global provider with key {provider.Key}");
                }
            }
        }

        public Dictionary<Guid, object> CaptureRuntimeData()
        {
            Dictionary<Guid, object> capturedData = new Dictionary<Guid, object>();

            foreach (var provider in RuntimeProviders)
            {
                Log?.Log($"Capturing data from runtime provider with ID {provider.Key}");
                capturedData[provider.Key] = provider.Value.Capture();
            }

            return capturedData;
        }

        public void RestoreRuntimeData(Dictionary<Guid, object> data)
        {
            foreach (var provider in RuntimeProviders)
            {
                if (data.TryGetValue(provider.Key, out var providerData))
                {
                    Log?.Log($"Restoring data for runtime provider with ID {provider.Key}");
                    provider.Value.Restore(providerData);
                }
                else
                {
                    Log?.Warn($"No data found for runtime provider with ID {provider.Key}");
                }
            }
        }

        public void RegisterGlobalProvider(
            IGlobalPersistenceProvider provider)
        {
            if (!GlobalProviders.ContainsKey(provider.Key))
            {
                Log?.Log($"Registering global provider with key {provider.Key}");
                GlobalProviders.Add(provider.Key, provider);
            }
            else
            {
                Log?.Warn($"Global provider with key {provider.Key} is already registered.");
            }
        }

        public void UnregisterGlobalProvider(
            IGlobalPersistenceProvider provider)
        {
            if (GlobalProviders.ContainsKey(provider.Key))
            {
                Log?.Log($"Unregistering global provider with key {provider.Key}");
                GlobalProviders.Remove(provider.Key);
            }
            else
            {
                Log?.Warn($"Global provider with key {provider.Key} is not registered.");
            }
        }

        public void RegisterRuntimeProvider(
            IRuntimePersistenceProvider provider)
        {
            if (!RuntimeProviders.ContainsKey(provider.Key))
            {
                Log?.Log($"Registering runtime provider with ID {provider.Key}");
                RuntimeProviders.Add(provider.Key, provider);
            }
            else
            {
                Log?.Warn($"Runtime provider with ID {provider.Key} is already registered.");
            }
        }

        public void UnregisterRuntimeProvider(
            IRuntimePersistenceProvider provider)
        {
            if (RuntimeProviders.ContainsKey(provider.Key))
            {
                Log?.Log($"Unregistering runtime provider with ID {provider.Key}");
                RuntimeProviders.Remove(provider.Key);
            }
            else
            {
                Log?.Warn($"Runtime provider with ID {provider.Key} is not registered.");
            }
        }

    }
}
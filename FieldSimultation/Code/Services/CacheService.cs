using System;
using System.Collections.Generic;

namespace FieldSimultation.Code.Services;

public class CacheService
{
    Dictionary<string, object> _cache = new Dictionary<string, object>();

    private static CacheService _instance;

    // Singleton instance to share across the app
    public static CacheService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CacheService();
            }
            return _instance;
        }
    }

    public CacheService()
    {
    }

    // Add an item to the cache
    public void AddToCache(string key, object value)
    {
        _cache.Add(key, value);
    }

    // Retrieve an item from the cache
    public object GetFromCache(string key)
    {
        return _cache[key];
    }

    // Remove an item from the cache
    public void RemoveFromCache(string key)
    {
        _cache.Remove(key);
    }
}

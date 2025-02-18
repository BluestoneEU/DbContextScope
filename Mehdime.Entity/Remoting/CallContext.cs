﻿using System.Collections.Concurrent;
using System.Threading;

namespace Remoting
{
    public static class CallContext<T>
    {
        static ConcurrentDictionary<string, AsyncLocal<T>> state = new ConcurrentDictionary<string, AsyncLocal<T>>();

        public static void SetData(string name, T data) =>
            state.AddOrUpdate(name, new AsyncLocal<T>(), (_, __) => new AsyncLocal<T>()).Value = data;
            // state.GetOrAdd(name, _ => new AsyncLocal<T>()).Value = data;

        public static T GetData(string name) =>
            state.TryGetValue(name, out AsyncLocal<T> data) ? data.Value : default(T);
    }
}

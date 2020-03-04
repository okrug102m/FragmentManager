using System;
using System.Collections.Generic;

namespace FragmentManager
{
  public sealed class MessageBus : IMessenger, IDisposable
  {
    private readonly Dictionary<Type, List<object>> subscribers = new Dictionary<Type, List<object>>();

    public void Subscribe<TMessage>(Action<TMessage> handler)
    {
      if (subscribers.ContainsKey(typeof (TMessage)))
        subscribers[typeof (TMessage)].Add(handler);
      else
        subscribers[typeof (TMessage)] = new List<object>
        {
          handler
        };
    }

    public void Unsubscribe<TMessage>(Action<TMessage> handler)
    {
      if (!subscribers.ContainsKey(typeof (TMessage)))
        return;
      List<object> subscriber = subscribers[typeof (TMessage)];
      subscriber.Remove(handler);
      if (subscriber.Count != 0)
        return;
      subscribers.Remove(typeof (TMessage));
    }

    public void Publish<TMessage>(TMessage message)
    {
      if (!subscribers.ContainsKey(typeof (TMessage)))
        return;
      foreach (Action<TMessage> action in subscribers[typeof (TMessage)])
        action(message);
    }

    public void Dispose()
    {
      subscribers.Clear();
    }
  }
}

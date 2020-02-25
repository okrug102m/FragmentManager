using System;
using System.Collections.Generic;

namespace FragmentManager
{
  public sealed class MessageBus : IMessenger, IDisposable
  {
    private readonly Dictionary<Type, List<object>> subscribers = new Dictionary<Type, List<object>>();

    public void Subscribe<TMessage>(Action<TMessage> handler)
    {
      if (this.subscribers.ContainsKey(typeof (TMessage)))
        this.subscribers[typeof (TMessage)].Add((object) handler);
      else
        this.subscribers[typeof (TMessage)] = new List<object>()
        {
          (object) handler
        };
    }

    public void Unsubscribe<TMessage>(Action<TMessage> handler)
    {
      if (!this.subscribers.ContainsKey(typeof (TMessage)))
        return;
      List<object> subscriber = this.subscribers[typeof (TMessage)];
      subscriber.Remove((object) handler);
      if (subscriber.Count != 0)
        return;
      this.subscribers.Remove(typeof (TMessage));
    }

    public void Publish<TMessage>(TMessage message)
    {
      if (!this.subscribers.ContainsKey(typeof (TMessage)))
        return;
      foreach (Action<TMessage> action in this.subscribers[typeof (TMessage)])
        action(message);
    }

    public void Dispose()
    {
      this.subscribers.Clear();
    }
  }
}

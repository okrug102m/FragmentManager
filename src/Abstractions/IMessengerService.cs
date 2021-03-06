﻿using System;

namespace FragmentManager.Abstractions
{
  public interface IMessengerService : IDisposable
  {
    void Publish<T>(T message);

    void Subscribe<T>(Action<T> handler);

    void Unsubscribe<T>(Action<T> handler);
  }
}

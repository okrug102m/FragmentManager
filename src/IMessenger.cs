﻿using System;

namespace FragmentManager
{
  public interface IMessenger : IDisposable
  {
    void Publish<T>(T message);

    void Subscribe<T>(Action<T> handler);

    void Unsubscribe<T>(Action<T> handler);
  }
}

using System;
using UnityEngine;

namespace Tools.Ui
{
    public interface IPoolableElementController<TKey, TView> : IDisposable
        where TView : Component
    {
        Transform Transform { get; }
        void SetView(TView view);
        void Open(TKey key);
        void Close();
    }
}
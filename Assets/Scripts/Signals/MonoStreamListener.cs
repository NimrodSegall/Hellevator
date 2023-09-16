using UnityEngine;
using System;
using UniRx;
using Zenject;


public abstract class UninitializedMonoStreamListener<T> : MonoBehaviour, IInitializable, IDisposable where T : ISignal
{
    [Inject] protected SignalBus _signalBus;
    protected readonly CompositeDisposable _disposables = new CompositeDisposable();

    public virtual void Initialize()
    {
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}

public class MonoStreamListener<T> : UninitializedMonoStreamListener<T> where T : ISignal
{
    public override void Initialize()
    {
        base.Initialize();
        _signalBus.GetStream<T>()
            .Subscribe(x => InvokedFromSignal(x)).AddTo(_disposables);
    }

    public virtual void InvokedFromSignal(T signal)
    {

    }
}

public abstract class MonoStreamListener<T1, T2> : MonoStreamListener<T1> where T1 : ISignal where T2 : ISignal
{
    public override void Initialize()
    {
        base.Initialize();
        _signalBus.GetStream<T2>()
            .Subscribe(x => InvokedFromSignal(x)).AddTo(_disposables);
    }

    public virtual void InvokedFromSignal(T2 signal)
    {

    }
}

public abstract class MonoStreamListener<T1, T2, T3> : MonoStreamListener<T1, T2>
    where T1 : ISignal where T2 : ISignal where T3 : ISignal
{
    public override void Initialize()
    {
        base.Initialize();
        _signalBus.GetStream<T3>()
            .Subscribe(x => InvokedFromSignal(x)).AddTo(_disposables);
    }

    public virtual void InvokedFromSignal(T3 signal)
    {

    }
}


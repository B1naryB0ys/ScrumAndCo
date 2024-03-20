﻿namespace ScrumAndCo.Domain.Notifications;

public class NotificationSubject<T> : ISubject<T>
{
    private List<IObserver<T>> _observers = new List<IObserver<T>>();
    
    public void Attach(IObserver<T> observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver<T> observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(T value)
    {
        foreach (var observer in _observers)
        {
            observer.Update(value);
        }
    }
}
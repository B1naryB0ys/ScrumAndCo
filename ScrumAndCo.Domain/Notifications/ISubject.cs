﻿namespace ScrumAndCo.Domain.Notifications;

public interface ISubject<T>
{
    void Attach(IObserver<T> observer);
    void Detach(IObserver<T> observer);
    void Notify(T value);
    void NotifySingle(T value, IObserver<T> observer);
}
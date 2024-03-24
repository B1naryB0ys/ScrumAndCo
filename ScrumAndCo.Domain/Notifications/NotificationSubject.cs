namespace ScrumAndCo.Domain.Notifications;

public class NotificationSubject<T> : ISubject<T>
{
    private List<IObserver<T>> _observers = new List<IObserver<T>>();
    
    public void Attach(IObserver<T> observer)
    {
        _observers.Add(observer);
    }

    public void NotifyAll(T value)
    {
        foreach (var observer in _observers)
        {
            observer.Update(value);
        }
    }

    public void NotifySingle(T value, IObserver<T> observer)
    {
        observer.Update(value);
    }
}
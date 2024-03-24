namespace ScrumAndCo.Domain.Notifications;

public interface ISubject<T>
{
    void Attach(IObserver<T> observer);
    void NotifyAll(T value);
    void NotifySingle(T value, IObserver<T> observer);
}
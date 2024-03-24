namespace ScrumAndCo.Domain.Notifications;

public interface IObserver<T>
{
    void Update(T value);
}
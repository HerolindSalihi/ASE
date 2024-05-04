public class Safe<T>
{
    private readonly T _value;

    public Safe()
    {
        // Hier können Sie einen Standardwert für _value setzen
        // Je nach Typ von T könnte dies z. B. default(T) sein
        _value = default(T);
    }

    public Safe(T value)
    {
        _value = value;
    }

    public T Value => _value;

    public T GetValueOrDefault()
    {
        return _value;
    }

    public T GetValueOrDefault(T defaultValue)
    {
        return _value != null ? _value : defaultValue;
    }

    public static implicit operator Safe<T>(T value)
    {
        if (value == null)
        {
            return new Safe<T>();
        }
        else
        {
            return new Safe<T>(value);
        }
    }

    public static implicit operator T(Safe<T> safe)
    {
        return safe._value;
    }
}
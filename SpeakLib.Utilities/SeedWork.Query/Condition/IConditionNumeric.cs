namespace SpeakFriend.Utilities
{
    public interface IConditionNumeric
    {
        string PropertyName { get; set; }
        object GetValue();

        bool IsGreaterThan();
        bool IsSet();
        bool IsEqualTo();

    }
}
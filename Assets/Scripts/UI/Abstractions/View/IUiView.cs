namespace UI.Abstractions.View
{
    public interface IUiView
    {
        bool AutoShow { get; }
        void Show();
        void Hide();
    }
}

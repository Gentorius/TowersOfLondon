namespace UI.Basics
{
    public interface IPresenter
    {
        bool IsShown { get; }
        virtual void Initialize(UIManager uiManager) { }
        void LoadAndShowWindow();
        void HideWindow();
        void ShowWindow();
        void LoadWindow();
        void DestroyWindow();
    }
}
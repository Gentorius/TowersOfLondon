namespace UI.Basics
{
    public abstract class BasicPresenter<TView> : IPresenter where TView : BasicView
    {
        protected UIManager UIManager;
        protected TView View;
        public bool IsShown => View && View.gameObject.activeSelf;

        public virtual void Initialize(UIManager uiManager)
        {
            UIManager = uiManager;
        }
        
        public void LoadAndShowWindow()
        {
            LoadWindow();
            ShowWindow();
        }
        
        public void HideWindow()
        {
            UIManager.HideView(View);
            OnHide();
        }
        
        public void ShowWindow()
        {
            UIManager.ShowView(View);
            OnShow();
        }
        
        public void LoadWindow()
        {
            View = UIManager.LoadView<TView>();
        }
        
        public void DestroyWindow()
        {
            UIManager.DestroyView(View);
            OnHide();
        }
        
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
    }
}
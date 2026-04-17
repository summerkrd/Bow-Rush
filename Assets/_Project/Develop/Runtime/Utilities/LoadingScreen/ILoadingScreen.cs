namespace Develop.Runtime.Gameplay.Infrastructure
{
    public interface ILoadingScreen
    {
        bool IsShown { get; }
        
        void Show();
        
        void Hide();
    }
}
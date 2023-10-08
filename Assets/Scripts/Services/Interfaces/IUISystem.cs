
namespace Game.Services
{
    public interface IUISystem
    {
        public FaderService FaderService { get; }
        public WindowsService WindowsService { get; }
    }
}
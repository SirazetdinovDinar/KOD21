using UnityEngine;

namespace Game.Services
{
    public class UISystem : MonoBehaviour, IUISystem
    {
        [SerializeField]
        private FaderService faderService;
        [SerializeField]
        private WindowsService windowsService;

        public FaderService FaderService => faderService;
        public WindowsService WindowsService => windowsService;
    }
}
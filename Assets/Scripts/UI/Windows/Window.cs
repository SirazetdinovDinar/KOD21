using UnityEngine;
using Game.Services;

namespace Game.UI
{
    public abstract class Window : MonoBehaviour
    {
        protected WindowsService windowsService;

        public bool IsActive => windowsService.IsActive(this);

        public virtual void Init(object data)
        {
        }

        public virtual void OnClose()
        {

        }

        public void CloseSelf()
        {
            windowsService.Close(this);
        }
    }
}
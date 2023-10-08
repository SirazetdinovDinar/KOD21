using UnityEngine;
using System.Threading.Tasks;

namespace Game.Services
{
    public class FaderService : MonoBehaviour
    {
        [SerializeField]
        private float fadeInDuration;
        [SerializeField]
        private float fadeOutDuration;
        [SerializeField]
        private CanvasGroup overlay;
        [SerializeField]
        private bool fadeOutAtAwake = true;

        private bool fadingIn;
        private bool fadingOut;
        private async void Awake()
        {
            if (fadeOutAtAwake)
            {
                await FadeOut();
            }
        }

        public async Task FadeIn()
        {
            fadingIn = true;

            var delta = 1f - overlay.alpha;
            while (overlay != null && overlay.alpha < 1f)
            {
                if (fadingOut)
                {
                    fadingIn = false;
                    return;
                }
                overlay.alpha += delta * Time.deltaTime / fadeInDuration;
                await Task.Yield();
            }

            if (overlay != null)
            {
                overlay.alpha = 1f;
            }

            fadingIn = false;
        }

        public async Task FadeOut()
        {
            fadingOut = true;

            var delta = overlay.alpha;
            while (overlay != null && overlay.alpha > 0f)
            {
                if(fadingIn)
                {
                    fadingOut = false;
                    return;
                }
                overlay.alpha -= delta * Time.deltaTime / fadeInDuration;
                await Task.Yield();
            }

            if (overlay != null)
            {
                overlay.alpha = 0f;
            }

            fadingOut = false;
        }
    }
}
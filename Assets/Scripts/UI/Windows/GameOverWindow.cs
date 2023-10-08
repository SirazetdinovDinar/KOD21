using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Game.Services;
using TMPro;

namespace Game.UI
{
    public class GameOverWindow : Window
    {
        public Action OnSubmit;
        [SerializeField]
        private TMP_Text label;
        [SerializeField]
        private InputActionReference submitActionRef;

        private float timeScaleCached;

        public override void OnClose()
        {
            base.OnClose();
            Time.timeScale = timeScaleCached;
        }

        public override void Init(object data)
        {
            if (data is string text)
            {
                label.text = text;
            }

            timeScaleCached = Time.timeScale;
            Time.timeScale = 0;

            submitActionRef.action.performed += ProcessActionSubmit;
        }

        private void ProcessActionSubmit(InputAction.CallbackContext ctx)
        {
            OnSubmit?.Invoke();
        }

        private void OnDestroy()
        {
            submitActionRef.action.performed -= ProcessActionSubmit;
            Time.timeScale = timeScaleCached;
        }
    }
}
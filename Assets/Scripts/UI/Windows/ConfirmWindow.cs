using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Game.Services;
using TMPro;

namespace Game.UI
{
    public class ConfirmWindow : Window
    {
        public Action OnSubmit;
        public Action OnCancel;
        [SerializeField]
        private Button submitButton;
        [SerializeField]
        private Button cancelButton;
        [SerializeField]
        private TMP_Text label;
        [SerializeField]
        private InputActionReference submitActionRef;
        [SerializeField]
        private InputActionReference cancelActionRef;

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
            cancelActionRef.action.performed += ProcessActionCancel;

            submitButton.onClick.AddListener(OnSubmitButtonClick);
            cancelButton.onClick.AddListener(OnCancelButtonClick);
        }

        private void OnCancelButtonClick()
        {
            OnCancel?.Invoke();
        }

        private void OnSubmitButtonClick()
        {
            OnSubmit?.Invoke();
        }

        private void ProcessActionCancel(InputAction.CallbackContext ctx)
        {
            OnCancel?.Invoke();
        }

        private void ProcessActionSubmit(InputAction.CallbackContext ctx)
        {
            OnSubmit?.Invoke();
        }

        private void OnDestroy()
        {
            submitActionRef.action.performed -= ProcessActionSubmit;
            cancelActionRef.action.performed -= ProcessActionCancel;
        }
    }
}
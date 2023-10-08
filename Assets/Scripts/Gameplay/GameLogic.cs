using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.UI;
using Game.Services;
using Game.Messages;
using System;
using System.Collections.Generic;

namespace Game
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] private WindowsService windowsService;
        [SerializeField] private FaderService faderService;
        [SerializeField] private InputActionReference inputActionReference;

        private ConfirmWindow exitGameWindow;
        private GameOverWindow gameOverWindow;
        private List<IDisposable> disposables = new();

        private void Awake()
        {
            windowsService.Open<PlayerHUDWindow>();
            inputActionReference.action.performed += OnCancel;
            disposables.Add(MessagesService.Subscribe<FogExtremeReached>(OnFogExtremeReached));
        }

        private void OnFogExtremeReached(FogExtremeReached ctx)
        {
            gameOverWindow = windowsService.Open<GameOverWindow>("������");
            gameOverWindow.OnSubmit += OnRestart;
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            if (!windowsService.IsActive(gameOverWindow))
            {
                if (!windowsService.IsActive(exitGameWindow))
                {
                    exitGameWindow = windowsService.Open<ConfirmWindow>("����� �� ����?");
                    exitGameWindow.OnSubmit += OnExitGame;
                }
                else
                {
                    windowsService.Close(exitGameWindow);
                }
            }
        }

        private void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        private void OnExitGame()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            inputActionReference.action.performed -= OnCancel;
            foreach (var d in disposables)
            {
                d.Dispose();
            }
        }
    }
}

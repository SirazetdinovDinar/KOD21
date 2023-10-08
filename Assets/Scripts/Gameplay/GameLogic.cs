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
            gameOverWindow = windowsService.Open<GameOverWindow>("Провал");
            gameOverWindow.OnSubmit += OnRestart;
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            if (!windowsService.IsActive(exitGameWindow) && !windowsService.IsActive(gameOverWindow))
            {
                exitGameWindow = windowsService.Open<ConfirmWindow>("Выйти из игры?");
                exitGameWindow.OnCancel += OnExitPauseMenu;
                exitGameWindow.OnSubmit += OnExitGame;
            }
        }

        private void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        private void OnExitPauseMenu()
        {
            windowsService.Close(exitGameWindow);
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

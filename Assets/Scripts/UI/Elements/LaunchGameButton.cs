using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace UI.Elements
{
    public class LaunchGameButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text buttonText;
        private readonly string newGame = "New Game";
        private readonly string continueGame = "Continue Game";
        private Action onClick;

        private void Awake() => button.onClick.AddListener(LaunchGameButtonClick);
        public void AddGameLaunchListener(Action listener) => onClick += listener;
        public void RemoveGameLaunchListener(Action listener) => onClick -= listener;
        public void SetButtonText(bool isNewGame) => buttonText.text = isNewGame ? newGame : continueGame;
        private void LaunchGameButtonClick() => onClick?.Invoke();
        private void OnDestroy() => button.onClick.RemoveListener(LaunchGameButtonClick);
    }
}
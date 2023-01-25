using Core.Services.Input;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FightButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IInputService _input;

        private void Start() => 
            _button.onClick.AddListener(OnButtonPressed);

        private void OnDestroy() => 
            _button.onClick.RemoveListener(OnButtonPressed);

        
        public void Construct(IInputService input) => 
            _input = input;

        public void Show()
        {
            _button.interactable = true;
            transform.DOScale(Vector3.one, 0.5f);
        }

        private void OnButtonPressed()
        {
            Hide();
            _input.InvokeFightButtonPressed();
        }

        private void Hide()
        {
            _button.interactable = false;
            transform.DOScale(Vector3.zero, 0.5f);
        }
    }
}
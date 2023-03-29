using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI keyMoveUpText;
   [SerializeField] private TextMeshProUGUI keyMoveDownText;
   [SerializeField] private TextMeshProUGUI keyMoveLeftText;
   [SerializeField] private TextMeshProUGUI keyMoveRightText;
   [SerializeField] private TextMeshProUGUI keyInteractText;
   [SerializeField] private TextMeshProUGUI keyInteractAltText;
   [SerializeField] private TextMeshProUGUI keyPauseText;
   [SerializeField] private TextMeshProUGUI keyMoveGamepadText;
   [SerializeField] private TextMeshProUGUI keyInteractGamepadText;
   [SerializeField] private TextMeshProUGUI keyInteractAltGamepadText;
   [SerializeField] private TextMeshProUGUI keyPauseGamepadText;

   private void Start()
   {
       GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
       GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
       UpdateVisual();
       Show();
   }

   private void GameManager_OnStateChanged(object sender, EventArgs e)
   {
       if (GameManager.Instance.IsCountdownToStartActive())
       {
           Hide();
       }
   }

   private void GameInput_OnBindingRebind(object sender, EventArgs e)
   {
       UpdateVisual();
   }

   private void UpdateVisual()
   {
       keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
       keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
       keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
       keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
       keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
       keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
       keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
       keyMoveGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
       keyInteractGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
       keyInteractAltGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
       keyPauseGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
   }

   private void Show()
   {
       gameObject.SetActive(true);
   }
   private void Hide()
   {
       gameObject.SetActive(false);
   }
}

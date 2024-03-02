
using UnityEngine;

public class InputManager : SingletonBase<InputManager>
{
    private bool isEnabled = true;

    public InputManager() { MonoManager.Instance().AddUpdateListener(InputUpdate); }
    public void SetCheckState(bool state) { isEnabled = state; }

    private void CheckKeyEvent(KeyCode key)
    {
        if (Input.GetKeyDown(key))
            EventManager.Instance().EventTrigger("KEY_DOWN", key);
        if (Input.GetKeyUp(key))
            EventManager.Instance().EventTrigger("KEY_UP", key);
    }

    private void CheckDirectionPad()
    {
        CheckKeyEvent(KeyCode.W);
        CheckKeyEvent(KeyCode.A);
        CheckKeyEvent(KeyCode.S);
        CheckKeyEvent(KeyCode.D);
    }
    
    private void InputUpdate()
    {
        if (!isEnabled) return;
        CheckDirectionPad();
    }
}

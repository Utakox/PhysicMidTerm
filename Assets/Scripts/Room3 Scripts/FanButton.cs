using UnityEngine;

public class FanButton : MonoBehaviour
{
    public enum ButtonType
    {
        Power,
        Plus,
        Minus
    }

    public ButtonType buttonType;
    public FanController fan;

    void OnMouseDown()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (buttonType == ButtonType.Power)
            fan.ToggleFan();

        if (buttonType == ButtonType.Plus)
            fan.IncreaseWind();

        if (buttonType == ButtonType.Minus)
            fan.DecreaseWind();
    }
}
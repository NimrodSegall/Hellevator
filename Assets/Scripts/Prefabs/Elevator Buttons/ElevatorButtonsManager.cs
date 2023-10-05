using System.Collections.Generic;

public class ElevatorButtonsManager
{
    private Dictionary<ElevatorButton, bool> buttonsSelectedState = new Dictionary<ElevatorButton, bool>();

    public void AddButton(ElevatorButton button)
    {
        buttonsSelectedState.Add(button, button.IsSelected);
    }

    public void SetButtonSelectedState(ElevatorButton button)
    {
        buttonsSelectedState[button] = button.IsSelected;
    }

    public bool IsAnyButtonSelected()
    {
        foreach (var selectedState in buttonsSelectedState.Values)
        {
            if (selectedState)
            {
                return true;
            }
        }
        return false;
    }
}

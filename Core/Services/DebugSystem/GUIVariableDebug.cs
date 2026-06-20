using UnityEngine;
using System.Collections.Generic;
using VContainer;
using AshenCore.Core;

public class GUIVariableDebug : MonoBehaviour
{
    public GameObject variablePrefab;
    public Transform variableContainer;


    public List<GUIVarController> variableControllers = new List<GUIVarController>();

    [Inject]
    void Construct(ACDebugSystem debugSystem)
    {
        debugSystem._variableDebug = this;
    }


        
    public void SetVariable(string varName, string varValue)
    {
        GUIVarController controller = variableControllers.Find(vc => vc.varNameText.text == varName);

        if (controller == null)
        {
            GameObject newVar = Instantiate(variablePrefab, variableContainer);
            controller = newVar.GetComponent<GUIVarController>();
            controller.varNameText.text = varName;
            variableControllers.Add(controller);
        }

        controller.varValueText.text = varValue;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExposeToScriptUI : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggleButton = default;

    [SerializeField]
    private TextMeshProUGUI _exposedText = default;

    void Start()
    {
        UpdateVisuals();
        SceneManager.OnUpdateSelection.AddListener(UpdateVisuals);
        _toggleButton.onValueChanged.AddListener(SetExpose);
    }

    public void UpdateVisuals()
    {
        if (SceneManager.SelectedEntity == null)
            return;

        _toggleButton.isOn = SceneManager.SelectedEntity.Exposed;
        _exposedText.color = Color.white; // ToDo use Color manager
        _exposedText.text = SceneManager.SelectedEntity.Exposed ? SceneManager.SelectedEntity.ExposedSymbol : "";
    }

    public void SetExpose(bool _)
    {
        if (SceneManager.SelectedEntity == null)
            return;

        var expose = _toggleButton.isOn;
        
        if (!expose) 
        {
            SceneManager.SelectedEntity.Exposed = false;
            UpdateVisuals();
            return;
        } 

        if (!SceneManager.SelectedEntity.TrySetExpose(true))
        {
            UpdateVisuals();
            _exposedText.color = Color.red;
            _exposedText.text = "Failed to expose";
            return;
        }
        
        UpdateVisuals();
    }
}

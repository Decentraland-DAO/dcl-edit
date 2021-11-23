using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;


public class Highlighter : Interface3DHover
{
    //Settings
    //The UV-Channel to store the smooth normals.
    //Must be the same UV-Channel as in the Shader.
    public const int UVChannel = 3;

    [SerializeField]
    private Highlight _hoverHighlightPrefab = default;

    [SerializeField]
    private Highlight _selectedHighlightPrefab = default;


    private bool _isHovering = false;
    private Highlight[] activeHighlights = Array.Empty<Highlight>();

    private Entity OwnEntity => GetComponent<Entity>();

    void Start()
    {
        SceneManager.OnUpdateSelection.AddListener(SetDirty);
    }

    private bool _isDirty = false;

    public void SetDirty()
    {
        _isDirty = true;
    }

    void LateUpdate()
    {
        if (_isDirty)
        {
            _isDirty = false;
            UpdateHovering();
        }
    }

    public void UpdateHovering()
    {
        if (_isHovering)
        {
            SetHighlight(_hoverHighlightPrefab);
        }
        else if (OwnEntity == SceneManager.SelectedEntity)
        {
            SetHighlight(_selectedHighlightPrefab);
        }
        else
        {
            SetHighlight(null);
        }
    }

    public override void StartHover()
    {
        _isHovering = true;
        UpdateHovering();
    }
    
    public override void EndHover()
    {
        _isHovering = false;
        UpdateHovering();
    }

    private void SetHighlight([CanBeNull] Highlight highlight)
    {
        Debug.Log(highlight);
        
        if (activeHighlights.Length>0)
        {
            foreach (var h in activeHighlights)
            {
                h.DestroyHighlight();
            }
        }

        if (highlight != null)
        {
            var targets = GetComponentsInChildren<Hilightable>();
        
            activeHighlights = new Highlight[targets.Length];
            for (int i = 0; i < targets.Length; i++)
            {
                activeHighlights[i] = highlight.HighlightTarget(targets[i]);
            }
        }
        else
        {
            activeHighlights = Array.Empty<Highlight>();
        }

    }
    
}
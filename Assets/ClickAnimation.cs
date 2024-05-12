using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ClickAnimation : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public AnimationCurve remapCurve;
    public Vector2 alphaRange = new Vector2(.1f, .4f);
    public float lerpDuration = 0.01f;
    
    private bool _isPinching = false;
    private bool _isLerping = false;
    
    private float _lerpValue = 0f;

    private bool _lerpUp = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _isPinching = true;
            Debug.Log($"Down: {_lerpValue}");
        }
        else
        {
            _isPinching = false;
            Debug.Log($"Up: {_lerpValue}");
        }

        if (!_isLerping)
        {
            if (!_isPinching && Mathf.Approximately(_lerpValue, 1f))
            {
                _isLerping = true;
                _lerpUp = false;
            }
            
            if (_isPinching && Mathf.Approximately(_lerpValue, 0f))
            {
                _isLerping = true;
                _lerpUp = true;
            }
        }
        
        if (_isLerping)
        {
            if (LerpTillDone(_lerpUp))
                _isLerping = false;
            
            UpdateClick(_lerpValue);
        }
        
        
        
        
        
    }

    bool LerpTillDone(bool up = true)
    {
        var delta = Time.deltaTime / lerpDuration;
        _lerpValue += delta * (up ? 1 : -1);
        if (_lerpValue is < 0f or > 1f)
        {
            _lerpValue = Mathf.Clamp01(_lerpValue);
            return true;
        } 


        return false;

    }

    void UpdateClick(float strength)
    {
        
        if (!skinnedMeshRenderer.enabled) skinnedMeshRenderer.enabled = true;

        var mappedPinchStrength = remapCurve.Evaluate(strength);

        skinnedMeshRenderer.material.color = _isPinching ? Color.white : new Color(1f, 1f, 1f, Mathf.Lerp(alphaRange.x, alphaRange.y, mappedPinchStrength));
        skinnedMeshRenderer.SetBlendShapeWeight(0, mappedPinchStrength * 100f);
        skinnedMeshRenderer.SetBlendShapeWeight(1, mappedPinchStrength * 100f);
    }

}

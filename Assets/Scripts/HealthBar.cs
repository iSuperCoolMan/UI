using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _valueChangeSpeed;

    private float _valueChangeSpeedMultiplier;
    private float _valueChangeSpeedWithMultiplier;

    private Slider _slider;

    private Coroutine _smoothValueChange;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _valueChangeSpeedMultiplier = 0.001f;
        _valueChangeSpeedWithMultiplier = _valueChangeSpeed * _valueChangeSpeedMultiplier;
        _slider.value = _player.Health / _player.MaxHealth;
    }

    public void SetActualValue()
    {
        if (_smoothValueChange == null)
        {
            _smoothValueChange = StartCoroutine(SmoothValueChange(_player.Health / _player.MaxHealth));
        }
        else
        {
            StopCoroutine(_smoothValueChange);
            _smoothValueChange = StartCoroutine(SmoothValueChange(_player.Health / _player.MaxHealth));
        }
    }

    private IEnumerator SmoothValueChange(float actualValue)
    {
        float maxDelta;

        while (_slider.value != actualValue)
        {
            maxDelta = Mathf.Abs(_slider.value - actualValue) * _valueChangeSpeedWithMultiplier;
            _slider.value = Mathf.MoveTowards(_slider.value, actualValue, maxDelta);

            yield return null;
        }

        _smoothValueChange = null;
    }
}

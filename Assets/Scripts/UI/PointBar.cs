using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointBar : MonoBehaviour
{
    public float m_MaxValue = 100.0f;

    private float m_Value = 100.0f;

    private Slider m_Slider;

    public bool m_UseTransitionTime = true;

    [Range(0.0f, 2.0f)]
    public float m_TransitionTime = 0.5f;

    private void Reset()
    {
        m_Slider.maxValue = m_MaxValue;
        m_Slider.value = m_MaxValue;

        m_Value = m_MaxValue;
    }

    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        Reset();
    }

    public void ReduceValue(float deduction)
    {
        float targetValue = Mathf.Max(0, (m_Value- deduction));

        if (m_UseTransitionTime)
        {
            StartCoroutine(TransitionValue(m_Value, targetValue, m_TransitionTime));
        }
        else
        {
            m_Value = targetValue;
            m_Slider.value = m_Value;
        }
    }

    public void IncreaseValue(float increment)
    {
        float targetValue = Mathf.Min(100, (m_Value + increment));

        if (m_UseTransitionTime)
        {
            StartCoroutine(TransitionValue(m_Value, targetValue, m_TransitionTime));
        }
        else
        {
            m_Value = targetValue;
            m_Slider.value = m_Value;
        }
    }

    private IEnumerator TransitionValue(float currentValue, float targetValue, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            m_Value = Mathf.Lerp(currentValue, targetValue, (elapsed / duration));
            m_Slider.value = m_Value;
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        m_Value = targetValue;
        m_Slider.value = m_Value;
    }
}

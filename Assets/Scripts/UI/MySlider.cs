using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/My Slider")]
    [RequireComponent(typeof(RectTransform))]
    public class MySlider : UIBehaviour,ICanvasElement
    {
        public enum Direction
        {
            LeftToRight,
            RightToLeft,
            BottomToTop,
            TopToBottom,
        }

        [Serializable] public class MySliderEvent : UnityEvent<float> {}
        [SerializeField] private RectTransform m_FillRect;
        [SerializeField] private Direction m_Direction = Direction.LeftToRight;
        [SerializeField] private MySlider m_Slider;

        [SerializeField]
        private float m_MinValue = 0;
        public float minValue 
        { 
            get { return m_MinValue; } 
            set { if (TTSetPropertyUtility.SetStruct(ref m_MinValue, value)) { Set(m_Value); UpdateVisuals(); } } 
        }

        [SerializeField]
        private float m_MaxValue = 1;
        public float maxValue 
        {
            get { return m_MaxValue; } 
            set { if (TTSetPropertyUtility.SetStruct(ref m_MaxValue, value)) { Set(m_Value); UpdateVisuals(); } } 
        }

        [SerializeField]
        protected float m_Value;
        public virtual float value
        {
            get { return m_Value; }
            set { Set(value);}
        }

        public float normalizedValue
        {
            get 
            {
                if (Mathf.Approximately(minValue, maxValue))
                    return 0;
                return Mathf.InverseLerp(minValue, maxValue, this.value);
            }
            set { this.value = Mathf.Lerp(minValue, maxValue, value); }
        }

        private MySliderEvent m_OnValueChanged = new MySliderEvent();
        public MySliderEvent onValueChanged
        {
            get { return m_OnValueChanged; }
            set 
            { 
                m_OnValueChanged = value;
            }
        }

        private Image m_FillImage;
        private Transform m_FillTransform;
        private RectTransform m_FillContainerRect;
        private DrivenRectTransformTracker m_Tracker;
        float stepSize { get { return (maxValue - minValue) * 0.1f; } }
        protected MySlider() {}

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateCachedRefences();
            Set(m_Value, false);
            UpdateVisuals();
        }

        protected override void OnDisable()
        {
            m_Tracker.Clear();
            base.OnDisable();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            m_Value = ClampValue(m_Value);
            float oldNormalizedValue = normalizedValue;
            if (m_FillContainerRect != null)
            {
                if (m_FillImage != null && m_FillImage.type == Image.Type.Filled)
                    oldNormalizedValue = m_FillImage.fillAmount;
                else
                    oldNormalizedValue = (reverseValue ? 1 - m_FillRect.anchorMin[(int)axis] : m_FillRect.anchorMax[(int)axis]);
            }

            UpdateVisuals();

            if (oldNormalizedValue != normalizedValue)
            {
                UISystemProfilerApi.AddMarker("MySlider.value", this);
                onValueChanged.Invoke(m_Value);
            }
        }

        void UpdateCachedRefences()
        {
            if (m_FillRect && m_FillRect != (RectTransform)transform)
            {
                m_FillTransform = m_FillRect.transform;
                m_FillImage = m_FillRect.GetComponent<Image>();
                m_FillImage.gameObject.SetActive(true);
                if (m_FillTransform.parent != null)
                    m_FillContainerRect = m_FillTransform.parent.GetComponent<RectTransform>();
            }
            else
            {
                m_FillRect = null;
                m_FillContainerRect = null;
                m_FillImage = null;
            }
        }

        void Set(float input)
        {
            Set(input, true);
        }

        float ClampValue(float input)
        {
            float newValue = Mathf.Clamp(input, minValue, maxValue);
            return newValue;
        }

        protected virtual void Set(float input, bool sendCallback)
        {
            float newValue = ClampValue(input);
            
            if (m_Value == newValue)
                return;
            
            m_Value = newValue;
            UpdateVisuals();
            if (sendCallback)
            {
                UISystemProfilerApi.AddMarker("MySlider.value", this);
                m_OnValueChanged.Invoke(newValue);
                InVokeSlider();
            }
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();

            if (!IsActive())
                return;

            UpdateVisuals();
        }

        enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        Axis axis { get { return (m_Direction == Direction.LeftToRight || m_Direction == Direction.RightToLeft) ? Axis.Horizontal : Axis.Vertical; } }
        bool reverseValue { get { return m_Direction == Direction.RightToLeft || m_Direction == Direction.TopToBottom; } }
        private void UpdateVisuals()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                UpdateCachedRefences();
#endif
            m_Tracker.Clear();
            if (m_FillContainerRect != null)
            {
                m_Tracker.Add(this, m_FillRect, DrivenTransformProperties.Anchors);
                Vector2 anchorMin = Vector2.zero;
                Vector2 anchorMax = Vector2.one;

                if (reverseValue)
                    anchorMin[(int)axis] = 1 - normalizedValue;
                else
                    anchorMax[(int)axis] = normalizedValue;

                m_FillRect.anchorMin = anchorMin;
                m_FillRect.anchorMax = anchorMax;
            }
        }

        int currentSliderPart = 0;
        void InVokeSlider() 
        {
            int iValue = Mathf.CeilToInt(m_Value * 100);
            int part = iValue / 10;
            if (part < currentSliderPart)
            {
                currentSliderPart = part;
                if (m_Slider)
                {
                    m_Slider.value += 0.1f;
                }  
            }
        }

        public virtual void Rebuild(CanvasUpdate executing)
        {

        }
        public virtual void LayoutComplete()
        {}
        public virtual void GraphicUpdateComplete()
        {}
    }
}


#region Includes
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#endregion

namespace TS.ColorPicker.Demo
{
    public class ColorPickerButton : MonoBehaviour
    {
        #region Variables

        public static UnityAction<bool> OnPickColor;

        [Header("References")]
        [SerializeField] Image _renderer;
        [SerializeField] private ColorPicker _colorPicker;
        [SerializeField] private GameObject _colorPickerPrefab;

        private Color _color;
        private Ray _ray;
        private RaycastHit _hit;

        Button _button;
        #endregion

        private void Start()
        {

            _colorPicker.OnChanged.AddListener(ColorPicker_OnChanged);
            _colorPicker.OnSubmit.AddListener(ColorPicker_OnSubmit);
            _colorPicker.OnCancel.AddListener(ColorPicker_OnCancel);

            _color = _renderer.color;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ColorPickerOpen);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, 100))
                {
                    _colorPicker.Open(_color);
                }
            }
        }

        private void ColorPickerOpen()
        {
            _colorPickerPrefab.SetActive(true);
            OnPickColor?.Invoke(true);
        }

        private void ColorPicker_OnChanged(Color color)
        {
            _renderer.color = color;
        }
        private void ColorPicker_OnSubmit(Color color)
        {
            _color = color;
            _renderer.GetComponent<ColorButtonController>().OnChangedColorPicker(_color);
            OnPickColor?.Invoke(false);
        }
        private void ColorPicker_OnCancel()
        {
            _renderer.color = _color;
            OnPickColor?.Invoke(false);
        }
    }
}
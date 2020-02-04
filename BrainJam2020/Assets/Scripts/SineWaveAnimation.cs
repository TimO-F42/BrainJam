using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SineWaveAnimation : MonoBehaviour
{
	[SerializeField] private Vector2 _animationMotion;
	[SerializeField, Range(0f, 10f)] private float _timingOffset;

	private RectTransform _rectTransform;
	private Vector3 _originalPosition;

	private void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
		_originalPosition = _rectTransform.anchoredPosition;
	}

	private void Update()
	{
		var time = Time.time;
		var x = _animationMotion.x;
		var y = _animationMotion.y;
		var offset = _timingOffset;
		var newPosition = new Vector3(x * Mathf.Sin(time + offset), y * Mathf.Sin(time + offset));

		_rectTransform.anchoredPosition = _originalPosition + newPosition;
	}
}

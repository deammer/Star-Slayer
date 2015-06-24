using UnityEngine;
using System.Collections;

public static class Tween
{
	public static IEnumerator VarTween(this float value, float target, float duration, Easer ease)
	{
		float elapsed = 0;
		float start = value;
		while (elapsed < duration)
		{
			Debug.Log("value = " + value);
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			value = start + (target - start) * ease(elapsed / duration);
			yield return 0;
		}
		value = target;
	}

	public static IEnumerator MoveTo(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localPosition;
		var range = target - start;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localPosition = start + range * ease(elapsed / duration);
			yield return 0;
		}
		transform.localPosition = target;
	}
	
	public static IEnumerator MoveFrom(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		var start = transform.localPosition;
		transform.localPosition = target;
		return MoveTo(transform, start, duration, ease);
	}
	
	public static IEnumerator ScaleTo(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localScale;
		var range = target - start;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localScale = start + range * ease(elapsed / duration);
			yield return 0;
		}
		transform.localScale = target;
	}
	
	public static IEnumerator ScaleFrom(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		var start = transform.localScale;
		transform.localScale = target;
		return ScaleTo(transform, start, duration, ease);
	}
	
	public static IEnumerator RotateTo(this Transform transform, Quaternion target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localRotation;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localRotation = Quaternion.Lerp(start, target, ease(elapsed / duration));
			yield return 0;
		}
		transform.localRotation = target;
	}
	
	public static IEnumerator RotateFrom(this Transform transform, Quaternion target, float duration, Easer ease)
	{
		var start = transform.localRotation;
		transform.localRotation = target;
		return RotateTo(transform, start, duration, ease);
	}
	
	public static IEnumerator CurveTo(this Transform transform, Vector3 control, Vector3 target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localPosition;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localPosition = Calc.Bezier(start, control, target, ease(elapsed / duration));
			yield return 0;
		}
		transform.localPosition = target;
	}
	
	public static IEnumerator Shake(this Transform transform, float amount, float duration)
	{
		var start = transform.localPosition;
		var shake = Vector3.zero;
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			shake.Set(Random.Range(-amount, amount), Random.Range(-amount, amount), Random.Range(-amount, amount));
			transform.localPosition = start + shake;
			yield return 0;
		}
		transform.localPosition = start;
	}
	
	public static IEnumerator ShakeXY(this Transform transform, float amount, float duration)
	{
		var start = transform.localPosition;
		var shake = Vector3.zero;
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			shake.Set(Random.Range(-amount, amount), Random.Range(-amount, amount), 0);
			transform.localPosition = start + shake;
			yield return 0;
		}
		transform.localPosition = start;
	}
	
	public static IEnumerator ShakeXZ(this Transform transform, float amount, float duration)
	{
		var start = transform.localPosition;
		var shake = Vector3.zero;
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			shake.Set(Random.Range(-amount, amount), 0, Random.Range(-amount, amount));
			transform.localPosition = start + shake;
			yield return 0;
		}
		transform.localPosition = start;
	}
	
	public static IEnumerator ShakeYZ(this Transform transform, float amount, float duration)
	{
		var start = transform.localPosition;
		var shake = Vector3.zero;
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			shake.Set(0, Random.Range(-amount, amount), Random.Range(-amount, amount));
			transform.localPosition = start + shake;
			yield return 0;
		}
		transform.localPosition = start;
	}
	
	public static IEnumerator Wait(float time)
	{
		while (time >= 0)
		{
			time -= Time.deltaTime;
			yield return 0;	
		}
	}
}

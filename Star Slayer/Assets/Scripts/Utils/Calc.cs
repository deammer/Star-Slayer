using UnityEngine;
using System.Collections;

public static class Calc
{
	public static float Clamp(float val, float min, float max)
	{
		if (max > min)
			return Mathf.Min(max, Mathf.Max(min, val));
		else
			return Mathf.Min(min, Mathf.Max(max, val));
	}
	public static Vector3 Clamp(Vector3 val, Vector3 min, Vector3 max)
	{
		val.x = Clamp(val.x, min.x, max.x);
		val.y = Clamp(val.y, min.y, max.y);
		val.z = Clamp(val.z, min.z, max.z);
		return val;
	}
	public static float Clamp01(float val, float min, float max)
	{
		return Clamp(val, 0, 1);
	}
	
	public static float Scale(float val, float min, float max, float min2, float max2)
	{
		return min2 + ((val - min) / (max - min)) * (max2 - min2);
	}
	
	public static float Scale01(float val, float min, float max)
	{
		return (val - min) / (max - min);	
	}
	
	public static float ScaleClamp(float val, float min, float max, float min2, float max2)
	{
		return Clamp(Scale(val, min, max, min2, max2), min2, max2);	
	}
	
	public static float ScaleClamp01(float val, float min, float max)
	{
		return Clamp(Scale01(val, min, max), min, max);
	}
	
	public static float Loop(float time, float min, float max, float offsetPercent)
	{
		var range = max - min;
        var total = (Time.time + time * offsetPercent) * (Mathf.Abs(range) / time);
        if (range > 0)
            return min + Time.time - (range * Mathf.FloorToInt((Time.time / range)));
        else
            return min - (Time.time - (Mathf.Abs(range) * Mathf.FloorToInt((total / Mathf.Abs(range)))));
	}
	public static float Loop(float time, float min, float max)
	{
		return Loop(time, min, max);	
	}
	public static Vector3 Loop(float time, Vector3 min, Vector3 max, float offsetPercent)
	{
		min.x = Loop(time, min.x, max.x, offsetPercent);
		min.y = Loop(time, min.y, max.y, offsetPercent);
		min.z = Loop(time, min.z, max.z, offsetPercent);
		return min;
	}
	public static Vector3 Loop(float time, Vector3 min, Vector3 max)
	{
		return Loop(time, min, max, 0);
	}
	
	public static float Wave(float time, float min, float max, float offsetPercent)
	{
		var range = (max - min) / 2;
		return min + range + Mathf.Sin(((Time.time + time * offsetPercent) / time) * (Mathf.PI * 2)) * range;
	}
	public static float Wave(float time, float min, float max)
	{
		return Wave(time, min, max, 0);
	}
	public static Vector3 Wave(float time, Vector3 min, Vector3 max, float offsetPercent)
	{
		min.x = Wave(time, min.x, max.x, offsetPercent);
		min.y = Wave(time, min.y, max.y, offsetPercent);
		min.z = Wave(time, min.z, max.z, offsetPercent);
		return min;
	}
	public static Vector3 Wave(float time, Vector3 min, Vector3 max)
	{
		return Wave(time, min, max, 0);
	}
	
	public static float Lerp(float from, float to, float t)
	{
		return from + (to - from) * t;
	}
	public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
	{
		return from + (to - from) * t;
	}
	public static Quaternion Lerp(Quaternion from, Quaternion to, float t)
	{
		from.x = from.x + (to.x - from.x) * t;
		from.y = from.y + (to.y - from.y) * t;
		from.z = from.z + (to.z - from.z) * t;
		from.w = from.w + (to.w - from.w) * t;
		return from;
	}
	
	public static float Bezier(float from, float control, float to, float t)
    {
        return from * (1 - t) * (1 - t) + control * 2 * (1 - t) * t + to * t * t;
    }
    public static Vector3 Bezier(Vector3 from, Vector3 control, Vector3 to, float t)
    {
        from.x = Bezier(from.x, control.x, to.x, t);
        from.y = Bezier(from.y, control.y, to.y, t);
		from.z = Bezier(from.z, control.z, to.z, t);
        return from;
    }
}

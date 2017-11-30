using System;
using UnityEngine;

namespace CCore
{
	/// <summary>
	/// CCore's Monobehaviour extension
	/// </summary>
	public class Monobehaviour : UnityEngine.MonoBehaviour
	{
		[SerializeField] private bool loggingEnabled = true;

		[SerializeField] private Color loggingColor = UnityEngine.Color.gray;

		private void DoLog(
            Action<string> logCall,
            string str,
            params object[] args)
        {
            // TODO: Check if "!Debug.isDebugBuild" is needed
			if (!loggingEnabled || !UnityEngine.Debug.isDebugBuild)
			{
				return;
			}

			string colorHex = ToRGBHex(loggingColor);
			
			logCall(string.Format(
					"<color="+ colorHex +"FF><b>[{0}][{1}] {2} </b></color>",
					DateTime.Now,
					this.GetType().Name.ToUpper(),
					string.Format(str, args))
			);
        }

        public void Log(string str, params object[] args)
        {
            DoLog(UnityEngine.Debug.Log, str, args);
        }

        public void LogWarning(string str, params object[] args)
        {
            DoLog(UnityEngine.Debug.LogWarning, str, args);
        }

        public void LogError(string str, params object[] args)
        {
            DoLog(UnityEngine.Debug.LogError, str, args);
        }

		private string ToRGBHex(Color color)
		{
			return string.Format(
				"#{0:X2}{1:X2}{2:X2}",
				ToByte(color.r),
				ToByte(color.g),
				ToByte(color.b)
			);
		}
	
		private byte ToByte(float value)
		{
			value = Mathf.Clamp01(value);

			return (byte)(value * 255);
		}
	}
}

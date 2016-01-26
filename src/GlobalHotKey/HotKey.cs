using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Interop;
using System.Diagnostics.Contracts;

namespace HotKey
{
    internal class HotKeyWinApi
	{
		public const int WmHotKey = 0x0312;

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, MOD_KEY fsModifiers, Keys vk);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
	}

    /// <summary>
    /// グローバルホットキーを登録機能を提供します。
    /// </summary>
	public sealed class HotKeyRegister
    {
        //ホットキーが押された時には発生するイベントです。
		public event Action<HotKeyRegister> HotKeyPressed;

		private readonly int id;
		private bool isKeyRegistered;
		private readonly IntPtr handle;

        /// <summary>
        /// ホットキーに使用されるキー
        /// </summary>
		public Keys Key { get; private set; }


        /// <summary>
        /// ホットキーに使用される修飾キー
        /// </summary>
		public MOD_KEY KeyModifier { get; private set; }

        /// <summary>
        /// 新規のホットキーを登録します。
        /// </summary>
        /// <param name="MOD_KEY">修飾キー</param>
        /// <param name="Key">キー</param>
        /// <param name="Window">親ウィンドウ</param>
        public HotKeyRegister(MOD_KEY MOD_KEY, System.Windows.Forms.Keys Key, System.Windows.Window Window)
		{
            var windowHandle = new WindowInteropHelper(Window).Handle;
            Contract.Requires(MOD_KEY != MOD_KEY.None || Key != Keys.None);
            Contract.Requires(windowHandle != IntPtr.Zero);

            this.Key = Key;
            KeyModifier = MOD_KEY;
            var r = new Random();
            id = r.Next();
            handle = windowHandle;
            RegisterHotKey();

            ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
		}

		private void RegisterHotKey()
		{
			if (Key == Keys.None)
				return;
			if (isKeyRegistered)
				UnregisterHotKey();
			isKeyRegistered = HotKeyWinApi.RegisterHotKey(handle, id, KeyModifier, Key);
			if (!isKeyRegistered)
				throw new ApplicationException("Hotkey already in use");
		}

        ~HotKeyRegister()
        {
            UnregisterHotKey();
        }

	    private void UnregisterHotKey()
		{
			isKeyRegistered = !HotKeyWinApi.UnregisterHotKey(handle, id);
		}


		private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
		{
			if (!handled)
			{
				if (msg.message == HotKeyWinApi.WmHotKey　&& (int)(msg.wParam) == id)
				{
					OnHotKeyPressed();
					handled = true;
				}
			}
		}

		private void OnHotKeyPressed()
		{
			if (HotKeyPressed != null)
				HotKeyPressed(this);
		}
	}
	public enum MOD_KEY : int
	{
		None = 0x0000,
		ALT = 0x0001,
		CONTROL = 0x0002,
		SHIFT = 0x0004,
	}
}
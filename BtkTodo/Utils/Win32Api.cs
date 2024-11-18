using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BtkTodo.Utils
{
    public class Win32Api
    {
        #region APPBAR

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        public enum ABMsg : int
        {
            ABM_NEW = 0,
            ABM_REMOVE = 1,
            ABM_QUERYPOS = 2,
            ABM_SETPOS = 3,
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
            ABM_ACTIVATE = 6,
            ABM_GETAUTOHIDEBAR = 7,
            ABM_SETAUTOHIDEBAR = 8,
            ABM_WINDOWPOSCHANGED = 9,
            ABM_SETSTATE = 10
        }

        public enum ABNotify : int
        {
            ABN_STATECHANGE = 0,
            ABN_POSCHANGED,
            ABN_FULLSCREENAPP,
            ABN_WINDOWARRANGE
        }

        public enum ABEdge : int
        {
            ABE_LEFT = 0,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }

        private bool fBarRegistered = false;

        [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
        static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);
        [DllImport("USER32")]
        static extern int GetSystemMetrics(int Index);
        [DllImport("User32.dll", ExactSpelling = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool MoveWindow
            (IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int RegisterWindowMessage(string msg);
       
        public static void SetAutoHide(IntPtr handle, ABEdge edge, bool autoHide)
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            /*
            abd.hWnd = handle;
            uint curAutoHide = SHAppBarMessage((int)ABMsg.ABM_GETAUTOHIDEBAR, ref abd);
            if (curAutoHide.Equals(IntPtr.Zero)) // if there is no current appbar with autohide set for that edge...
            {
                abd.lParam = new IntPtr(1); // true
                SHAppBarMessage((int)ABMsg.ABM_SETAUTOHIDEBAR, ref abd);
            }
            */
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = handle;
            abd.uEdge = (int)edge;
            abd.lParam = autoHide ? new IntPtr(1) : new IntPtr(1); // true or false
            SHAppBarMessage((int)ABMsg.ABM_SETAUTOHIDEBAR, ref abd);
           
        }

        public static int RegisterAppBar(IntPtr handle)
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = handle;
            
            int uCallBack = 0;


            uCallBack = RegisterWindowMessage("AppBarMessage");
            abd.uCallbackMessage = uCallBack;

            uint ret = SHAppBarMessage((int)ABMsg.ABM_NEW, ref abd);

            return uCallBack;
        }

        public static void UnregisterAppBar(IntPtr handle)
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = handle;

            SHAppBarMessage((int)ABMsg.ABM_REMOVE, ref abd);
        }


        public static void ABSetPos(IntPtr handle, ABEdge edge, int size)
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = handle;
            abd.uEdge = (int)edge;//ABEdge.ABE_TOP;

            if (abd.uEdge == (int)ABEdge.ABE_LEFT || abd.uEdge == (int)ABEdge.ABE_RIGHT)
            {
                abd.rc.top = 0;
                abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
                if (abd.uEdge == (int)ABEdge.ABE_LEFT)
                {
                    abd.rc.left = 0;
                    abd.rc.right = size;
                }
                else
                {
                    abd.rc.right = SystemInformation.PrimaryMonitorSize.Width;
                    abd.rc.left = abd.rc.right - size;
                }

            }
            else
            {
                abd.rc.left = 0;
                abd.rc.right = SystemInformation.PrimaryMonitorSize.Width;
                if (abd.uEdge == (int)ABEdge.ABE_TOP)
                {
                    abd.rc.top = 0;
                    abd.rc.bottom = size;
                }
                else
                {
                    abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
                    abd.rc.top = abd.rc.bottom - size;
                }
            }

            // Query the system for an approved size and position. 
            SHAppBarMessage((int)ABMsg.ABM_QUERYPOS, ref abd);

            // Adjust the rectangle, depending on the edge to which the 
            // appbar is anchored. 
            switch (abd.uEdge)
            {
                case (int)ABEdge.ABE_LEFT:
                    abd.rc.right = abd.rc.left + size;
                    break;
                case (int)ABEdge.ABE_RIGHT:
                    abd.rc.left = abd.rc.right - size;
                    break;
                case (int)ABEdge.ABE_TOP:
                    abd.rc.bottom = abd.rc.top + size;
                    break;
                case (int)ABEdge.ABE_BOTTOM:
                    abd.rc.top = abd.rc.bottom - size;
                    break;
            }

            // Pass the final bounding rectangle to the system. 
            SHAppBarMessage((int)ABMsg.ABM_SETPOS, ref abd);

            // Move and size the appbar so that it conforms to the 
            // bounding rectangle passed to the system. 
            MoveWindow(abd.hWnd, abd.rc.left, abd.rc.top,
                abd.rc.right - abd.rc.left, abd.rc.bottom - abd.rc.top, true);
        }


        /*
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= (~0x00C00000); // WS_CAPTION
                cp.Style &= (~0x00800000); // WS_BORDER
                cp.ExStyle = 0x00000080 | 0x00000008; // WS_EX_TOOLWINDOW | WS_EX_TOPMOST
                return cp;
            }
        }
        */
        #endregion
    }
}

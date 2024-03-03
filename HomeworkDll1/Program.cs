using System.Runtime.InteropServices;

namespace HomeworkDll1
{
    internal class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, MessageBoxOptions options);

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

        const int WM_CLOSE = 0x0010;
        const int WM_SETTEXT = 0x000C;
        const int WM_SIZE = 0x0005;

        static void Main(string[] args)
        {
            int answer;
            Console.WriteLine("1 - Information about me, \n2 - Custom Window Editing, \n3 - Beep Test, \n4 - ");
            int.TryParse(Console.ReadLine(), out answer);

            Console.Clear();

            switch (answer)
            {
                case 1: InfoMessageBox(); break;

                case 2: CustomWindowEditing(); break;

                case 3: BeepTest(); break;

                case 4:  break;

                default: break;
            }
        }

        public static void CustomWindowEditing()
        {
            Console.Write("Enter window name: ");
            string windowName = Console.ReadLine();
            IntPtr handle = FindWindow(windowName, null);
            if (handle == IntPtr.Zero)
            {
                MessageBox(IntPtr.Zero, $"{windowName} was NOT found!", "Information", MessageBoxOptions.OK | MessageBoxOptions.IconInformation);
            }
            else
            {
                int answer;
                Console.WriteLine("1 - Change title, \n2 - Close window, \n3 - Change window size");
                int.TryParse(Console.ReadLine(), out answer);
                if(answer == 1)
                {
                    Console.Write("Enter window name: ");
                    string titleName = Console.ReadLine();
                    SendMessage(handle, WM_SETTEXT, IntPtr.Zero, titleName);
                }
                else if(answer == 2)
                {
                    var close_option = MessageBox(IntPtr.Zero, $"Are you sure you want to close \"{windowName}\"", "Closing", MessageBoxOptions.YesNo | MessageBoxOptions.IconQuestion);
                    if (close_option == 6)
                    {
                        SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    }
                    else if (close_option == 7)
                    {
                        return;
                    }
                }
                else if (answer == 3)
                {
                    Console.Write("Enter width: ");
                    int width = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Enter height: ");
                    int height = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    SendMessage(handle, WM_SIZE, IntPtr.Zero, (IntPtr)((height << 16) | width));
                }
            }
        }

        public static void InfoMessageBox()
        {
            MessageBox(IntPtr.Zero, "My name is Timur", "Information", MessageBoxOptions.OK | MessageBoxOptions.IconInformation);
            MessageBox(IntPtr.Zero, "I am 16 years old", "Information", MessageBoxOptions.OK | MessageBoxOptions.IconInformation);
            MessageBox(IntPtr.Zero, "I am studying in Computer Science degree", "Information", MessageBoxOptions.OK | MessageBoxOptions.IconInformation);
        }

        public static void BeepTest()
        {
            Beep(1000, 500);
            Thread.Sleep(1000);
            MessageBeep(0);
            Thread.Sleep(1000);
        }



        public enum MessageBoxOptions : uint
        {
            OK = 0x00000000,
            OKCancel = 0x00000001,
            AbortRetryIgnore = 0x00000002,
            YesNoCancel = 0x00000003,
            YesNo = 0x00000004,
            RetryCancel = 0x00000005,
            CancelTryContinue = 0x00000006,
            IconError = 0x00000010,
            IconQuestion = 0x00000020,
            IconWarning = 0x00000030,
            IconInformation = 0x00000040,
            UserIcon = 0x00000080
        }
    }
}

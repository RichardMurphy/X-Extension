using Microsoft.SmallBasic.Library;
using Microsoft.SmallBasic.Library.Internal;
using System;
using System.Reflection;
using System.Windows.Forms;
//using Window = System.Windows.Forms.VisualStyles.VisualStyleElement;  //was mixing up wpf and forms -Password
using Window = System.Windows.Window;

//litdev solved Password for me. https://social.msdn.microsoft.com/Forums/en-US/73082625-9b17-4923-a280-2caf0709c1d4/lddialoguesconfirm?forum=smallbasic
namespace Jibba
{
    /// <summary>
    /// Provides dialogue boxes
    /// </summary>
    [SmallBasicType]
    public static class XDialogue
    {
        /// <summary>
        /// A dialogue box to type in passwords. Characters are masked with *.
        /// </summary>
        /// <param name="title">a title for the dialogue box</param>
        /// <param name="text">text label for the dialogue box</param>
        /// <returns>the password</returns>
        public static Primitive Password(Primitive title, Primitive text)
        {
            DialoguePassword dialogue = new DialoguePassword();

            Type GraphicsWindowType = typeof(GraphicsWindow);
            Window _window = (Window)GraphicsWindowType.GetField("_window", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            InvokeHelperWithReturn ret = new InvokeHelperWithReturn(delegate
            {
                return dialogue.Show(title, text, _window);
            });
            MethodInfo method = GraphicsWindowType.GetMethod("InvokeWithReturn", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.IgnoreCase);
            return method.Invoke(null, new object[] { ret }).ToString();
        }

        /// <summary>
        /// A yes no dialogue box
        /// </summary>
        /// <param name="title">Dialogue title</param>
        /// <param name="text">Dialogue text</param>
        /// <returns>the button clicked value, Yes or No</returns>
        public static Primitive YesNo(Primitive title, Primitive text)
        {
            DialogueYesNo dialogue = new DialogueYesNo();

            Type GraphicsWindowType = typeof(GraphicsWindow);
            Window _window = (Window)GraphicsWindowType.GetField("_window", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            InvokeHelperWithReturn ret = new InvokeHelperWithReturn(delegate
            {
                return dialogue.Show(title, text, _window);
            });
            MethodInfo method = GraphicsWindowType.GetMethod("InvokeWithReturn", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.IgnoreCase);
            return method.Invoke(null, new object[] { ret }).ToString();
        }
    }

    class DialogueYesNo
    {
        public string Show(string title, string text, Window _window)
        {
            Form myDialogue = new Form();

            myDialogue.Width = 300;
            myDialogue.Height = 150;
            myDialogue.Text = title;
            myDialogue.MaximizeBox = false;
            myDialogue.MinimizeBox = false;
            myDialogue.StartPosition = FormStartPosition.CenterParent;
            myDialogue.Icon = Properties.Resources.question_mark;
            myDialogue.FormBorderStyle = FormBorderStyle.FixedDialog;

            string val = "";

            Label txtLabel = new Label() { Left = 20, Top = 20, Width = 260, Height = 55, Text = text };

            Button yesBtn = new Button() { Text = "Yes", Left = 300 - ((10 + 70) * 2), Width = 70, Top = 80 };
            Button noBtn = new Button() { Text = "No", Left = 300 - 10 - 70, Width = 70, Top = 80 };

            yesBtn.Click += (sender, e) => { myDialogue.Close(); val = yesBtn.Text; };
            noBtn.Click += (sender, e) => { myDialogue.Close(); val = noBtn.Text; };

            myDialogue.Controls.Add(yesBtn);
            myDialogue.Controls.Add(noBtn);
            myDialogue.Controls.Add(txtLabel);

            if (null != _window) myDialogue.ShowDialog(new Wpf32Window(_window));
            else myDialogue.ShowDialog();

            return val;
        }
    }


    class DialoguePassword
    {
        public string Show(string title, string text, Window _window)
        {
            Form myDialogue = new Form();

            myDialogue.Width = 300;
            myDialogue.Height = 150;
            myDialogue.Text = title;
            myDialogue.MaximizeBox = false;
            myDialogue.MinimizeBox = false;
            myDialogue.StartPosition = FormStartPosition.CenterParent;
            myDialogue.Icon = Properties.Resources.feather_icon;
            myDialogue.FormBorderStyle = FormBorderStyle.FixedDialog;


            string val = "";

            Label txtLabel = new Label() { Left = 20, Top = 15, Width = 240, Height = 32, Text = text };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            inputBox.PasswordChar = '*';

            Button confirmation = new Button() { Text = "OK", Left = 190, Width = 70, Top = 80 };

            confirmation.Click += (sender, e) => { myDialogue.Close(); val = inputBox.Text; };

            myDialogue.Controls.Add(confirmation);
            myDialogue.Controls.Add(txtLabel);
            myDialogue.Controls.Add(inputBox);

            if (null != _window) myDialogue.ShowDialog(new Wpf32Window(_window));
            else myDialogue.ShowDialog();

            return val;
        }
    }

    class Wpf32Window : IWin32Window
    {
        public IntPtr Handle { get; private set; }

        public Wpf32Window(Window wpfWindow)
        {
            Handle = new System.Windows.Interop.WindowInteropHelper(wpfWindow).Handle;
        }
    }
}

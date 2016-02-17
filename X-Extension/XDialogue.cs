using Microsoft.SmallBasic.Library;
using Microsoft.SmallBasic.Library.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;

using System.Windows.Forms;
using Window = System.Windows.Forms.VisualStyles.VisualStyleElement;


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
            Dialogue dialogue = new Dialogue();

            Type GraphicsWindowType = typeof(GraphicsWindow);
            Window _window = (Window)GraphicsWindowType.GetField("_window", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            InvokeHelperWithReturn ret = new InvokeHelperWithReturn(delegate
            {
                return dialogue.Show(title, text, _window);
            });
            MethodInfo method = GraphicsWindowType.GetMethod("InvokeWithReturn", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.IgnoreCase);
            return method.Invoke(null, new object[] { ret }).ToString();



            // return dialogue.Show(window, title, text);

        }
    }

    class Dialogue
    {
        public string Show(string title, string text, Window _window)
        {     
               
            //Form myDialogue = new Form();

            System.Windows.Forms.Form myDialogue = new Form();

            myDialogue.Width = 300;
            myDialogue.Height = 150;
            myDialogue.Text = title;
            myDialogue.MaximizeBox = false;
            myDialogue.MinimizeBox = false;
            myDialogue.StartPosition = FormStartPosition.CenterParent;
            myDialogue.Icon = Properties.Resources.feather_icon;
            myDialogue.FormBorderStyle = FormBorderStyle.FixedDialog;


            string val = "";

            Label txtLabel = new Label() { Left = 20, Top = 20, Text = text };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            inputBox.PasswordChar = '*';

            Button confirmation = new Button() { Text = "OK", Left = 190, Width = 70, Top = 80 };

            confirmation.Click += (sender, e) => { myDialogue.Close(); val = inputBox.Text; };

            myDialogue.Controls.Add(confirmation);
            myDialogue.Controls.Add(txtLabel);
            myDialogue.Controls.Add(inputBox);

            if (null != _window) myDialogue.ShowDialog(new Wpf32Window(_window));
            else myDialogue.ShowDialog();

            //myDialogue.ShowDialog();

            return val;
        }
    }

    public class Wpf32Window : System.Windows.Forms.IWin32Window
    {
        public IntPtr Handle { get; private set; }

        public Wpf32Window(Window wpfWindow)
        {
            Handle = new System.Windows.Interop.WindowInteropHelper(wpfWindow).Handle;
        }
    }
}

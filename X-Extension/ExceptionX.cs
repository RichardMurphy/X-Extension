using System;
using Microsoft.SmallBasic.Library;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Jibba
{
    class ExceptionX 
    {
        public void Handler([CallerMemberName] string memberName = "")
        {            
            string message = "Exception has occurred with:" + Environment.NewLine + memberName; 
            GraphicsWindow.Hide();
            System.Media.SystemSounds.Exclamation.Play();
            GraphicsWindow.ShowMessage(message, "Exception");
            Program.End();
        }
    }
}

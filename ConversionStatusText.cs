using System;
using System.Collections.Generic;

namespace VideoMinify
{
    public class ConversionStatusText
    {
        private int yPos;
        private string loadingChars = "/-\\|";
        private int counter = 0;
        private string textToDisplay;
        
        public ConversionStatusText(int yPos, string textToDisplay)
        {
            this.yPos = yPos;
            this.textToDisplay = textToDisplay;
        }

        public void Update(bool isDone)
        {
            if (isDone)
            {
                Console.SetCursorPosition(0,yPos);
                Console.WriteLine("Working on {0} ... {1}",textToDisplay, "Done!");
            }
            else
            {
                Console.SetCursorPosition(0,yPos);
                Console.WriteLine("Working on {0} ... {1}",textToDisplay, GetChar(counter%4));
                counter += 1;
            }
        }

        private char GetChar(int count)
        {
            return loadingChars[count];
        }
    }
}
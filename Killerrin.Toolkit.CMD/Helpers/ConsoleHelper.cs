using Killerrin.Toolkit.CMD.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Helpers
{
    public class ConsoleHelper
    {
        /// <summary>
        /// A multiple choice interactive menu within the Console using your Keyboard
        /// </summary>
        /// <param name="multipleChoiceConsole">The menu configuration </param>
        /// <param name="options">The options to display</param>
        /// <returns>The selected option</returns>
        public static int MultipleChoice(MultipleChoiceConsoleConfiguration multipleChoiceConsole, params string[] options)
        {
            int currentSelection = 0;

            // Hide the Cursor
            Console.CursorVisible = false;

            // Loop through until either the menu is cancelled, or selected
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(multipleChoiceConsole.StartX + (i % multipleChoiceConsole.OptionsPerLine) * multipleChoiceConsole.SpacingPerLine, multipleChoiceConsole.StartY + i / multipleChoiceConsole.OptionsPerLine);

                    // If the option is the currently selected option, change the color and apply the Prefix and Postfix
                    if (i == currentSelection) Console.ForegroundColor = multipleChoiceConsole.SelectedColor;
                    Console.Write($"{multipleChoiceConsole.SelectedPrefix}{options[i]}{multipleChoiceConsole.SelectedPostfix}");

                    // Reset the colors after every iteration to ensure that the Deselected are always the proper colors
                    Console.ResetColor();
                }

                // Read in the Users Input
                ConsoleKey key = Console.ReadKey(true).Key;

                // If the input is navigation...
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % multipleChoiceConsole.OptionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % multipleChoiceConsole.OptionsPerLine < multipleChoiceConsole.OptionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= multipleChoiceConsole.OptionsPerLine)
                                currentSelection -= multipleChoiceConsole.OptionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + multipleChoiceConsole.OptionsPerLine < options.Length)
                                currentSelection += multipleChoiceConsole.OptionsPerLine;
                            break;
                        }
                }

                // If the Input is Escape
                foreach (var escapeKey in multipleChoiceConsole.ExitKeys)
                {
                    if (key == escapeKey)
                    {
                        loop = false;
                        currentSelection = -1;
                        break;
                    }
                }
                if (!loop) break;

                // If the Input is Select
                foreach (var selectKey in multipleChoiceConsole.SelectKeys)
                {
                    if (key == selectKey)
                    {
                        loop = false;
                        break;
                    }
                }
                if (!loop) break;
            }

            // Make the Cursor visible and return
            Console.CursorVisible = true;
            return currentSelection;
        }
    }
}

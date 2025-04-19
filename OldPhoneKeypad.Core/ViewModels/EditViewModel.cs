using OldPhoneKeypad.Core.Enums.KeyPadNumber;
using OldPhoneKeypad.Core.Interfaces.ViewModels;
using OldPhoneKeypad.Core.ViewModels;
using System.Text;

namespace OldPhoneKeypad.Core.ViewModels
{
    public class EditViewModel : BaseViewModel, IEditViewModel
    {
        public string DigitString { get; set; } = default!;
        public string OutputText { get; set; } = default!;
        private ButtonCharecters DigitCharecters { get; set; } = default!;
        public EditViewModel()
        {
            InitialKeyPadConstant();
        }
        private void InitialKeyPadConstant() 
        {
            DigitCharecters = new ButtonCharecters();
        }

        public Task HandleProcessDigitInput(string inputText)
        {
            string rawInput = inputText.Replace("#", string.Empty);
            var result = new List<string>();
            var buffer = new StringBuilder();

            for (int i = 0; i < rawInput.Length; i++)
            {
                var current = rawInput[i];

                if (current == '*')
                {
                    if (buffer.Length > 0)
                    {
                        buffer.Length--;
                    }
                    else if (result.Count > 0)
                    {
                        result.RemoveAt(result.Count - 1);
                    }
                }
                else if (current == ' ')
                {
                    if (buffer.Length > 0)
                    {
                        var letter = ResolveBuffer(buffer.ToString());
                        if (letter == null) return Task.CompletedTask;
                        result.Add(letter);
                        buffer.Clear();
                    }
                }
                else if (char.IsDigit(current))
                {
                    // If buffer is empty or repeating the same digit
                    if (buffer.Length == 0 || buffer[^1] == current)
                    {
                        buffer.Append(current);

                        // If buffer exceeded 4 — invalid
                        if (buffer.Length > 4)
                        {
                            OutputText = "The digit length should be 1–4.";
                            return Task.CompletedTask;
                        }
                    }
                    else
                    {
                        // Resolve the buffer
                        var letter = ResolveBuffer(buffer.ToString());
                        if (letter == null) return Task.CompletedTask;

                        result.Add(letter);
                        buffer.Clear();
                        buffer.Append(current);
                    }
                }
                else
                {
                    OutputText = $"Invalid character '{current}' in input.";
                    return Task.CompletedTask;
                }
            }

            // Resolve any remaining buffer
            if (buffer.Length > 0)
            {
                var letter = ResolveBuffer(buffer.ToString());
                if (letter != null)
                    result.Add(letter);
            }

            OutputText = string.Join("", result);
            return Task.CompletedTask;
        }

        private string? ResolveBuffer(string buffer)
        {
            var digit = buffer[0].ToString();
            int pressCount = buffer.Length;

            if (!DigitCharecters.ContainsKey(digit))
                return null;

            var characters = DigitCharecters.GetCharacters(digit);

            return characters[Math.Min(pressCount, characters.Length) - 1];
        }
    }
}

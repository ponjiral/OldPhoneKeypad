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
                    if (buffer.Length == 0 || buffer[^1] == current)
                    {
                        buffer.Append(current);

                        var digit = buffer[0].ToString();
                        int maxAllowed = DigitCharecters.MaxPressCount(digit);
                        if (buffer.Length > maxAllowed)
                        {
                            OutputText = "The digit length should be 1–4.";
                            return Task.CompletedTask;
                        }
                    }
                    else
                    {
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

            OutputText = string.Join("", result).ToUpper();
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

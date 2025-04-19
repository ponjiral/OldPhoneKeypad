using Ardalis.SmartEnum;

namespace OldPhoneKeypad.Core.Enums.KeyPadNumber
{
    public class ButtonCharecters
    {
        private readonly Dictionary<string, string[]> _buttonMappings = new()
        {       
            { "1", new[] { "&", "'", "(" } },
            { "2", new[] { "a", "b", "c" } },
            { "3", new[] { "d", "e", "f" } },
            { "4", new[] { "g", "h", "i" } },
            { "5", new[] { "j", "k", "l" } },
            { "6", new[] { "m", "n", "o" } },
            { "7", new[] { "p", "q", "r", "s" } },
            { "8", new[] { "t", "u", "v" } },
            { "9", new[] { "w", "x", "y", "z" } }
        };

        public bool ContainsKey(string key) => _buttonMappings.ContainsKey(key);

        public string[] GetCharacters(string key)
        {
            if (_buttonMappings.TryGetValue(key, out var chars))
                return chars;
            return Array.Empty<string>();
        }
    }
}
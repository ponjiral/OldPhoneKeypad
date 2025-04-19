using OldPhoneKeypad.Core.Enums.KeyPadNumber;
using OldPhoneKeypad.Core.Interfaces.ViewModels;
using OldPhoneKeypad.Core.ViewModels;

namespace OldPhoneKeypad.Core.ViewModels
{
    public class EditViewModel : BaseViewModel, IEditViewModel
    {
        public string DigitString { get; set; } = default!;
        public EditViewModel()
        {
        }
        public async Task LoadSellerCommissions()
        {

        }

        private string? ResolveBuffer(string buffer)
        {
            var digit = buffer[0].ToString();
            int pressCount = buffer.Length;

            if (!DigitCharecters.ContainsKey(digit))
                return null;

            var characters = DigitCharecters.GetCharacters(digit);
            if (pressCount > characters.Length)
                pressCount = characters.Length; // Clamp to max

            return characters[pressCount - 1];
        }
    }
}

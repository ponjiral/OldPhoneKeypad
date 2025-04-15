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
        public async Task LoadSellerSummary()
        {

        }
    }
}

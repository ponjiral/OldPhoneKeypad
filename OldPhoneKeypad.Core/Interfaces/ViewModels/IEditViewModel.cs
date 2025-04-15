namespace OldPhoneKeypad.Core.Interfaces.ViewModels
{
    public interface IEditViewModel : IViewModel
    {
        string DigitString { get; set; }

        Task LoadSellerCommissions();
        Task LoadSellerSummary();
    }
}

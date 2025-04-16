namespace OldPhoneKeypad.Core.Interfaces.ViewModels
{
    public interface IEditViewModel : IViewModel
    {
        string DigitString { get; set; }
        string OutputText { get; set; }
        Task HandleProcessDigitInput(string inputText);
    }
}

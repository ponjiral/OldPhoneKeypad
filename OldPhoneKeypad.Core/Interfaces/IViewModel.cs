namespace OldPhoneKeypad.Core.Interfaces
{
    public interface IViewModel
    {
        void OnInitialized();
        Task OnInitializedAsync();
        void OnParametersSet();
        Task OnParametersSetAsync();
        void OnAfterRender(bool firstRender);
        Task OnAfterRenderAsync(bool firstRender);

    }
}

namespace OldPhoneKeypad.Core.ViewModels
{
    public abstract class BaseViewModel
    {
        public virtual void OnInitialized()
        {

        }

        public virtual Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void OnParametersSet()
        {

        }

        public virtual Task OnParametersSetAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void OnAfterRender(bool firstRender)
        {

        }

        public virtual Task OnAfterRenderAsync(bool firstRender)
        {
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Components;
using OldPhoneKeypad.Core.Interfaces;
using OldPhoneKeypad.Core.Models;

namespace OldPhoneKeypad.Web.Pages
{
    public abstract class ViewBase<IVM> : ComponentBase where IVM : IViewModel
    {
        [Inject]
        public IVM ViewModel { get; set; } = default!;

        public bool IsPageLoading { get; set; }
        public bool IsLoading { get; set; }
        public virtual bool IsNeedPermissionAccess => false;

        public enum LoadingType
        {
            FULLPAGE,
            PARTIAL,
            NONE
        }

        protected override void OnInitialized()
        {
            ViewModel?.OnInitialized();
        }

        protected async Task RunTaskAsync(Func<Task> func, LoadingType loadingType = LoadingType.PARTIAL, Func<Exception, Task>? onAfterExceptionCallback = null, Func<Exception, Task>? onBeforeExceptionCallback = null)
        {
            try
            {
                await InvokeAsync(StateHasChanged);
                SetLoading(loadingType, true);

                await func();
            }
            catch (Exception ex)
            {
                SetLoading(loadingType, false);
                if (onBeforeExceptionCallback is not null)
                    await onBeforeExceptionCallback.Invoke(ex);
                if (onAfterExceptionCallback is not null)
                    await onAfterExceptionCallback.Invoke(ex);
            }
            finally
            {
                SetLoading(loadingType, false);
            }
        }

        private void SetLoading(LoadingType type, bool isLoading)
        {
            switch (type)
            {
                case LoadingType.FULLPAGE:
                    IsPageLoading = isLoading;
                    break;

                case LoadingType.PARTIAL:
                    IsLoading = isLoading;
                    break;

                default:
                    return;
            }
        }

    }
}
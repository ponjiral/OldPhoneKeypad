using Microsoft.AspNetCore.Components;
using OldPhoneKeypad.Core.Interfaces;
using OldPhoneKeypad.Core.Models;

namespace OldPhoneKeypad.Web.Pages
{
    public abstract class ViewBase<IVM> : ComponentBase where IVM : IViewModel
    {
        [Inject]
        public IVM ViewModel { get; set; } = default!;

        public AlertOption AlertOption { get; set; } = new();
        public bool IsPageLoading { get; set; }
        public bool IsLoading { get; set; }
        public virtual bool IsNeedPermissionAccess => false;

        public enum LoadingType
        {
            FULLPAGE,
            PARTIAL,
            MODAL,
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
                AlertOption.Clear();

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

                //case LoadingType.MODAL:
                //    if (isLoading)
                //        LoadingModal = ModalHelper.ShowLoadingMessageAsync("&lt;strong&gt;System is processing&lt;/strong&gt;&lt;br/&gt;Please wait");
                //    else
                //        LoadingModal.Close();
                //    break;

                default:
                    return;
            }
        }

    }
}
using Abp.Web.Mvc.Views;

namespace NewWeb.Web.Views
{
    public abstract class NewWebWebViewPageBase : NewWebWebViewPageBase<dynamic>
    {

    }

    public abstract class NewWebWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected NewWebWebViewPageBase()
        {
            LocalizationSourceName = NewWebConsts.LocalizationSourceName;
        }
    }
}
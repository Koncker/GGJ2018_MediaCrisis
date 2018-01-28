using Assets.Scripts.UI.UIBinder;

namespace Assets.Scripts.ViewModel.News
{
    public enum NewsType
    {
        Fake,
        Legit
    }

    public class NewsItemModel
    {
        public PropertyBinder<string> Title = new PropertyBinder<string>();
        public PropertyBinder<NewsType> NewsType = new PropertyBinder<NewsType>();
    }
}

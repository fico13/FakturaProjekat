using System.Windows.Controls;

namespace WPFPresentation.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly ContentControl _navigationHost;
        private readonly Dictionary<string, Func<UserControl>> _views;

        public NavigationService(ContentControl navigationHost)
        {
            _navigationHost = navigationHost;
            _views = new Dictionary<string, Func<UserControl>>();
        }

        public void RegisterView(string viewName, Func<UserControl> viewFactory)
        {
            if (!_views.ContainsKey(viewName))
            {
                _views[viewName] = viewFactory;
            }
        }

        public void NavigateTo(string viewName)
        {
            if (_views.TryGetValue(viewName, out var viewFactory))
            {
                _navigationHost.Content = viewFactory();
            }
            else
            {
                throw new ArgumentException($"View '{viewName}' not found. Did you forget to register it?");
            }
        }
    }
}

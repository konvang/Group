using System.Windows;

namespace SaftStopSystem
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Actions taken on the startup of the application.
        /// </summary>
        /// <param name="e">The startup of the application.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginViewModel viewModel = new LoginViewModel();
            LoginView view = new LoginView();

            WorkspaceWindow window = new WorkspaceWindow();

            window.Width = 400;
#if DEBUG
            window.Width = 550;
#endif
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }
    }
}

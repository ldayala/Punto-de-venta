using Ily_s_Store.Views;
using System.Windows;
using System.Windows.Input;


namespace Ily_s_Store
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnShow_Checked(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 0.5;
        }

        private void BtnHide_Unchecked(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 1;
        }

        private void GridContent_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BtnShowHide.IsChecked=false;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            //Close();
            Application.Current.Shutdown();
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState=WindowState.Minimized;
        }

        private void usuarios_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Usuarios();
            

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //para mover las ventana principal si el el clic del mouse esta presionado;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void productosCRUD(object sender, RoutedEventArgs e)
        {
            DataContext = new Productos();
        }
    }
}

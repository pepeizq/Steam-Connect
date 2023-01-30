using CommunityToolkit.WinUI.UI.Controls;
using Herramientas;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;
using Windows.UI;
using static Steam_Connect.MainWindow;

namespace Plataformas
{
    public static class GOG
    {
        public async static void Cargar()
        {
            ObjetosVentana.prGOGJuegosInstalados.Visibility = Visibility.Visible;
            ObjetosVentana.gvGOGJuegosInstalados.Visibility = Visibility.Collapsed;
            ObjetosVentana.tbGOGMensajeNoJuegos.Visibility = Visibility.Collapsed;

            List<GOGJuego> listaJuegos = new List<GOGJuego>();

            RegistryKey registro = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\GOG.com\\Games");

            foreach (string id in registro.GetSubKeyNames()) 
            {
                RegistryKey registroJuego = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\GOG.com\\Games\\" + id);
                
                if (registroJuego.GetValue("exe") != null)
                {
                    string ejecutable = registroJuego.GetValue("exe").ToString().Trim();
                    string argumentos = registroJuego.GetValue("launchParam").ToString().Trim();
                    string nombre = registroJuego.GetValue("gameName").ToString().Trim();

                    string html = await Decompiladores.CogerHtml("https://api.gog.com/products?ids=" + id);

                    if (html != string.Empty)
                    {
                        List<GOGAPI> json = JsonConvert.DeserializeObject<List<GOGAPI>>(html);

                        if (json != null)
                        {
                            string imagen = json[0].imagenes.logo;
                            imagen = imagen.Replace("_glx_logo", null);

                            if (imagen.Contains("https:") == false)
                            {
                                imagen = "https:" + imagen;
                            }

                            listaJuegos.Add(new GOGJuego(id, nombre, ejecutable, argumentos, imagen));
                        }
                    }
                }               
            }

            if (listaJuegos.Count > 0)
            {
                ObjetosVentana.tbGOGMensajeNoJuegos.Visibility = Visibility.Collapsed;
                ObjetosVentana.gvGOGJuegosInstalados.Items.Clear();

                listaJuegos.Sort(delegate (GOGJuego c1, GOGJuego c2) { return c1.nombre.CompareTo(c2.nombre); });

                foreach (GOGJuego juego in listaJuegos)
                {
                    StackPanel sp = new StackPanel
                    {
                        Orientation = Orientation.Vertical
                    };

                    ToggleSwitch ts = new ToggleSwitch
                    {
                        RequestedTheme = ElementTheme.Dark,
                        OnContent = string.Empty,
                        OffContent = string.Empty,
                        Margin = new Thickness(10, 0, 0, 0),
                        IsOn = AccesosDirectos.ComprobarAcceso(juego.nombre, juego.ejecutable)
                    };

                    sp.Children.Add(ts);

                    ImageEx imagen = new ImageEx
                    {
                        IsCacheEnabled = true,
                        EnableLazyLoading = true,
                        Stretch = Stretch.UniformToFill,
                        Source = juego.imagen,
                        CornerRadius = new CornerRadius(2)
                    };

                    sp.Children.Add(imagen);

                    Button2 botonJuego = new Button2
                    {
                        Content = sp,
                        Margin = new Thickness(0),
                        Padding = new Thickness(0),
                        Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]),
                        BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]),
                        BorderThickness = new Thickness(2),
                        Tag = juego,
                        MaxWidth = 300,
                        CornerRadius = new CornerRadius(5)
                    };

                    botonJuego.Click += JuegoClick;
                    botonJuego.PointerEntered += Animaciones.EntraRatonBoton2;
                    botonJuego.PointerExited += Animaciones.SaleRatonBoton2;

                    GridViewItem item = new GridViewItem
                    {
                        Content = botonJuego,
                        Margin = new Thickness(5, 0, 5, 10)
                    };

                    ObjetosVentana.gvGOGJuegosInstalados.Items.Add(item);
                }

                ObjetosVentana.gvGOGJuegosInstalados.Visibility = Visibility.Visible;
            }
            else
            {
                ObjetosVentana.tbGOGMensajeNoJuegos.Visibility = Visibility.Visible;
            }

            ObjetosVentana.prGOGJuegosInstalados.Visibility = Visibility.Collapsed;         
        }

        private static void JuegoClick(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            GOGJuego juego = boton.Tag as GOGJuego;

            StackPanel sp = boton.Content as StackPanel;
            ToggleSwitch ts = sp.Children[0] as ToggleSwitch;

            if (ts.IsOn == true)
            {
                ts.IsOn = false;
            }
            else
            {
                ts.IsOn = true;
            }

            SteamAccesoDirecto acceso = new SteamAccesoDirecto
            {
                appname = juego.nombre,
                exe = juego.ejecutable,
                icon = juego.imagen
            };

            AccesosDirectos.Modificar(acceso, ts.IsOn);
        }
    }

    public class GOGJuego
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string ejecutable { get; set; }
        public string argumentos { get; set; }
        public string imagen { get; set; }

        public GOGJuego(string Id, string Nombre, string Ejecutable, string Argumentos, string Imagen)
        {
            id = Id;
            nombre = Nombre;
            ejecutable = Ejecutable;
            argumentos = Argumentos;
            imagen = Imagen;
        }
    }
}

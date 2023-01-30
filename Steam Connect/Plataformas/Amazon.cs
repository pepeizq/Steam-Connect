using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Data.Sqlite;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using static Steam_Connect.MainWindow;
using Windows.UI;
using Interfaz;

namespace Plataformas
{
    public static class Amazon
    {
        public static void CargarJuegosInstalados()
        {
            ObjetosVentana.prAmazonJuegosInstalados.Visibility = Visibility.Visible;
            ObjetosVentana.gvAmazonJuegosInstalados.Visibility = Visibility.Collapsed;
            ObjetosVentana.tbAmazonMensajeNoJuegos.Visibility = Visibility.Collapsed;

            List<AmazonJuego> listaJuegos = new List<AmazonJuego>();
            List<string> listaIDsInstalados = new List<string>();
            string rutaBDInstalados = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Amazon Games\Data\Games\Sql\GameInstallInfo.sqlite");

            using (SqliteConnection conexion = new SqliteConnection("Data Source=" + rutaBDInstalados))
            {
                conexion.Open();

                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = @"SELECT * FROM DbSet WHERE Installed = 1;";

                using (SqliteDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        string id = lector.GetString(0);
                        listaIDsInstalados.Add(id);
                    }
                }
            }

            if (listaIDsInstalados.Count > 0)
            {
                string rutaBDDetalles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Amazon Games\Data\Games\Sql\ProductDetails.sqlite");

                using (SqliteConnection conexion = new SqliteConnection("Data Source=" + rutaBDDetalles))
                {
                    conexion.Open();

                    SqliteCommand comando = conexion.CreateCommand();
                    comando.CommandText = @"SELECT * FROM game_product_info;";

                    using (SqliteDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string contenido = lector.GetString(2);

                            foreach (string id in listaIDsInstalados)
                            {
                                if (contenido.Contains(Strings.ChrW(34) + "Id" + Strings.ChrW(34) + ":" + Strings.ChrW(34) + id + Strings.ChrW(34)) == true)
                                {
                                    int int1 = contenido.IndexOf(Strings.ChrW(34) + "ProductTitle" + Strings.ChrW(34));
                                    string temp1 = contenido.Remove(0, int1);

                                    int int2 = temp1.IndexOf(":");
                                    string temp2 = temp1.Remove(0, int2 + 2);

                                    int int3 = temp2.IndexOf(Strings.ChrW(34));
                                    string temp3 = temp2.Remove(int3, temp2.Length - int3);

                                    string nombre = temp3.Trim();

                                    int int4 = contenido.IndexOf(Strings.ChrW(34) + "ProductIconUrl" + Strings.ChrW(34));
                                    string temp4 = contenido.Remove(0, int4);

                                    int int5 = temp4.IndexOf(":");
                                    string temp5 = temp4.Remove(0, int5 + 2);

                                    int int6 = temp5.IndexOf(Strings.ChrW(34));
                                    string temp6 = temp5.Remove(int6, temp5.Length - int6);

                                    string imagen = temp6.Trim();

                                    listaJuegos.Add(new AmazonJuego(nombre, "amazon-games://play/" + id, imagen));
                                }
                            }
                        }
                    }
                }

                if (listaJuegos.Count > 0)
                {
                    ObjetosVentana.tbAmazonMensajeNoJuegos.Visibility = Visibility.Collapsed;
                    ObjetosVentana.gvAmazonJuegosInstalados.Visibility = Visibility.Visible;
                    ObjetosVentana.gvAmazonJuegosInstalados.Items.Clear();

                    listaJuegos.Sort(delegate (AmazonJuego c1, AmazonJuego c2) { return c1.nombre.CompareTo(c2.nombre); });

                    foreach (AmazonJuego juego in listaJuegos)
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
                            Source = juego.imagenGrande,
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

                        ObjetosVentana.gvAmazonJuegosInstalados.Items.Add(item);
                    }
                }
                else
                {
                    ObjetosVentana.tbAmazonMensajeNoJuegos.Visibility = Visibility.Visible;
                }
            }
            else
            {
                ObjetosVentana.tbAmazonMensajeNoJuegos.Visibility = Visibility.Visible;
            }
                
            ObjetosVentana.prAmazonJuegosInstalados.Visibility = Visibility.Collapsed;
        }

        private static void JuegoClick(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            AmazonJuego juego = boton.Tag as AmazonJuego;

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
                icon = juego.imagenGrande
            };

            AccesosDirectos.Modificar(acceso, ts.IsOn);
        }
    }

    public class AmazonJuego
    {
        public string nombre { get; set; }
        public string ejecutable { get; set; }
        public string imagenGrande { get; set; }

        public AmazonJuego(string Nombre, string Ejecutable, string ImagenGrande)
        {
            nombre = Nombre;
            ejecutable = Ejecutable;
            imagenGrande = ImagenGrande;
        }
    }
}

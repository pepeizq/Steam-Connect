using CommunityToolkit.WinUI.UI.Controls;
using Herramientas;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI;
using static Steam_Connect.MainWindow;

namespace Plataformas
{
    public static class Ubisoft
    {
        public static void Cargar()
        {
            ObjetosVentana.botonUbisoftJuegosNoBBDDContactar.Click += AbrirContactarClick;
            ObjetosVentana.botonUbisoftJuegosNoBBDDContactar.PointerEntered += Animaciones.EntraRatonBoton2;
            ObjetosVentana.botonUbisoftJuegosNoBBDDContactar.PointerExited += Animaciones.SaleRatonBoton2;
        }

        private static async void AbrirContactarClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://pepeizqapps.com/contact/"));
        }

        public static async void CargarJuegosInstalados()
        {
            ObjetosVentana.expanderUbisoftJuegosNoBBDD.Visibility = Visibility.Collapsed;
            ObjetosVentana.prUbisoftJuegosInstalados.Visibility = Visibility.Visible;
            ObjetosVentana.gvUbisoftJuegosInstalados.Visibility = Visibility.Collapsed;
            ObjetosVentana.tbUbisoftMensajeNoJuegos.Visibility = Visibility.Collapsed;

            List<string> listaIDsInstaladas = new List<string>();
            RegistryKey registro = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Ubisoft\\Launcher\\Installs");

            if (registro != null)
            {
                foreach (string id in registro.GetSubKeyNames())
                {
                    bool añadir = true;

                    if (listaIDsInstaladas.Count > 0)
                    {
                        foreach (string idInstalada in listaIDsInstaladas)
                        {
                            if (id == idInstalada)
                            {
                                añadir = false;
                            }
                        }
                    }

                    if (añadir == true)
                    {
                        listaIDsInstaladas.Add(id);
                    }
                }
            }
           
            if (listaIDsInstaladas.Count > 0)
            {
                ObjetosVentana.tbUbisoftMensajeNoJuegos.Visibility = Visibility.Collapsed;
                ObjetosVentana.gvUbisoftJuegosInstalados.Items.Clear();

                List<UbisoftJuego> listaJuegos = new List<UbisoftJuego>();
                List<string> listaIDsNoEncontradas = new List<string>();
                string bbdd = await Decompiladores.CogerHtml("https://raw.githubusercontent.com/pepeizq/Database-Games/master/Base%20de%20Datos%20Plataformas/Jsons/Ubisoft.json");
                
                if (bbdd != null)
                {
                    List<UbisoftAPI> json = JsonConvert.DeserializeObject<List<UbisoftAPI>>(bbdd);

                    if (json != null)
                    {
                        if (json.Count > 0)
                        {
                            foreach (string id in listaIDsInstaladas)
                            {
                                bool idEncontrada = false;

                                foreach (UbisoftAPI juegoJson in json)
                                {
                                    foreach (string id2 in juegoJson.idsUbi)
                                    {
                                        if (id == id2)
                                        {
                                            idEncontrada = true;

                                            string imagenPequeña = string.Empty;

                                            if (juegoJson.imagenPequeña != string.Empty)
                                            {
                                                imagenPequeña = juegoJson.imagenPequeña;
                                            }
                                            else
                                            {
                                                if (juegoJson.idSteam != string.Empty)
                                                {
                                                    imagenPequeña = AccesosDirectos.dominioImagenes + "/steam/apps/" + juegoJson.idSteam + "/header.jpg";
                                                }
                                            }

                                            string imagenGrande = string.Empty;

                                            if (juegoJson.imagenGrande != string.Empty)
                                            {
                                                imagenGrande = juegoJson.imagenGrande;
                                            }
                                            else
                                            {
                                                if (juegoJson.idSteam != string.Empty)
                                                {
                                                    imagenGrande = AccesosDirectos.dominioImagenes + "/steam/apps/" + juegoJson.idSteam + "/library_600x900.jpg";
                                                }
                                            }
                                  
                                            listaJuegos.Add(new UbisoftJuego(juegoJson.nombre, "uplay://launch/" + id + "/0",
                                                                    imagenPequeña, imagenGrande, juegoJson.idSteam));
                                            break;
                                        }
                                    }

                                    if (idEncontrada == true)
                                    {
                                        break;
                                    }
                                }

                                if (idEncontrada == false)
                                {
                                    listaIDsNoEncontradas.Add(id);
                                }
                            }
                        }
                    }                    
                }

                if (listaIDsNoEncontradas.Count > 0)
                {
                    ObjetosVentana.expanderUbisoftJuegosNoBBDD.Visibility = Visibility.Visible;
                    ObjetosVentana.expanderUbisoftJuegosNoBBDD.IsExpanded = true;

                    string idsNoEncontradas = string.Empty;

                    foreach (string idNoEncontrada in listaIDsNoEncontradas)
                    {
                        idsNoEncontradas = idsNoEncontradas + idNoEncontrada + Environment.NewLine;
                    }

                    if (idsNoEncontradas != string.Empty) 
                    { 
                        ObjetosVentana.tbUbisoftJuegosNoBBDDIds.Text = idsNoEncontradas;
                    }
                }
                else
                {
                    ObjetosVentana.expanderUbisoftJuegosNoBBDD.Visibility = Visibility.Collapsed;
                }
                
                if (listaJuegos.Count > 0)
                {
                    ObjetosVentana.tbUbisoftMensajeNoJuegos.Visibility = Visibility.Collapsed;
                    ObjetosVentana.gvUbisoftJuegosInstalados.Visibility = Visibility.Visible;
                    ObjetosVentana.gvUbisoftJuegosInstalados.Items.Clear();

                    listaJuegos.Sort(delegate (UbisoftJuego c1, UbisoftJuego c2) { return c1.nombre.CompareTo(c2.nombre); });

                    foreach (UbisoftJuego juego in listaJuegos)
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

                        ObjetosVentana.gvUbisoftJuegosInstalados.Items.Add(item);
                    }
                }
                else
                {
                    ObjetosVentana.tbUbisoftMensajeNoJuegos.Visibility = Visibility.Visible;                 
                }
            }
            else
            {
                ObjetosVentana.tbUbisoftMensajeNoJuegos.Visibility = Visibility.Visible;
            }

            ObjetosVentana.prUbisoftJuegosInstalados.Visibility = Visibility.Collapsed;
        }

        private static void JuegoClick(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            UbisoftJuego juego = boton.Tag as UbisoftJuego;

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
                exe = juego.ejecutable
            };

            if (juego.idSteam != string.Empty && juego.imagenGrande == string.Empty)
            {
                acceso.icon = "pepesteam_" + juego.idSteam;
            }
            else
            {
                acceso.icon = juego.imagenGrande;
            }

            AccesosDirectos.Modificar(acceso, ts.IsOn);
        }
    }

    public class UbisoftJuego
    {
        public string nombre { get; set; }
        public string ejecutable { get; set; }
        public string imagenPequeña { get; set; }
        public string imagenGrande { get; set; }
        public string idSteam { get; set; }

        public UbisoftJuego(string Nombre, string Ejecutable, string ImagenPequeña, string ImagenGrande, string IdSteam)
        {
            nombre = Nombre;
            ejecutable = Ejecutable;
            imagenPequeña = ImagenPequeña;
            imagenGrande = ImagenGrande;
            idSteam = IdSteam;
        }
    }
}

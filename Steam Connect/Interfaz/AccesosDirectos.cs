using Force.Crc32;
using Microsoft.UI.Xaml;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ValveKeyValue;
using Windows.System;
using static Steam_Connect.MainWindow;

namespace Interfaz
{
    public static class AccesosDirectos
    {
        public static string dominioImagenes = "https://cdn.cloudflare.steamstatic.com";

        public static void Cargar()
        {
            ObjetosVentana.botonAccesosDirectosAñadir.Click += Añadir;
            ObjetosVentana.botonAccesosDirectosAñadir.PointerEntered += Animaciones.EntraRatonBoton2;
            ObjetosVentana.botonAccesosDirectosAñadir.PointerExited += Animaciones.SaleRatonBoton2;

            //-------------------------------------

            List<SteamAccesoDirecto> datos = new List<SteamAccesoDirecto>();
            string ficheroAccesos = GenerarRutaAccesos();

            if (File.Exists(ficheroAccesos) == true) 
            {
                using FileStream stream = File.OpenRead(ficheroAccesos);
                KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Binary);
                datos = kv.Deserialize<List<SteamAccesoDirecto>>(stream);
            }

            ObjetosVentana.tbAccesosDirectosCargados.Text = datos.Count.ToString();
            ObjetosVentana.botonAccesosDirectosAñadir.Tag = datos;
        }

        public static void Modificar(SteamAccesoDirecto juego, bool estado)
        {
            List<SteamAccesoDirecto> datos = ObjetosVentana.botonAccesosDirectosAñadir.Tag as List<SteamAccesoDirecto>;

            if (estado == true)
            {
                datos.Add(juego);
            }
            else
            {
                if (datos.Count > 0)
                {
                    int i = 0;
                    foreach (SteamAccesoDirecto acceso in datos)
                    {
                        if (acceso.appname == juego.appname && acceso.exe == juego.exe)
                        {
                            break;
                        }
                     
                        i += 1;
                    }

                    datos.RemoveAt(i);
                }
            }

            ObjetosVentana.tbAccesosDirectosCargados.Text = datos.Count.ToString();

            if (datos.Count > 0)
            {
                ObjetosVentana.botonAccesosDirectosAñadir.IsEnabled = true;
            }
            else
            {
                ObjetosVentana.botonAccesosDirectosAñadir.IsEnabled = false;
            }
        }

        public static bool ComprobarAcceso(string nombre, string ejecutable)
        {
            List<SteamAccesoDirecto> datos = ObjetosVentana.botonAccesosDirectosAñadir.Tag as List<SteamAccesoDirecto>;

            foreach (SteamAccesoDirecto acceso in datos)
            {
                if (acceso.appname == nombre && acceso.exe == ejecutable)
                {
                    return true;
                }
            }

            return false;
        }

        public static async void Añadir(object sender, RoutedEventArgs e)
        {
            ActivarDesactivar(false);
            ObjetosVentana.pbAccesosDirectos.Visibility = Visibility.Visible;
            ObjetosVentana.pbAccesosDirectos.Value = 0;
            await Task.Delay(100);

            List<SteamAccesoDirecto> datos = ObjetosVentana.botonAccesosDirectosAñadir.Tag as List<SteamAccesoDirecto>;

            string ficheroAccesos = GenerarRutaAccesos();
            string carpetaImagenes = ficheroAccesos.Replace("\\shortcuts.vdf", null);
            carpetaImagenes = carpetaImagenes + "\\grid";

            int i = 0;
            foreach (SteamAccesoDirecto acceso in datos)
            {
                if (acceso.icon != null)
                {
                    string enlaceImagenGrid = null;
                    string enlaceImagenLogo = null;
                    string enlaceImagenHero = null;

                    if (acceso.icon.Contains("pepesteam_") == true)
                    {
                        string id = acceso.icon.Replace("pepesteam_", null);

                        enlaceImagenGrid = dominioImagenes + "/steam/apps/" + id + "/library_600x900.jpg";
                        enlaceImagenLogo = dominioImagenes + "/steam/apps/" + id + "/logo.png";
                        enlaceImagenHero = dominioImagenes + "/steam/apps/" + id + "/library_hero.jpg";          
                    }
                    else
                    {
                        enlaceImagenGrid = acceso.icon;                      
                    }

                    acceso.icon = null;

                    string idImagenes = GenerarIdImagen(acceso.appname, acceso.exe).ToString();

                    if (enlaceImagenGrid != null)
                    {
                        DescargarImagen(enlaceImagenGrid, carpetaImagenes + "\\" + idImagenes + "p.png");
                    }
                    
                    if (enlaceImagenLogo != null)
                    {
                        DescargarImagen(enlaceImagenLogo, carpetaImagenes + "\\" + idImagenes + "_logo.png");
                    }
                
                    if (enlaceImagenHero != null)
                    {
                        DescargarImagen(enlaceImagenHero, carpetaImagenes + "\\" + idImagenes + "_hero.jpg");
                    }                  
                }

                ObjetosVentana.pbAccesosDirectos.Value = Convert.ToDouble((100 / datos.Count) * i);
                i += 1;
            }

            using FileStream stream = File.OpenWrite(ficheroAccesos);

            KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Binary);
            kv.Serialize(stream, datos, "shortcuts");

            await Launcher.LaunchUriAsync(new Uri("steam://ExitSteam"));
            await Task.Delay(1000);
            await Launcher.LaunchUriAsync(new Uri("steam://open/games"));

            ObjetosVentana.pbAccesosDirectos.Visibility = Visibility.Collapsed;
            ActivarDesactivar(true);
        }

        private static string GenerarRutaAccesos()
        {
            RegistryKey registro = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");
            string carpetaSteam = registro.GetValue("SteamPath").ToString();
            carpetaSteam = carpetaSteam.Replace("/", "\\");

            RegistryKey registroUsuario = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam\\ActiveProcess");
            string usuario = registroUsuario.GetValue("ActiveUser", RegistryValueKind.DWord).ToString();

            if (usuario == "0")
            {
                RegistryKey registroUsuario2 = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam\\Users");

                foreach (string id in registroUsuario2.GetSubKeyNames())
                {
                    if (id.Length > 2)
                    {
                        usuario = id;
                        break;
                    }
                }
            }

            return carpetaSteam + "\\userdata\\" + usuario + "\\config\\shortcuts.vdf";
        }

        private static UInt64 GenerarIdImagen(string appName, string exe)
        {
            byte[] fusionBytes = Encoding.UTF8.GetBytes(exe + appName + "");
            UInt64 crc = Crc32Algorithm.Compute(fusionBytes);
            UInt64 juegoId = crc | 0x80000000;

            return juegoId;
        }

        private static async void DescargarImagen(string enlace, string ubicacionynombre)
        {
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:15.0) Gecko/20100101 Firefox/15.0.1");
            
            try
            {
                byte[] ficheroBytes = await cliente.GetByteArrayAsync(enlace);
                File.WriteAllBytes(ubicacionynombre, ficheroBytes);
            }
            catch { }
        }

        private static void ActivarDesactivar(bool estado)
        {
            ObjetosVentana.botonAccesosDirectosAñadir.IsEnabled = estado;

            ActivarDesactivarGridviews(estado);
        }
    }

    public class SteamAccesoDirecto
    {
        public string appid { get; set; }
        public string appname { get; set; }
        public string exe { get; set; }
        public string StartDir { get; set; }
        public string icon { get; set; }
        public int IsHidden { get; set; }
        public int AllowDesktopConfig { get; set; }
        public int AllowOverlay { get; set; }
        public int LastPlayTime { get; set; }
        //public List<string> tags { get; set; }
    }
}

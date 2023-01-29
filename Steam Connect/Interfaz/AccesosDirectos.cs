using Force.Crc32;
using Herramientas;
using Microsoft.UI.Xaml;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using ValveKeyValue;
using Windows.Web.Http;
using static Steam_Connect.MainWindow;

namespace Interfaz
{
    public static class AccesosDirectos
    {
        public static string dominioImagenes = "https://cdn.cloudflare.steamstatic.com";

        public static void Cargar()
        {
            ObjetosVentana.botonAccesosDirectosAñadir.Click += Añadir;

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

        public static void Modificar(SteamAccesoDirecto juego, bool estado, string enlaceImagen)
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
        }

        public static void Añadir(object sender, RoutedEventArgs e)
        {
            List<SteamAccesoDirecto> datos = ObjetosVentana.botonAccesosDirectosAñadir.Tag as List<SteamAccesoDirecto>;

            string ficheroAccesos = GenerarRutaAccesos();
            string carpetaImagenes = ficheroAccesos.Replace("\\shortcuts.vdf", null);
            carpetaImagenes = carpetaImagenes + "\\grid";

            foreach (SteamAccesoDirecto acceso in datos)
            {
                if (acceso.icon != null)
                {
                    if (acceso.icon.Contains("pepesteam_") == true)
                    {
                        string id = acceso.icon.Replace("pepesteam_", null);
                        acceso.icon = null;

                        string enlaceImagenGrid = dominioImagenes + "/steam/apps/" + id + "/library_600x900.jpg";
                        string enlaceImagenLogo = dominioImagenes + "/steam/apps/" + id + "/logo.png";
                        string enlaceImagenHero = dominioImagenes + "/steam/apps/" + id + "/library_hero.jpg";

                        string idImagenes = GenerarIdImagen(acceso.appname, acceso.exe).ToString();

                        DescargarImagen(enlaceImagenGrid, carpetaImagenes + "\\" + idImagenes + "p.png");
                        DescargarImagen(enlaceImagenLogo, carpetaImagenes + "\\" + idImagenes + "_logo.png");
                        DescargarImagen(enlaceImagenHero, carpetaImagenes + "\\" + idImagenes + "_hero.jpg");
                    }
                }               
            }


            using var stream = File.OpenWrite(ficheroAccesos);

            var kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Binary);
            kv.Serialize(stream, datos, "shortcuts");
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
            System.Net.Http.HttpClient cliente = new System.Net.Http.HttpClient();
            cliente.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:15.0) Gecko/20100101 Firefox/15.0.1");
            byte[] ficheroBytes = await cliente.GetByteArrayAsync(enlace);
            File.WriteAllBytes(ubicacionynombre, ficheroBytes);

            //var httpResult = await httpClient.GetAsync(new Uri(enlace));
            //using var resultStream = await httpResult.Content.stea;
            //using var fileStream = File.Create(pathToSave);
            //resultStream.CopyTo(fileStream);
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
    }
}

using CommunityToolkit.WinUI.UI.Controls;
using FontAwesome6.Fonts;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using Plataformas;

namespace Steam_Connect
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            CargarObjetosVentana();

            BarraTitulo.Generar(this);
            BarraTitulo.CambiarTitulo(null);
            Cerrar.Cargar();
            Pestañas.Cargar();
            ScrollViewers.Cargar();
            Interfaz.Menu.Cargar();
            Battlenet.Cargar();
            EAPlay.Cargar();
            Ubisoft.Cargar();
            EpicGames.Cargar();
            Opciones.CargarDatos();

            Pestañas.Visibilidad(gridPresentacion, true, null, false);

            AccesosDirectos.Cargar();
        }

        public void CargarObjetosVentana()
        {
            ObjetosVentana.ventana = ventana;
            ObjetosVentana.gridTitulo = gridTitulo;
            ObjetosVentana.tbTitulo = tbTitulo;
            ObjetosVentana.nvPrincipal = nvPrincipal;
            ObjetosVentana.nvItemMenu = nvItemMenu;
            ObjetosVentana.menuItemMenu = menuItemMenu;
            ObjetosVentana.nvItemOpciones = nvItemOpciones;
            ObjetosVentana.nvItemSubirArriba = nvItemSubirArriba;
            ObjetosVentana.gridCierre = gridCierre;

            //-------------------------------------------------------------------

            ObjetosVentana.botonCerrarAppSi = botonCerrarAppSi;
            ObjetosVentana.botonCerrarAppNo = botonCerrarAppNo;
            ObjetosVentana.iconoCerrarAppAzar = iconoCerrarAppAzar;
            ObjetosVentana.tbCerrarAppAzar = tbCerrarAppAzar;
            ObjetosVentana.botonCerrarAppAzar = botonCerrarAppAzar;

            //-------------------------------------------------------------------

            ObjetosVentana.gridPresentacion = gridPresentacion;
            ObjetosVentana.gridGOG = gridGOG;
            ObjetosVentana.gridEAPlay = gridEAPlay;
            ObjetosVentana.gridUbisoft = gridUbisoft;
            ObjetosVentana.gridBattlenet = gridBattlenet;
            ObjetosVentana.gridAmazon = gridAmazon;
            ObjetosVentana.gridEpicGames = gridEpicGames;
            ObjetosVentana.gridAccesosDirectos = gridAccesosDirectos;
            ObjetosVentana.gridOpciones = gridOpciones;

            //-------------------------------------------------------------------

            ObjetosVentana.svPresentacion = svPresentacion;
            ObjetosVentana.gvPresentacionPlataformas = gvPresentacionPlataformas;

            //-------------------------------------------------------------------

            ObjetosVentana.svGOGJuegosInstalados = svGOGJuegosInstalados;
            ObjetosVentana.prGOGJuegosInstalados = prGOGJuegosInstalados;
            ObjetosVentana.gvGOGJuegosInstalados = gvGOGJuegosInstalados;
            ObjetosVentana.tbGOGMensajeNoJuegos = tbGOGMensajeNoJuegos;

            //-------------------------------------------------------------------

            ObjetosVentana.botonEAPlayBuscarJuegosInstalados = botonEAPlayBuscarJuegosInstalados;
            ObjetosVentana.tbEAPlayCarpetaJuegosInstalados = tbEAPlayCarpetaJuegosInstalados;
            ObjetosVentana.svEAPlayJuegosInstalados = svEAPlayJuegosInstalados;
            ObjetosVentana.prEAPlayJuegosInstalados = prEAPlayJuegosInstalados;
            ObjetosVentana.gvEAPlayJuegosInstalados = gvEAPlayJuegosInstalados;
            ObjetosVentana.tbEAPlayMensajeNoJuegos = tbEAPlayMensajeNoJuegos;

            //-------------------------------------------------------------------

            ObjetosVentana.expanderUbisoftJuegosNoBBDD = expanderUbisoftJuegosNoBBDD;
            ObjetosVentana.tbUbisoftJuegosNoBBDDIds = tbUbisoftJuegosNoBBDDIds;
            ObjetosVentana.botonUbisoftJuegosNoBBDDContactar = botonUbisoftJuegosNoBBDDContactar;
            ObjetosVentana.svUbisoftJuegosInstalados = svUbisoftJuegosInstalados;
            ObjetosVentana.prUbisoftJuegosInstalados = prUbisoftJuegosInstalados;
            ObjetosVentana.gvUbisoftJuegosInstalados = gvUbisoftJuegosInstalados;
            ObjetosVentana.tbUbisoftMensajeNoJuegos = tbUbisoftMensajeNoJuegos;

            //-------------------------------------------------------------------

            ObjetosVentana.gridBattlenetMensajeSiJuegos = gridBattlenetMensajeSiJuegos;
            ObjetosVentana.botonBattlenetCerrarMensajeSiJuegos = botonBattlenetCerrarMensajeSiJuegos;
            ObjetosVentana.svBattlenetJuegosInstalados = svBattlenetJuegosInstalados;
            ObjetosVentana.prBattlenetJuegosInstalados = prBattlenetJuegosInstalados;
            ObjetosVentana.gvBattlenetJuegosInstalados = gvBattlenetJuegosInstalados;
            ObjetosVentana.tbBattlenetMensajeNoJuegos = tbBattlenetMensajeNoJuegos;

            //-------------------------------------------------------------------

            ObjetosVentana.svAmazonJuegosInstalados = svAmazonJuegosInstalados;
            ObjetosVentana.prAmazonJuegosInstalados = prAmazonJuegosInstalados;
            ObjetosVentana.gvAmazonJuegosInstalados = gvAmazonJuegosInstalados;
            ObjetosVentana.tbAmazonMensajeNoJuegos = tbAmazonMensajeNoJuegos;

            //-------------------------------------------------------------------

            ObjetosVentana.expanderEpicGamesJuegosNoBBDD = expanderEpicGamesJuegosNoBBDD;
            ObjetosVentana.tbEpicGamesJuegosNoBBDDIds = tbEpicGamesJuegosNoBBDDIds;
            ObjetosVentana.botonEpicGamesJuegosNoBBDDContactar = botonEpicGamesJuegosNoBBDDContactar;
            ObjetosVentana.svEpicGamesJuegosInstalados = svEpicGamesJuegosInstalados;
            ObjetosVentana.prEpicGamesJuegosInstalados = prEpicGamesJuegosInstalados;
            ObjetosVentana.gvEpicGamesJuegosInstalados = gvEpicGamesJuegosInstalados;
            ObjetosVentana.tbEpicGamesMensajeNoJuegos = tbEpicGamesMensajeNoJuegos;

            //-------------------------------------------------------------------

            ObjetosVentana.tbAccesosDirectosCargados = tbAccesosDirectosCargados;
            ObjetosVentana.botonAccesosDirectosAñadir = botonAccesosDirectosAnadir;
            ObjetosVentana.pbAccesosDirectos = pbAccesosDirectos;

            //-------------------------------------------------------------------

            ObjetosVentana.spOpcionesBotones = spOpcionesBotones;
            ObjetosVentana.svOpciones = svOpciones;
            ObjetosVentana.spOpcionesPestañas = spOpcionesPestanas;
            ObjetosVentana.cbOpcionesIdioma = cbOpcionesIdioma;
            ObjetosVentana.cbOpcionesPantalla = cbOpcionesPantalla;
            ObjetosVentana.botonOpcionesLimpiar = botonOpcionesLimpiar;
        }

        public static class ObjetosVentana
        {
            public static Window ventana { get; set; }
            public static Grid gridTitulo { get; set; }
            public static TextBlock tbTitulo { get; set; }
            public static NavigationView nvPrincipal { get; set; }
            public static NavigationViewItem nvItemMenu { get; set; }
            public static MenuFlyout menuItemMenu { get; set; }
            public static NavigationViewItem nvItemOpciones { get; set; }
            public static NavigationViewItem nvItemSubirArriba { get; set; }
            public static Grid gridCierre { get; set; }

            //-------------------------------------------------------------------

            public static Button botonCerrarAppSi { get; set; }
            public static Button botonCerrarAppNo { get; set; }
            public static FontAwesome iconoCerrarAppAzar { get; set; }
            public static TextBlock tbCerrarAppAzar { get; set; }
            public static Button botonCerrarAppAzar { get; set; }

            //-------------------------------------------------------------------

            public static Grid gridPresentacion { get; set; }
            public static Grid gridGOG { get; set; }
            public static Grid gridEAPlay { get; set; }
            public static Grid gridUbisoft { get; set; }
            public static Grid gridBattlenet { get; set; }
            public static Grid gridAmazon { get; set; }
            public static Grid gridEpicGames { get; set; }
            public static Grid gridAccesosDirectos { get; set; }
            public static Grid gridOpciones { get; set; }

            //-------------------------------------------------------------------

            public static ScrollViewer svPresentacion { get; set; }
            public static AdaptiveGridView gvPresentacionPlataformas { get; set; }

            //-------------------------------------------------------------------

            public static ScrollViewer svGOGJuegosInstalados { get; set; }
            public static ProgressRing prGOGJuegosInstalados { get; set; }
            public static AdaptiveGridView gvGOGJuegosInstalados { get; set; }
            public static TextBlock tbGOGMensajeNoJuegos { get; set; }

            //-------------------------------------------------------------------

            public static Button botonEAPlayBuscarJuegosInstalados { get; set; }
            public static TextBlock tbEAPlayCarpetaJuegosInstalados { get; set; }
            public static ScrollViewer svEAPlayJuegosInstalados { get; set; }
            public static ProgressRing prEAPlayJuegosInstalados { get; set; }
            public static AdaptiveGridView gvEAPlayJuegosInstalados { get; set; }
            public static TextBlock tbEAPlayMensajeNoJuegos { get; set; }

            //-------------------------------------------------------------------

            public static Microsoft.UI.Xaml.Controls.Expander expanderUbisoftJuegosNoBBDD { get; set; }
            public static TextBox tbUbisoftJuegosNoBBDDIds { get; set; }
            public static Button botonUbisoftJuegosNoBBDDContactar { get; set; }
            public static ScrollViewer svUbisoftJuegosInstalados { get; set; }
            public static ProgressRing prUbisoftJuegosInstalados { get; set; }
            public static AdaptiveGridView gvUbisoftJuegosInstalados { get; set; }
            public static TextBlock tbUbisoftMensajeNoJuegos { get; set; }

            //-------------------------------------------------------------------

            public static Grid gridBattlenetMensajeSiJuegos { get; set; }
            public static Button botonBattlenetCerrarMensajeSiJuegos { get; set; }
            public static ScrollViewer svBattlenetJuegosInstalados { get; set; }
            public static ProgressRing prBattlenetJuegosInstalados { get; set; }
            public static AdaptiveGridView gvBattlenetJuegosInstalados { get; set; }
            public static TextBlock tbBattlenetMensajeNoJuegos { get; set; }

            //-------------------------------------------------------------------

            public static ScrollViewer svAmazonJuegosInstalados { get; set; }
            public static ProgressRing prAmazonJuegosInstalados { get; set; }
            public static AdaptiveGridView gvAmazonJuegosInstalados { get; set; }
            public static TextBlock tbAmazonMensajeNoJuegos { get; set; }

            //-------------------------------------------------------------------

            public static Microsoft.UI.Xaml.Controls.Expander expanderEpicGamesJuegosNoBBDD { get; set; }
            public static TextBox tbEpicGamesJuegosNoBBDDIds { get; set; }
            public static Button botonEpicGamesJuegosNoBBDDContactar { get; set; }
            public static ScrollViewer svEpicGamesJuegosInstalados { get; set; }
            public static ProgressRing prEpicGamesJuegosInstalados { get; set; }
            public static AdaptiveGridView gvEpicGamesJuegosInstalados { get; set; }
            public static TextBlock tbEpicGamesMensajeNoJuegos { get; set; }

            //-------------------------------------------------------------------

            public static TextBlock tbAccesosDirectosCargados { get; set; }
            public static Button botonAccesosDirectosAñadir { get; set; }
            public static ProgressBar pbAccesosDirectos { get; set; }

            //-------------------------------------------------------------------

            public static StackPanel spOpcionesBotones { get; set; }
            public static ScrollViewer svOpciones { get; set; }
            public static StackPanel spOpcionesPestañas { get; set; }
            public static ComboBox cbOpcionesIdioma { get; set; }
            public static ComboBox cbOpcionesPantalla { get; set; }
            public static Button botonOpcionesLimpiar { get; set; }
        }

        private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            Pestañas.CreadorItems("/Assets/Plataformas/logo_epicgames.png", "Epic Games");
            Pestañas.CreadorItems("/Assets/Plataformas/logo_amazon.png", "Amazon Games");
            Pestañas.CreadorItems("/Assets/Plataformas/logo_battlenet.png", "Battle.net");
            Pestañas.CreadorItems("/Assets/Plataformas/logo_ubisoft.png", "Ubisoft Connect");
            Pestañas.CreadorItems("/Assets/Plataformas/logo_eaplay.png", "EA Play");
            Pestañas.CreadorItems("/Assets/Plataformas/logo_gog.png", "GOG");

            //----------------------------------------------------------

            GridViewItem itemGOG = Presentacion.CreadorBotones("/Assets/Plataformas/logo_gog.png", "GOG", false);
            Button2 botonGOG = itemGOG.Content as Button2;
            botonGOG.Click += AbrirGOGClick;
            ObjetosVentana.gvPresentacionPlataformas.Items.Add(itemGOG);

            GridViewItem itemEAPlay = Presentacion.CreadorBotones("/Assets/Plataformas/logo_eaplay_completo.png", "EA Play", false);
            Button2 botonEAPlay = itemEAPlay.Content as Button2;
            botonEAPlay.Click += AbrirEAPlayClick;
            ObjetosVentana.gvPresentacionPlataformas.Items.Add(itemEAPlay);

            GridViewItem itemUbisoft = Presentacion.CreadorBotones("/Assets/Plataformas/logo_ubisoft_completo.png", "Ubisoft Connect", false);
            Button2 botonUbisoft = itemUbisoft.Content as Button2;
            botonUbisoft.Click += AbrirUbisoftClick;
            ObjetosVentana.gvPresentacionPlataformas.Items.Add(itemUbisoft);

            GridViewItem itemBattlenet = Presentacion.CreadorBotones("/Assets/Plataformas/logo_battlenet_completo.png", "Battle.net", false);
            Button2 botonBattlenet = itemBattlenet.Content as Button2;
            botonBattlenet.Click += AbrirBattlenetClick;
            ObjetosVentana.gvPresentacionPlataformas.Items.Add(itemBattlenet);

            GridViewItem itemAmazon = Presentacion.CreadorBotones("/Assets/Plataformas/logo_amazon.png", "Amazon Games", false);
            Button2 botonAmazon = itemAmazon.Content as Button2;
            botonAmazon.Click += AbrirAmazonClick;
            ObjetosVentana.gvPresentacionPlataformas.Items.Add(itemAmazon);

            GridViewItem itemEpicGames = Presentacion.CreadorBotones("/Assets/Plataformas/logo_epicgames.png", "Epic Games", false);
            Button2 botonEpicGames = itemEpicGames.Content as Button2;
            botonEpicGames.Click += AbrirEpicGamesClick;
            ObjetosVentana.gvPresentacionPlataformas.Items.Add(itemEpicGames);
        }

        private void nvPrincipal_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ResourceLoader recursos = new ResourceLoader();

            if (args.InvokedItemContainer != null)
            {
                if (args.InvokedItemContainer.GetType() == typeof(NavigationViewItem2))
                {
                    NavigationViewItem2 item = args.InvokedItemContainer as NavigationViewItem2;

                    if (item.Name == "nvItemMenu")
                    {

                    }
                    else if (item.Name == "nvItemOpciones")
                    {
                        Pestañas.Visibilidad(gridOpciones, true, null, false);
                        BarraTitulo.CambiarTitulo(recursos.GetString("Options"));
                        ScrollViewers.EnseñarSubir(svOpciones);
                    }
                }
            }

            if (args.InvokedItem != null)
            {
                if (args.InvokedItem.GetType() == typeof(StackPanel2))
                {
                    StackPanel2 sp = (StackPanel2)args.InvokedItem;

                    if (sp.Children[1] != null)
                    {
                        if (sp.Children[1].GetType() == typeof(TextBlock))
                        {
                            TextBlock tb = sp.Children[1] as TextBlock;

                            if (tb.Text == "GOG")
                            {
                                Pestañas.Visibilidad(gridGOG, true, sp, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svGOGJuegosInstalados);
                                GOG.Cargar();
                            }
                            else if (tb.Text == "EA Play")
                            {
                                Pestañas.Visibilidad(gridEAPlay, true, sp, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svEAPlayJuegosInstalados);
                                EAPlay.CargarJuegosInstalados();
                            }
                            else if (tb.Text == "Ubisoft Connect")
                            {
                                Pestañas.Visibilidad(gridUbisoft, true, sp, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svUbisoftJuegosInstalados);
                                Ubisoft.CargarJuegosInstalados();
                            }
                            else if (tb.Text == "Battle.net")
                            {
                                Pestañas.Visibilidad(gridBattlenet, true, sp, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svBattlenetJuegosInstalados);
                                Battlenet.CargarJuegosInstalados();
                            }
                            else if (tb.Text == "Amazon Games")
                            {
                                Pestañas.Visibilidad(gridAmazon, true, sp, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svAmazonJuegosInstalados);
                                Amazon.CargarJuegosInstalados();
                            }
                            else if (tb.Text == "Epic Games")
                            {
                                Pestañas.Visibilidad(gridEpicGames, true, sp, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svEpicGamesJuegosInstalados);
                                EpicGames.CargarJuegosInstalados();
                            }
                        }
                    }
                }
            }
        }
        private void AbrirGOGClick(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)ObjetosVentana.nvPrincipal.MenuItems[1];
            Pestañas.Visibilidad(gridGOG, true, sp, true);
            BarraTitulo.CambiarTitulo(null);
            ScrollViewers.EnseñarSubir(svGOGJuegosInstalados);
            GOG.Cargar();
        }

        private void AbrirEAPlayClick(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)ObjetosVentana.nvPrincipal.MenuItems[2];
            Pestañas.Visibilidad(gridEAPlay, true, sp, true);
            BarraTitulo.CambiarTitulo(null);
            ScrollViewers.EnseñarSubir(svEAPlayJuegosInstalados);
            EAPlay.CargarJuegosInstalados();
        }

        private void AbrirUbisoftClick(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)ObjetosVentana.nvPrincipal.MenuItems[3];
            Pestañas.Visibilidad(gridUbisoft, true, sp, true);
            BarraTitulo.CambiarTitulo(null);
            ScrollViewers.EnseñarSubir(svUbisoftJuegosInstalados);
            Ubisoft.CargarJuegosInstalados();
        }

        private void AbrirBattlenetClick(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)ObjetosVentana.nvPrincipal.MenuItems[4];
            Pestañas.Visibilidad(gridBattlenet, true, sp, true);
            BarraTitulo.CambiarTitulo(null);
            ScrollViewers.EnseñarSubir(svBattlenetJuegosInstalados);
            Battlenet.CargarJuegosInstalados();
        }

        private void AbrirAmazonClick(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)ObjetosVentana.nvPrincipal.MenuItems[5];
            Pestañas.Visibilidad(gridAmazon, true, sp, true);
            BarraTitulo.CambiarTitulo(null);
            ScrollViewers.EnseñarSubir(svAmazonJuegosInstalados);
            Amazon.CargarJuegosInstalados();
        }

        private void AbrirEpicGamesClick(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)ObjetosVentana.nvPrincipal.MenuItems[6];
            Pestañas.Visibilidad(gridEpicGames, true, sp, true);
            BarraTitulo.CambiarTitulo(null);
            ScrollViewers.EnseñarSubir(svEpicGamesJuegosInstalados);
            EpicGames.CargarJuegosInstalados();
        }

        public static void ActivarDesactivarGridviews(bool estado)
        {
            foreach (GridViewItem item in ObjetosVentana.gvGOGJuegosInstalados.Items)
            {
                item.IsEnabled = estado;
            }

            foreach (GridViewItem item in ObjetosVentana.gvEAPlayJuegosInstalados.Items)
            {
                item.IsEnabled = estado;
            }

            foreach (GridViewItem item in ObjetosVentana.gvUbisoftJuegosInstalados.Items)
            {
                item.IsEnabled = estado;
            }

            foreach (GridViewItem item in ObjetosVentana.gvBattlenetJuegosInstalados.Items)
            {
                item.IsEnabled = estado;
            }

            foreach (GridViewItem item in ObjetosVentana.gvAmazonJuegosInstalados.Items)
            {
                item.IsEnabled = estado;
            }

            foreach (GridViewItem item in ObjetosVentana.gvEpicGamesJuegosInstalados.Items)
            {
                item.IsEnabled = estado;
            }
        }
    }
}
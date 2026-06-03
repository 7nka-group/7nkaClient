using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace WebApp;

public class MainForm : Form
{
    // Replace with your Netlify URL.
    private const string SiteUrl = "https://7nkaclient.netlify.app";

    private readonly WebView2 _web = new();
    private FormWindowState _prevState;
    private FormBorderStyle _prevBorder;
    private Rectangle _prevBounds;
    private bool _isFullscreen;

    public MainForm()
    {
        Text = "WebApp";
        Width = 1200;
        Height = 800;
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(480, 360);
        KeyPreview = true;

        _web.Dock = DockStyle.Fill;
        Controls.Add(_web);

        Load += async (_, _) => await InitAsync();
        KeyDown += OnKeyDown;
    }

    private async Task InitAsync()
    {
        var userDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WebApp",
            "WebView2");
        Directory.CreateDirectory(userDataFolder);

        var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
        await _web.EnsureCoreWebView2Async(env);

        var s = _web.CoreWebView2.Settings;
        s.AreDevToolsEnabled = false;
        s.AreDefaultContextMenusEnabled = false;
        s.IsStatusBarEnabled = false;
        s.IsZoomControlEnabled = true;

        _web.CoreWebView2.ContainsFullScreenElementChanged += OnSiteFullscreenChanged;
        _web.CoreWebView2.Navigate(SiteUrl);
    }

    private void OnSiteFullscreenChanged(object? sender, object e)
    {
        SetFullscreen(_web.CoreWebView2.ContainsFullScreenElement);
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F11)
        {
            SetFullscreen(!_isFullscreen);
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Escape && _isFullscreen)
        {
            SetFullscreen(false);
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.F5)
        {
            _web.CoreWebView2?.Reload();
            e.Handled = true;
        }
    }

    private void SetFullscreen(bool enable)
    {
        if (enable == _isFullscreen) return;

        if (enable)
        {
            _prevState = WindowState;
            _prevBorder = FormBorderStyle;
            _prevBounds = Bounds;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            Bounds = Screen.FromControl(this).Bounds;
            TopMost = true;
        }
        else
        {
            TopMost = false;
            FormBorderStyle = _prevBorder;
            WindowState = _prevState;
            Bounds = _prevBounds;
        }

        _isFullscreen = enable;
    }
}

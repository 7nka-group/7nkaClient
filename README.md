# 7nkaClient

A desktop launcher for the 7nka group's game servers. The app wraps a small WebView2 surface inside a native Windows shell, so the server list, news, and quick-connect links live in one place instead of scattered across Discord pins and forum posts. Currently it points to our FiveM and Minecraft servers, with room to add more as the group grows.

Under the hood it's a .NET 8 WinForms project that publishes to a single self-contained `.exe` — no installer, no runtime to chase down, no registry entries left behind. Drop the file anywhere, double-click, and it runs at user-level privileges. The whole windows/ folder is the build; the web/ folder is the static content the client loads.

Built for the people who actually play on the servers, not as a portfolio piece. If something feels off or a server link breaks, open an issue. MIT licensed, so fork it, gut it, or ship your own variant — just don't pretend you wrote it from scratch.

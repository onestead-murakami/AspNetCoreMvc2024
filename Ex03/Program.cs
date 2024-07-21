namespace Ex03;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // MVC機能を有効化
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // app.MapGet("/", () => "Hello World!");

        // 既定のファイルマッピングを有効
        app.UseDefaultFiles();

        // wwwroot配下をドキュメントルートとする静的ファイルを提供可能
        app.UseStaticFiles();

        // MVCルーティングの基本ルール
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}"
        );

        app.Run();
    }
}

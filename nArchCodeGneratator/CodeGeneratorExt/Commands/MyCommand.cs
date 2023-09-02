using MaterialDesignThemes.Wpf;

namespace CodeGeneratorExt
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
           Form1 form1 = new Form1();
            form1.ShowDialog();
            await VS.MessageBox.ShowWarningAsync("CodeGeneratorExt", "Button clicked");
        }
    }
}

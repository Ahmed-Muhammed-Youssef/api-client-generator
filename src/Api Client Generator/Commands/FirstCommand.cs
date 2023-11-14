namespace Api_Client_Generator
{
    [Command("<insert guid from .vsct file>", 0x0100)]
    internal sealed class FirstCommand : BaseCommand<FirstCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("FirstCommand", "Button clicked");
        }
    }
}

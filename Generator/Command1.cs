using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;
using Generator.Forms;
using EnvDTE;
using Generator.Helpers;
using EnvDTE80;
using System.Linq;

namespace Generator
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class Command1
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("b6ce2924-a6ec-496c-81e6-5074198f242d");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command1"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private Command1(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.StartGenerating, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static Command1 Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command1's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new Command1(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void StartGenerating(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string url = null;
            List<string> projectNames = null;
            string selectedProjectName = null;
            // Get the json URL
            UrlInputDialog dialog = new UrlInputDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                url = dialog.GetUrl();
            }

            // get current project's names
            projectNames = SolutionProjects.GetProjectsNames((IVsSolution)Package.GetGlobalService(typeof(SVsSolution)));

            

            // Display the projects' names
            // and choose one of them

            ProjectSelectionDialog seletDialog = new ProjectSelectionDialog(projectNames.ToArray());
            if (seletDialog.ShowDialog() == DialogResult.OK)
            {
                selectedProjectName = seletDialog.GetSelectedProject();
                // Use the selected project
                // Test
                Console.WriteLine(selectedProjectName);
            }

            // var dte = SolutionProjects.GetDTEProject((IVsSolution)Package.GetGlobalService(typeof(SVsSolution)));
            DTE2 dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
            // Find the project with the selected name
            EnvDTE.Project selectedProject = dte.Solution.Projects
                .OfType<EnvDTE.Project>()
                .FirstOrDefault(p => p.Name == selectedProjectName);

            if (selectedProject != null)
            {
                // Get the path of the project
                string projectPath = selectedProject.FullName;
                // Use the project path here
            }

        }
       
       
    }
}
